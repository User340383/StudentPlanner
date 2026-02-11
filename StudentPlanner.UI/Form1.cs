using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using StudentPlanner.Data;

namespace StudentPlanner.UI
{
	public partial class Form1 : Form
	{
		private readonly ICourseRepository _courses = new CourseRepository();
		public Form1()
		{
			InitializeComponent();
		}

		private void btnAddCourse_Click(object sender, EventArgs e)
		{
			string name = txtCourseName.Text.Trim();
			if (string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Please enter a course name.");
				return;
			}

			try
			{
				_courses.Add(name);
				txtCourseName.Clear();
				RefreshCoursesGrid();
			}
			catch (Exception)
			{
				MessageBox.Show("That course already exists (or the database rejected it).");
			}
		}

		private void btnEditCourse_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedCourse();
			if (selected == null)
			{
				MessageBox.Show("Select a course first.");
				return;
			}

			string newName = txtCourseName.Text.Trim();
			if (string.IsNullOrWhiteSpace(newName))
			{
				MessageBox.Show("Type the new course name in the textbox first.");
				return;
			}

			try
			{
				_courses.Update(selected.Id, newName);
				txtCourseName.Clear();
				RefreshCoursesGrid();
			}
			catch
			{
				MessageBox.Show("That course name already exists.");
			}
		}

		private void btnDeleteCourse_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedCourse();
			if (selected == null)
			{
				MessageBox.Show("Select a course first.");
				return;
			}

			var confirm = MessageBox.Show($"Delete '{selected.Name}'?", "Confirm", MessageBoxButtons.YesNo);
			if (confirm != DialogResult.Yes) return;

			_courses.Delete(selected.Id);
			RefreshCoursesGrid();
		}

		private void btnAddTask_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnEditTask_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnDeleteTask_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnAddAvailability_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnDeleteAvailability_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnAddCommitment_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnDeleteCommitment_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnGenerateSchedule_Click(object sender, EventArgs e)
		{
			IScheduler scheduler = new DummyScheduler();

			var input = new ScheduleInput
			{
				Tasks = new List<TaskItem>(),
				Availability = new List<Availability>(),
				Commitments = new List<Commitment>()
			};

			var result = scheduler.GenerateWeeklySchedule(input);

			MessageBox.Show(result.Warnings.First());
		}

		private void btnRegenerateSchedule_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void RefreshCoursesGrid()
		{
			var courses = _courses.GetAll();

			dgvCourses.DataSource = null;
			dgvCourses.AutoGenerateColumns = true;   // default, but explicit is fine
			dgvCourses.DataSource = courses;

			// Optional: hide internal Id
			if (dgvCourses.Columns.Contains("Id"))
			{
				dgvCourses.Columns["Id"].Visible = false;
			}

			dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvCourses.MultiSelect = false;
			dgvCourses.ReadOnly = true;
			dgvCourses.AllowUserToAddRows = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			RefreshCoursesGrid();
		}

		private Course? GetSelectedCourse()
		{
			return dgvCourses.CurrentRow?.DataBoundItem as Course;
		}
	}
}
