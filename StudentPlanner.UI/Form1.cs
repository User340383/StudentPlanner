using StudentPlanner.Core;

namespace StudentPlanner.UI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnAddCourse_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnEditCourse_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
		}

		private void btnDeleteCourse_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet");
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
	}
}
