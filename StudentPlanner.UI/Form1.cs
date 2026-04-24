using StudentPlanner.Core; // Domain models + interfaces (contracts)
using StudentPlanner.Data; // Concrete repository implementations (SQLite persistence)
using System.Text;

namespace StudentPlanner.UI
{
	public partial class Form1 : Form
	{
		// Repositories provide persistence operations (CRUD) for each domain entity.
		// We store them as interface types so the UI depends on abstractions (Core),
		// not directly on implementation details (Data).
		private readonly ICourseRepository _courses = new CourseRepository();
		private readonly ITaskRepository _tasks = new TaskRepository();
		private readonly IAvailabilityRepository _availability = new AvailabilityRepository();
		private readonly ICommitmentRepository _commitments = new CommitmentRepository();
		private readonly IScheduleBlockRepository _scheduleBlocks = new ScheduleBlockRepository();

		private ScheduleResult? _lastSchedule;

		public Form1()
		{
			InitializeComponent();
		}

		// -----------------------------
		// Courses tab: Add/Edit/Delete
		// -----------------------------

		private void btnAddCourse_Click(object sender, EventArgs e)
		{
			// Read user input and normalize it (trim whitespace).
			// This prevents accidental duplicates like "Math" vs "Math ".
			string name = txtCourseName.Text.Trim();

			// Validate required field early to avoid DB round-trips.
			if (string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Please enter a course name.");
				return;
			}

			try
			{
				// Persist course. The DB enforces uniqueness on Name.
				_courses.Add(name);

				// Clear input and refresh the grid to reflect saved state.
				txtCourseName.Clear();
				RefreshCoursesGrid();
			}
			catch (Exception)
			{
				// In production you'd log ex details.
				// Here we give the user a friendly message without exposing internals.
				MessageBox.Show("That course already exists (or the database rejected it).");
			}
		}

		private void btnEditCourse_Click(object sender, EventArgs e)
		{
			// Editing requires a selected row in the courses grid.
			var selected = GetSelectedCourse();
			if (selected == null)
			{
				MessageBox.Show("Select a course first.");
				return;
			}

			// For this simple UI, we use the textbox as the "new name".
			// (A dedicated edit dialog is a common future improvement.)
			string newName = txtCourseName.Text.Trim();
			if (string.IsNullOrWhiteSpace(newName))
			{
				MessageBox.Show("Type the new course name in the textbox first.");
				return;
			}

			try
			{
				// Update uses the selected course Id to target the correct record.
				_courses.Update(selected.Id, newName);

				// Clear input and reload the grid so the user sees the result immediately.
				txtCourseName.Clear();
				RefreshCoursesGrid();
			}
			catch
			{
				// Typically triggered by the UNIQUE constraint on Courses.Name.
				MessageBox.Show("That course name already exists.");
			}
		}

		private void btnDeleteCourse_Click(object sender, EventArgs e)
		{
			// Deleting requires a selected course.
			var selected = GetSelectedCourse();
			if (selected == null)
			{
				MessageBox.Show("Select a course first.");
				return;
			}

			// Confirm destructive action.
			var confirm = MessageBox.Show(
				$"Delete '{selected.Name}'?",
				"Confirm",
				MessageBoxButtons.YesNo);

			if (confirm != DialogResult.Yes)
			{
				return;
			}

			// Delete and refresh to keep UI consistent with DB state.
			_courses.Delete(selected.Id);
			RefreshCoursesGrid();
		}

		// -----------------------------
		// Tasks tab: Add/Edit/Delete
		// -----------------------------

		private void btnAddTask_Click(object sender, EventArgs e)
		{
			// Tasks reference Courses via CourseId (foreign key),
			// so at least one course must exist.
			var courses = _courses.GetAll();
			if (courses.Count == 0)
			{
				MessageBox.Show("Please add a course first before creating tasks.");
				return;
			}

			// Use a modal dialog to collect task details in a clean UX.
			using var dlg = new TaskEditForm(courses, "Add Task");

			// If the user cancels, do nothing.
			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			try
			{
				// Persist task to DB.
				_tasks.Add(dlg.ResultTask);

				// Refresh grid to show the inserted record.
				RefreshTasksGrid();
			}
			catch (Exception ex)
			{
				// Friendly error for the user; include message for debugging.
				MessageBox.Show($"Could not add task: {ex.Message}");
			}
		}

