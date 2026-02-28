using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using StudentPlanner.Data;

namespace StudentPlanner.UI
{
	public partial class Form1 : Form
	{
		private readonly ICourseRepository _courses = new CourseRepository();
		private readonly ITaskRepository _tasks = new TaskRepository();
		private readonly IAvailabilityRepository _availability = new AvailabilityRepository();
		private readonly ICommitmentRepository _commitments = new CommitmentRepository();
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
			// Need at least one course because Tasks have CourseId (FK)
			var courses = _courses.GetAll();
			if (courses.Count == 0)
			{
				MessageBox.Show("Please add a course first before creating tasks.");
				return;
			}

			using var dlg = new TaskEditForm(courses, "Add Task");

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			try
			{
				_tasks.Add(dlg.ResultTask);
				RefreshTasksGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Could not add task: {ex.Message}");
			}
		}

		private void btnEditTask_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedTask();
			if (selected == null)
			{
				MessageBox.Show("Select a task first.");
				return;
			}

			var courses = _courses.GetAll();
			if (courses.Count == 0)
			{
				MessageBox.Show("No courses exist. Add a course first.");
				return;
			}
			using var dlg = new TaskEditForm(courses, "Edit Task", selected);

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			// IMPORTANT: preserve the task ID so UPDATE targets the right row
			var updated = dlg.ResultTask;
			updated.Id = selected.Id;

			try
			{
				_tasks.Update(updated);
				RefreshTasksGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Could not update task: {ex.Message}");
			}
		}

		private void btnDeleteTask_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedTask();
			if (selected == null)
			{
				MessageBox.Show("Select a task first.");
				return;
			}

			var confirm = MessageBox.Show(
				$"Delete task '{selected.Title}'?",
				"Confirm delete",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (confirm != DialogResult.Yes)
			{
				return;
			}

			try
			{
				_tasks.Delete(selected.Id);
				RefreshTasksGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Could not delete task: {ex.Message}");
			}
		}

		private void btnAddAvailability_Click(object sender, EventArgs e)
		{
			using var dlg = new AvailabilityEditForm("Add Availability");

			if (dlg.ShowDialog(this) != DialogResult.OK)
				return;

			_availability.Add(dlg.Result);
			RefreshAvailabilityGrid();
		}

		private void btnDeleteAvailability_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedAvailability();
			if (selected == null)
			{
				MessageBox.Show("Select an availability block first.");
				return;
			}

			var confirm = MessageBox.Show(
				$"Delete availability on {selected.Day} {selected.Start:hh\\:mm}-{selected.End:hh\\:mm}?",
				"Confirm delete",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (confirm != DialogResult.Yes) return;

			_availability.Delete(selected.Id);
			RefreshAvailabilityGrid();
		}

		private void btnAddCommitment_Click(object sender, EventArgs e)
		{
			using var dlg = new CommitmentEditForm("Add Commitment");

			if (dlg.ShowDialog(this) != DialogResult.OK)
				return;

			_commitments.Add(dlg.Result);
			RefreshCommitmentsGrid();
		}

		private void btnDeleteCommitment_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedCommitment();
			if (selected == null)
			{
				MessageBox.Show("Select a commitment first.");
				return;
			}

			var confirm = MessageBox.Show(
				$"Delete commitment '{selected.Description}' on {selected.Day} {selected.Start:hh\\:mm}-{selected.End:hh\\:mm}?",
				"Confirm delete",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (confirm != DialogResult.Yes) return;

			_commitments.Delete(selected.Id);
			RefreshCommitmentsGrid();
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
			dgvCourses.AutoGenerateColumns = true; // default, but explicit is fine
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
			RefreshTasksGrid();
			RefreshAvailabilityGrid();
			RefreshCommitmentsGrid();
		}

		private Course? GetSelectedCourse()
		{
			return dgvCourses.CurrentRow?.DataBoundItem as Course;
		}

		private void RefreshTasksGrid()
		{
			var tasks = _tasks.GetAll();

			dgvTasks.DataSource = null;
			dgvTasks.DataSource = tasks;

			// Hide internal ids (optional)
			if (dgvTasks.Columns.Contains("Id"))
				dgvTasks.Columns["Id"].Visible = false;

			if (dgvTasks.Columns.Contains("CourseId"))
			{
				dgvTasks.Columns["CourseId"].Visible = false; // optional (hide if you don't want it visible)
			}

			dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvTasks.MultiSelect = false;
			dgvTasks.ReadOnly = true;
			dgvTasks.AllowUserToAddRows = false;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedTab == tabPageTasks)
			{
				RefreshTasksGrid();
			}

			if (tabControl1.SelectedTab == tabPageCourses)
			{
				RefreshCoursesGrid();
			}

			if (tabControl1.SelectedTab == tabPageAvailability)
			{
				RefreshAvailabilityGrid();
				RefreshCommitmentsGrid();
			}
		}

		private TaskItem? GetSelectedTask()
		{
			return dgvTasks.CurrentRow?.DataBoundItem as TaskItem;
		}

		private void dgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			btnEditTask.PerformClick();
		}

		private void RefreshAvailabilityGrid()
		{
			var blocks = _availability.GetAll();

			dgvAvailability.DataSource = null;
			dgvAvailability.DataSource = blocks;

			if (dgvAvailability.Columns.Contains("Id"))
				dgvAvailability.Columns["Id"].Visible = false;

			dgvAvailability.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvAvailability.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvAvailability.MultiSelect = false;
			dgvAvailability.ReadOnly = true;
			dgvAvailability.AllowUserToAddRows = false;
		}

		private void RefreshCommitmentsGrid()
		{
			var items = _commitments.GetAll();

			dgvCommitments.DataSource = null;
			dgvCommitments.DataSource = items;

			if (dgvCommitments.Columns.Contains("Id"))
				dgvCommitments.Columns["Id"].Visible = false;

			dgvCommitments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvCommitments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvCommitments.MultiSelect = false;
			dgvCommitments.ReadOnly = true;
			dgvCommitments.AllowUserToAddRows = false;
		}

		private Availability? GetSelectedAvailability()
		{
			return dgvAvailability.CurrentRow?.DataBoundItem as Availability;
		}

		private Commitment? GetSelectedCommitment()
		{
			return dgvCommitments.CurrentRow?.DataBoundItem as Commitment;
		}
	}
}