		private void btnEditTask_Click(object sender, EventArgs e)
		{
			// Edit requires selection.
			var selected = GetSelectedTask();
			if (selected == null)
			{
				MessageBox.Show("Select a task first.");
				return;
			}

			// Tasks depend on courses; ensure we can populate the course dropdown.
			var courses = _courses.GetAll();
			if (courses.Count == 0)
			{
				MessageBox.Show("No courses exist. Add a course first.");
				return;
			}

			// Open dialog pre-filled with the selected task.
			using var dlg = new TaskEditForm(courses, "Edit Task", selected);

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			// IMPORTANT:
			// The dialog returns a new TaskItem built from user input.
			// We must preserve the original Id so Update() targets the correct DB row.
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

		// -----------------------------
		// Availability tab: Add/Delete
		// -----------------------------

		private void btnAddAvailability_Click(object sender, EventArgs e)
		{
			// Modal dialog collects day + start + end; it returns Availability in dlg.Result.
			using var dlg = new AvailabilityEditForm("Add Availability");

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			// Persist and refresh immediately.
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

			if (confirm != DialogResult.Yes)
			{
				return;
			}

			_availability.Delete(selected.Id);
			RefreshAvailabilityGrid();
		}

		// -----------------------------
		// Commitments tab: Add/Delete
		// -----------------------------

		private void btnAddCommitment_Click(object sender, EventArgs e)
		{
			using var dlg = new CommitmentEditForm("Add Commitment");

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

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

			if (confirm != DialogResult.Yes)
			{
				return;
			}

			_commitments.Delete(selected.Id);
			RefreshCommitmentsGrid();
		}

		// -----------------------------
		// Scheduling (stub)
		// -----------------------------

		private void btnGenerateSchedule_Click(object sender, EventArgs e)
		{
			IScheduler scheduler = new GreedyScheduler(TimeSpan.FromMinutes(60));

			var input = new ScheduleInput
			{
				Tasks = _tasks.GetAll(),
				Availability = _availability.GetAll(),
				Commitments = _commitments.GetAll()
			};

			var result = scheduler.GenerateWeeklySchedule(input);

			// Replace old persisted schedule with the newly generated one
			_scheduleBlocks.DeleteAll();
			_scheduleBlocks.AddMany(result.ScheduleBlocks);

			// Reload from DB so SQLite becomes the source of truth
			RefreshScheduleGrid();
			ShowWarnings(result.Warnings);
		}

		private void btnRegenerateSchedule_Click(object sender, EventArgs e)
		{
			IScheduler scheduler = new GreedyScheduler(TimeSpan.FromMinutes(60));

			// Load current persisted state
			var allTasks = _tasks.GetAll();
			var availability = _availability.GetAll();
			var commitments = _commitments.GetAll();
			var existingBlocks = _scheduleBlocks.GetAll();

			// Blocks we must preserve during regeneration
			var preservedBlocks = existingBlocks
				.Where(b => b.IsLocked || b.IsCompleted)
				.ToList();

			// Convert preserved blocks into temporary commitments
			// so the scheduler will avoid those time ranges.
			var preservedAsCommitments = preservedBlocks
				.Select(b => new Commitment
				{
					Day = b.Date.DayOfWeek,
					Start = b.Start,
					End = b.End,
					Description = b.IsLocked ? "Locked schedule block" : "Completed schedule block"
				})
				.ToList();

			// Combine real commitments with preserved schedule blocks
			var effectiveCommitments = commitments
				.Concat(preservedAsCommitments)
				.ToList();

			// Build a new task list with reduced remaining hours
			var regeneratedTasks = new List<TaskItem>();

			foreach (var task in allTasks)
			{
				// Sum completed block hours for this task
				double preservedHours = existingBlocks
					.Where(b => b.TaskId == task.Id && (b.IsCompleted || b.IsLocked))
					.Sum(b => (b.End - b.Start).TotalHours);

				double remainingHours = Math.Max(0, task.EstimatedHours - preservedHours);

				// Skip tasks that are already effectively completed
				if (remainingHours <= 0.0001)
				{
					continue;
				}

				regeneratedTasks.Add(new TaskItem
				{
					Id = task.Id,
					CourseId = task.CourseId,
					Title = task.Title,
					Deadline = task.Deadline,
					EstimatedHours = remainingHours,
					Priority = task.Priority,
					IsCompleted = task.IsCompleted
				});
			}

			var input = new ScheduleInput
			{
				Tasks = regeneratedTasks,
				Availability = availability,
				Commitments = effectiveCommitments
			};

			var result = scheduler.GenerateWeeklySchedule(input);

			// Remove only blocks that are allowed to be regenerated
			_scheduleBlocks.DeleteUnlockedAndIncomplete();

			// Persist the newly generated blocks
			_scheduleBlocks.AddMany(result.ScheduleBlocks);

			// Reload persisted state into UI
			RefreshScheduleGrid();
			ShowWarnings(result.Warnings);
		}

		// -----------------------------
		// Grid binding helpers
		// -----------------------------

		private void RefreshCoursesGrid()
		{
			// Load from DB on-demand (no caching), ensuring UI reflects persisted state.
			var courses = _courses.GetAll();

			// Reset binding to avoid stale data.
			dgvCourses.DataSource = null;
			dgvCourses.AutoGenerateColumns = true;
			dgvCourses.DataSource = courses;

			// Hide internal DB key from user (optional).
			if (dgvCourses.Columns.Contains("Id"))
			{
				dgvCourses.Columns["Id"].Visible = false;
			}

			// Standardized grid UX.
			dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvCourses.MultiSelect = false;
			dgvCourses.ReadOnly = true;
			dgvCourses.AllowUserToAddRows = false;
		}

		private void RefreshTasksGrid()
		{
			var tasks = _tasks.GetAll();

			dgvTasks.DataSource = null;
			dgvTasks.DataSource = tasks;

			dgvTasks.Columns["Title"].HeaderText = "Task";
			dgvTasks.Columns["Deadline"].HeaderText = "Due Date";
			dgvTasks.Columns["EstimatedHours"].HeaderText = "Hours";

			if (dgvTasks.Columns["IsCompleted"] != null)
			{
				dgvTasks.Columns["IsCompleted"].Visible = false;
			}

			// Hide internal DB keys (optional).
			if (dgvTasks.Columns.Contains("Id"))
			{
				dgvTasks.Columns["Id"].Visible = false;
			}

			if (dgvTasks.Columns.Contains("CourseId"))
			{
				dgvTasks.Columns["CourseId"].Visible = false;
			}

			dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvTasks.MultiSelect = false;
			dgvTasks.ReadOnly = true;
			dgvTasks.AllowUserToAddRows = false;
		}

		private void RefreshAvailabilityGrid()
		{
			var blocks = _availability.GetAll();

			dgvAvailability.DataSource = null;
			dgvAvailability.DataSource = blocks;

			dgvAvailability.Columns["Day"].HeaderText = "Day (availability)";
			dgvAvailability.Columns["Start"].HeaderText = "Start Time";
			dgvAvailability.Columns["End"].HeaderText = "End Time";

			if (dgvAvailability.Columns.Contains("Id"))
			{
				dgvAvailability.Columns["Id"].Visible = false;
			}

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

			dgvCommitments.Columns["Day"].HeaderText = "Day (commitments)";
			dgvCommitments.Columns["Start"].HeaderText = "Start Time";
			dgvCommitments.Columns["End"].HeaderText = "End Time";

			if (dgvCommitments.Columns.Contains("Id"))
			{
				dgvCommitments.Columns["Id"].Visible = false;
			}

			dgvCommitments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvCommitments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvCommitments.MultiSelect = false;
			dgvCommitments.ReadOnly = true;
			dgvCommitments.AllowUserToAddRows = false;
		}

		// -----------------------------
		// Form + tab lifecycle events
		// -----------------------------

		private void Form1_Load(object sender, EventArgs e)
		{
			// Initial load: bring all grids into sync with DB.
			// (Some apps only refresh the active tab; doing all is fine for small datasets.)
			RefreshCoursesGrid();
			RefreshTasksGrid();
			RefreshAvailabilityGrid();
			RefreshCommitmentsGrid();
			RefreshScheduleGrid();
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

			if (tabControl1.SelectedTab == tabPageSchedule)
			{
				RefreshScheduleGrid();
			}
		}

		// -----------------------------
		// Selection helpers
		// -----------------------------

		private Course? GetSelectedCourse()
		{
			// DataBoundItem returns the underlying object bound to the selected row.
			return dgvCourses.CurrentRow?.DataBoundItem as Course;
		}

		private TaskItem? GetSelectedTask()
		{
			return dgvTasks.CurrentRow?.DataBoundItem as TaskItem;
		}

		private Availability? GetSelectedAvailability()
		{
			return dgvAvailability.CurrentRow?.DataBoundItem as Availability;
		}

		private Commitment? GetSelectedCommitment()
		{
			return dgvCommitments.CurrentRow?.DataBoundItem as Commitment;
		}

		// -----------------------------
		// Convenience UX
		// -----------------------------

		private void dgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			// Ignore header clicks (RowIndex < 0 indicates header).
			if (e.RowIndex < 0)
			{
				return;
			}

			// Treat double-click as a shortcut for editing.
			btnEditTask.PerformClick();
		}

		private void RefreshScheduleGrid()
		{
			var blocks = _scheduleBlocks.GetAll();
			var tasks = _tasks.GetAll();
			var courses = _courses.GetAll();

			// Build a display-friendly view
			var display = blocks.Select(b =>
			{
				var task = tasks.FirstOrDefault(t => t.Id == b.TaskId);
				var course = task != null
					? courses.FirstOrDefault(c => c.Id == task.CourseId)
					: null;

				return new
				{
					b.Id,
					Task = task != null
						? $"{task.Title} ({course?.Name ?? "Unknown"})"
						: "Unknown Task",

					Date = b.Date.ToShortDateString(),
					Start = b.Start.ToString(@"hh\:mm"),
					End = b.End.ToString(@"hh\:mm"),
					b.IsCompleted,
					b.IsLocked
				};
			}).ToList();

			dgvSchedule.DataSource = null;
			dgvSchedule.DataSource = display;

			dgvSchedule.Columns["Task"].HeaderText = "Task";
			dgvSchedule.Columns["Date"].HeaderText = "Date";
			dgvSchedule.Columns["Start"].HeaderText = "Start Time";
			dgvSchedule.Columns["End"].HeaderText = "End Time";
			dgvSchedule.Columns["IsCompleted"].HeaderText = "Completed";
			dgvSchedule.Columns["IsLocked"].HeaderText = "Locked";

			// Hide internal Id
			if (dgvSchedule.Columns.Contains("Id"))
			{
				dgvSchedule.Columns["Id"].Visible = false;
			}

			dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvSchedule.MultiSelect = false;
			dgvSchedule.ReadOnly = true;
			dgvSchedule.AllowUserToAddRows = false;
		}

		private void ShowWarnings(List<string> warnings)
		{
			lstWarnings.Items.Clear();

			foreach (var warning in warnings)
			{
				lstWarnings.Items.Add(warning);
			}
		}

		private ScheduleBlock? GetSelectedScheduleBlock()
		{
			if (dgvSchedule.SelectedRows.Count == 0)
			{
				return null;
			}

			DataGridViewRow row = dgvSchedule.SelectedRows[0];

			if (row == null || row.IsNewRow)
			{
				return null;
			}

			if (!dgvSchedule.Columns.Contains("Id"))
			{
				return null;
			}

			object value = row.Cells["Id"].Value;

			if (value == null || value == DBNull.Value)
			{
				return null;
			}

			if (!int.TryParse(value.ToString(), out int id))
			{
				return null;
			}

			return _scheduleBlocks.GetAll().FirstOrDefault(b => b.Id == id);
		}

		private void btnToggleLock_Click(object sender, EventArgs e)
		{
			try
			{
				var selected = GetSelectedScheduleBlock();
				if (selected == null)
				{
					MessageBox.Show("Select a schedule block first.");
					return;
				}

				_scheduleBlocks.SetLocked(selected.Id, !selected.IsLocked);
				RefreshScheduleGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnToggleComplete_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedScheduleBlock();
			if (selected == null)
			{
				MessageBox.Show("Select a schedule block first.");
				return;
			}

			_scheduleBlocks.SetCompleted(selected.Id, !selected.IsCompleted);
			RefreshScheduleGrid();
		}

		private void dgvSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			// Ignore invalid row indexes
			if (e.RowIndex < 0)
			{
				return;
			}

			var row = dgvSchedule.Rows[e.RowIndex];

			// Safely read the two boolean flags from the bound row
			bool isCompleted = row.Cells["IsCompleted"].Value != null && Convert.ToBoolean(row.Cells["IsCompleted"].Value);

			bool isLocked = row.Cells["IsLocked"].Value != null && Convert.ToBoolean(row.Cells["IsLocked"].Value);

			// Reset row style first so colors do not "stick" incorrectly
			row.DefaultCellStyle.BackColor = Color.White;
			row.DefaultCellStyle.ForeColor = Color.Black;

			// Completed blocks take priority visually over locked ones
			if (isCompleted)
			{
				row.DefaultCellStyle.BackColor = Color.LightGreen;
			}
			else if (isLocked)
			{
				row.DefaultCellStyle.BackColor = Color.LightYellow;
			}
		}

		private string BuildReport()
		{
			var courses = _courses.GetAll();
			var tasks = _tasks.GetAll();
			var scheduleBlocks = _scheduleBlocks.GetAll();

			int totalCourses = courses.Count;
			int totalTasks = tasks.Count;
			int completedTasks = tasks.Count(t => t.IsCompleted);
			int incompleteTasks = tasks.Count(t => !t.IsCompleted);
			int overdueTasks = tasks.Count(t =>
			{
				var blocksForTask = scheduleBlocks.Where(b => b.TaskId == t.Id).ToList();

				return t.Deadline.Date < DateTime.Today &&
					   (blocksForTask.Count == 0 || blocksForTask.Any(b => !b.IsCompleted));
			});
			int tasksDueSoon = tasks.Count(t =>
			{
				var blocksForTask = scheduleBlocks.Where(b => b.TaskId == t.Id).ToList();

				return t.Deadline.Date >= DateTime.Today &&
					   t.Deadline.Date <= DateTime.Today.AddDays(7) &&
					   (blocksForTask.Count == 0 || blocksForTask.Any(b => !b.IsCompleted));
			});

			int totalBlocks = scheduleBlocks.Count;
			int completedBlocks = scheduleBlocks.Count(b => b.IsCompleted);
			int lockedBlocks = scheduleBlocks.Count(b => b.IsLocked);

			double totalScheduledHours = scheduleBlocks.Sum(b => (b.End - b.Start).TotalHours);

			var upcomingTasks = tasks
				.Where(t =>
				{
					var blocksForTask = scheduleBlocks.Where(b => b.TaskId == t.Id).ToList();

					return t.Deadline.Date >= DateTime.Today &&
						(blocksForTask.Count == 0 || blocksForTask.Any(b => !b.IsCompleted));
				})
				.OrderBy(t => t.Deadline)
				.Take(5)
				.ToList();

			var sb = new StringBuilder();

			sb.AppendLine("STUDENT PLANNER REPORT");
			sb.AppendLine("======================");
			sb.AppendLine();
			sb.AppendLine($"Courses: {totalCourses}");
			sb.AppendLine($"Tasks: {totalTasks}");
			//sb.AppendLine($"Completed Tasks: {completedTasks}");
			//sb.AppendLine($"Incomplete Tasks: {incompleteTasks}");
			sb.AppendLine($"Overdue Tasks: {overdueTasks}");
			sb.AppendLine($"Tasks Due in Next 7 Days: {tasksDueSoon}");
			sb.AppendLine();
			sb.AppendLine($"Schedule Blocks: {totalBlocks}");
			sb.AppendLine($"Completed Schedule Blocks: {completedBlocks}");
			sb.AppendLine($"Locked Schedule Blocks: {lockedBlocks}");
			sb.AppendLine($"Total Scheduled Hours: {totalScheduledHours:F2}");
			sb.AppendLine();
			sb.AppendLine("Upcoming Deadlines:");

			if (upcomingTasks.Count == 0)
			{
				sb.AppendLine("- None");
			}
			else
			{
				foreach (var task in upcomingTasks)
				{
					sb.AppendLine($"- {task.Title} - {task.Deadline:yyyy-MM-dd}");
				}
			}

			return sb.ToString();
		}

		private void btnGenerateReport_Click(object sender, EventArgs e)
		{
			rtbReport.Text = BuildReport();
		}

		private void btnExportReport_Click(object sender, EventArgs e)
		{
			string report = BuildReport();

			using SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			saveDialog.Title = "Export Report";
			saveDialog.FileName = "StudentPlannerReport.txt";

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllText(saveDialog.FileName, report);
				MessageBox.Show("Report exported successfully.");
			}
		}
	}
}
