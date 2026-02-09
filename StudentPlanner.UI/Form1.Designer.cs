namespace StudentPlanner.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tabControl1 = new TabControl();
			tabPageCourses = new TabPage();
			pnlCoursesTop = new Panel();
			btnAddCourse = new Button();
			btnDeleteCourse = new Button();
			btnEditCourse = new Button();
			dgvCourses = new DataGridView();
			tabPageTasks = new TabPage();
			pnlTasksTop = new Panel();
			btnDeleteTask = new Button();
			btnEditTask = new Button();
			btnAddTask = new Button();
			dgvTasks = new DataGridView();
			tabPageAvailability = new TabPage();
			pnlAvailabilityTop = new Panel();
			btnDeleteCommitment = new Button();
			btnAddCommitment = new Button();
			btnDeleteAvailability = new Button();
			btnAddAvailability = new Button();
			dgvCommitments = new DataGridView();
			dgvAvailability = new DataGridView();
			tabPageSchedule = new TabPage();
			pnlScheduleTop = new Panel();
			lstWarnings = new ListBox();
			dgvSchedule = new DataGridView();
			btnRegenerateSchedule = new Button();
			btnGenerateSchedule = new Button();
			tlpAvailability = new TableLayoutPanel();
			tlpSchedule = new TableLayoutPanel();
			tabControl1.SuspendLayout();
			tabPageCourses.SuspendLayout();
			pnlCoursesTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
			tabPageTasks.SuspendLayout();
			pnlTasksTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
			tabPageAvailability.SuspendLayout();
			pnlAvailabilityTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvCommitments).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvAvailability).BeginInit();
			tabPageSchedule.SuspendLayout();
			pnlScheduleTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
			tlpAvailability.SuspendLayout();
			tlpSchedule.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(tabPageCourses);
			tabControl1.Controls.Add(tabPageTasks);
			tabControl1.Controls.Add(tabPageAvailability);
			tabControl1.Controls.Add(tabPageSchedule);
			tabControl1.Dock = DockStyle.Fill;
			tabControl1.Location = new Point(0, 0);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(800, 450);
			tabControl1.TabIndex = 0;
			// 
			// tabPageCourses
			// 
			tabPageCourses.Controls.Add(pnlCoursesTop);
			tabPageCourses.Controls.Add(dgvCourses);
			tabPageCourses.Location = new Point(4, 29);
			tabPageCourses.Name = "tabPageCourses";
			tabPageCourses.Padding = new Padding(3);
			tabPageCourses.Size = new Size(792, 417);
			tabPageCourses.TabIndex = 0;
			tabPageCourses.Text = "Courses";
			tabPageCourses.UseVisualStyleBackColor = true;
			// 
			// pnlCoursesTop
			// 
			pnlCoursesTop.Controls.Add(btnAddCourse);
			pnlCoursesTop.Controls.Add(btnDeleteCourse);
			pnlCoursesTop.Controls.Add(btnEditCourse);
			pnlCoursesTop.Dock = DockStyle.Top;
			pnlCoursesTop.Location = new Point(3, 3);
			pnlCoursesTop.Name = "pnlCoursesTop";
			pnlCoursesTop.Padding = new Padding(5);
			pnlCoursesTop.Size = new Size(786, 55);
			pnlCoursesTop.TabIndex = 4;
			// 
			// btnAddCourse
			// 
			btnAddCourse.AutoSize = true;
			btnAddCourse.Location = new Point(8, 11);
			btnAddCourse.Name = "btnAddCourse";
			btnAddCourse.Size = new Size(113, 30);
			btnAddCourse.TabIndex = 1;
			btnAddCourse.Text = "Add course";
			btnAddCourse.UseVisualStyleBackColor = true;
			btnAddCourse.Click += btnAddCourse_Click;
			// 
			// btnDeleteCourse
			// 
			btnDeleteCourse.AutoSize = true;
			btnDeleteCourse.Location = new Point(246, 11);
			btnDeleteCourse.Name = "btnDeleteCourse";
			btnDeleteCourse.Size = new Size(113, 30);
			btnDeleteCourse.TabIndex = 3;
			btnDeleteCourse.Text = "Delete course";
			btnDeleteCourse.UseVisualStyleBackColor = true;
			btnDeleteCourse.Click += btnDeleteCourse_Click;
			// 
			// btnEditCourse
			// 
			btnEditCourse.AutoSize = true;
			btnEditCourse.Location = new Point(127, 11);
			btnEditCourse.Name = "btnEditCourse";
			btnEditCourse.Size = new Size(113, 30);
			btnEditCourse.TabIndex = 2;
			btnEditCourse.Text = "Edit course";
			btnEditCourse.UseVisualStyleBackColor = true;
			btnEditCourse.Click += btnEditCourse_Click;
			// 
			// dgvCourses
			// 
			dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvCourses.Dock = DockStyle.Fill;
			dgvCourses.Location = new Point(3, 3);
			dgvCourses.Name = "dgvCourses";
			dgvCourses.RowHeadersWidth = 51;
			dgvCourses.Size = new Size(786, 411);
			dgvCourses.TabIndex = 0;
			// 
			// tabPageTasks
			// 
			tabPageTasks.Controls.Add(pnlTasksTop);
			tabPageTasks.Controls.Add(dgvTasks);
			tabPageTasks.Location = new Point(4, 29);
			tabPageTasks.Name = "tabPageTasks";
			tabPageTasks.Padding = new Padding(3);
			tabPageTasks.Size = new Size(792, 417);
			tabPageTasks.TabIndex = 1;
			tabPageTasks.Text = "Tasks";
			tabPageTasks.UseVisualStyleBackColor = true;
			// 
			// pnlTasksTop
			// 
			pnlTasksTop.Controls.Add(btnAddTask);
			pnlTasksTop.Controls.Add(btnDeleteTask);
			pnlTasksTop.Controls.Add(btnEditTask);
			pnlTasksTop.Dock = DockStyle.Top;
			pnlTasksTop.Location = new Point(3, 3);
			pnlTasksTop.Name = "pnlTasksTop";
			pnlTasksTop.Padding = new Padding(5);
			pnlTasksTop.Size = new Size(786, 55);
			pnlTasksTop.TabIndex = 4;
			// 
			// btnDeleteTask
			// 
			btnDeleteTask.AutoSize = true;
			btnDeleteTask.Location = new Point(246, 11);
			btnDeleteTask.Name = "btnDeleteTask";
			btnDeleteTask.Size = new Size(113, 30);
			btnDeleteTask.TabIndex = 3;
			btnDeleteTask.Text = "Delete task";
			btnDeleteTask.UseVisualStyleBackColor = true;
			btnDeleteTask.Click += btnDeleteTask_Click;
			// 
			// btnEditTask
			// 
			btnEditTask.AutoSize = true;
			btnEditTask.Location = new Point(127, 11);
			btnEditTask.Name = "btnEditTask";
			btnEditTask.Size = new Size(113, 30);
			btnEditTask.TabIndex = 2;
			btnEditTask.Text = "Edit task";
			btnEditTask.UseVisualStyleBackColor = true;
			btnEditTask.Click += btnEditTask_Click;
			// 
			// btnAddTask
			// 
			btnAddTask.AutoSize = true;
			btnAddTask.Location = new Point(8, 11);
			btnAddTask.Name = "btnAddTask";
			btnAddTask.Size = new Size(113, 30);
			btnAddTask.TabIndex = 1;
			btnAddTask.Text = "Add task";
			btnAddTask.UseVisualStyleBackColor = true;
			btnAddTask.Click += btnAddTask_Click;
			// 
			// dgvTasks
			// 
			dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvTasks.Dock = DockStyle.Fill;
			dgvTasks.Location = new Point(3, 3);
			dgvTasks.Name = "dgvTasks";
			dgvTasks.RowHeadersWidth = 51;
			dgvTasks.Size = new Size(786, 411);
			dgvTasks.TabIndex = 0;
			// 
			// tabPageAvailability
			// 
			tabPageAvailability.Controls.Add(tlpAvailability);
			tabPageAvailability.Location = new Point(4, 29);
			tabPageAvailability.Name = "tabPageAvailability";
			tabPageAvailability.Size = new Size(792, 417);
			tabPageAvailability.TabIndex = 2;
			tabPageAvailability.Text = "Availability";
			tabPageAvailability.UseVisualStyleBackColor = true;
			// 
			// pnlAvailabilityTop
			// 
			pnlAvailabilityTop.Controls.Add(btnAddAvailability);
			pnlAvailabilityTop.Controls.Add(btnDeleteAvailability);
			pnlAvailabilityTop.Controls.Add(btnDeleteCommitment);
			pnlAvailabilityTop.Controls.Add(btnAddCommitment);
			pnlAvailabilityTop.Dock = DockStyle.Fill;
			pnlAvailabilityTop.Location = new Point(3, 3);
			pnlAvailabilityTop.Name = "pnlAvailabilityTop";
			pnlAvailabilityTop.Padding = new Padding(5);
			pnlAvailabilityTop.Size = new Size(786, 49);
			pnlAvailabilityTop.TabIndex = 6;
			// 
			// btnDeleteCommitment
			// 
			btnDeleteCommitment.AutoSize = true;
			btnDeleteCommitment.Location = new Point(565, 8);
			btnDeleteCommitment.Name = "btnDeleteCommitment";
			btnDeleteCommitment.Size = new Size(156, 30);
			btnDeleteCommitment.TabIndex = 5;
			btnDeleteCommitment.Text = "Delete commitment";
			btnDeleteCommitment.UseVisualStyleBackColor = true;
			btnDeleteCommitment.Click += btnDeleteCommitment_Click;
			// 
			// btnAddCommitment
			// 
			btnAddCommitment.AutoSize = true;
			btnAddCommitment.Location = new Point(380, 8);
			btnAddCommitment.Name = "btnAddCommitment";
			btnAddCommitment.Size = new Size(138, 30);
			btnAddCommitment.TabIndex = 4;
			btnAddCommitment.Text = "Add commitment";
			btnAddCommitment.UseVisualStyleBackColor = true;
			btnAddCommitment.Click += btnAddCommitment_Click;
			// 
			// btnDeleteAvailability
			// 
			btnDeleteAvailability.AutoSize = true;
			btnDeleteAvailability.Location = new Point(185, 8);
			btnDeleteAvailability.Name = "btnDeleteAvailability";
			btnDeleteAvailability.Size = new Size(156, 30);
			btnDeleteAvailability.TabIndex = 3;
			btnDeleteAvailability.Text = "Delete Availability";
			btnDeleteAvailability.UseVisualStyleBackColor = true;
			btnDeleteAvailability.Click += btnDeleteAvailability_Click;
			// 
			// btnAddAvailability
			// 
			btnAddAvailability.AutoSize = true;
			btnAddAvailability.Location = new Point(20, 8);
			btnAddAvailability.Name = "btnAddAvailability";
			btnAddAvailability.Size = new Size(138, 30);
			btnAddAvailability.TabIndex = 2;
			btnAddAvailability.Text = "Add availability";
			btnAddAvailability.UseVisualStyleBackColor = true;
			btnAddAvailability.Click += btnAddAvailability_Click;
			// 
			// dgvCommitments
			// 
			dgvCommitments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvCommitments.Dock = DockStyle.Fill;
			dgvCommitments.Location = new Point(3, 239);
			dgvCommitments.Name = "dgvCommitments";
			dgvCommitments.RowHeadersWidth = 51;
			dgvCommitments.Size = new Size(786, 175);
			dgvCommitments.TabIndex = 1;
			// 
			// dgvAvailability
			// 
			dgvAvailability.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvAvailability.Dock = DockStyle.Fill;
			dgvAvailability.Location = new Point(3, 58);
			dgvAvailability.Name = "dgvAvailability";
			dgvAvailability.RowHeadersWidth = 51;
			dgvAvailability.Size = new Size(786, 175);
			dgvAvailability.TabIndex = 0;
			// 
			// tabPageSchedule
			// 
			tabPageSchedule.Controls.Add(tlpSchedule);
			tabPageSchedule.Location = new Point(4, 29);
			tabPageSchedule.Name = "tabPageSchedule";
			tabPageSchedule.Size = new Size(792, 417);
			tabPageSchedule.TabIndex = 3;
			tabPageSchedule.Text = "Schedule";
			tabPageSchedule.UseVisualStyleBackColor = true;
			// 
			// pnlScheduleTop
			// 
			pnlScheduleTop.Controls.Add(btnGenerateSchedule);
			pnlScheduleTop.Controls.Add(btnRegenerateSchedule);
			pnlScheduleTop.Dock = DockStyle.Fill;
			pnlScheduleTop.Location = new Point(3, 3);
			pnlScheduleTop.Name = "pnlScheduleTop";
			pnlScheduleTop.Padding = new Padding(5);
			pnlScheduleTop.Size = new Size(786, 49);
			pnlScheduleTop.TabIndex = 4;
			// 
			// lstWarnings
			// 
			lstWarnings.Dock = DockStyle.Fill;
			lstWarnings.FormattingEnabled = true;
			lstWarnings.Location = new Point(3, 311);
			lstWarnings.Name = "lstWarnings";
			lstWarnings.Size = new Size(786, 103);
			lstWarnings.TabIndex = 3;
			// 
			// dgvSchedule
			// 
			dgvSchedule.AllowUserToAddRows = false;
			dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvSchedule.Dock = DockStyle.Fill;
			dgvSchedule.Location = new Point(3, 58);
			dgvSchedule.Name = "dgvSchedule";
			dgvSchedule.ReadOnly = true;
			dgvSchedule.RowHeadersWidth = 51;
			dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvSchedule.Size = new Size(786, 247);
			dgvSchedule.TabIndex = 2;
			// 
			// btnRegenerateSchedule
			// 
			btnRegenerateSchedule.AutoSize = true;
			btnRegenerateSchedule.Location = new Point(155, 8);
			btnRegenerateSchedule.Name = "btnRegenerateSchedule";
			btnRegenerateSchedule.Size = new Size(207, 30);
			btnRegenerateSchedule.TabIndex = 1;
			btnRegenerateSchedule.Text = "Regenerate schedule";
			btnRegenerateSchedule.UseVisualStyleBackColor = true;
			btnRegenerateSchedule.Click += btnRegenerateSchedule_Click;
			// 
			// btnGenerateSchedule
			// 
			btnGenerateSchedule.AutoSize = true;
			btnGenerateSchedule.Location = new Point(8, 8);
			btnGenerateSchedule.Name = "btnGenerateSchedule";
			btnGenerateSchedule.Size = new Size(141, 30);
			btnGenerateSchedule.TabIndex = 0;
			btnGenerateSchedule.Text = "Generate schedule";
			btnGenerateSchedule.UseVisualStyleBackColor = true;
			btnGenerateSchedule.Click += btnGenerateSchedule_Click;
			// 
			// tlpAvailability
			// 
			tlpAvailability.ColumnCount = 1;
			tlpAvailability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlpAvailability.Controls.Add(pnlAvailabilityTop, 0, 0);
			tlpAvailability.Controls.Add(dgvCommitments, 0, 2);
			tlpAvailability.Controls.Add(dgvAvailability, 0, 1);
			tlpAvailability.Dock = DockStyle.Fill;
			tlpAvailability.Location = new Point(0, 0);
			tlpAvailability.Name = "tlpAvailability";
			tlpAvailability.RowCount = 3;
			tlpAvailability.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
			tlpAvailability.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpAvailability.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpAvailability.Size = new Size(792, 417);
			tlpAvailability.TabIndex = 7;
			// 
			// tlpSchedule
			// 
			tlpSchedule.ColumnCount = 1;
			tlpSchedule.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlpSchedule.Controls.Add(pnlScheduleTop, 0, 0);
			tlpSchedule.Controls.Add(lstWarnings, 0, 2);
			tlpSchedule.Controls.Add(dgvSchedule, 0, 1);
			tlpSchedule.Dock = DockStyle.Fill;
			tlpSchedule.Location = new Point(0, 0);
			tlpSchedule.Name = "tlpSchedule";
			tlpSchedule.RowCount = 3;
			tlpSchedule.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
			tlpSchedule.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
			tlpSchedule.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
			tlpSchedule.Size = new Size(792, 417);
			tlpSchedule.TabIndex = 5;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(tabControl1);
			Name = "Form1";
			Text = "Form1";
			tabControl1.ResumeLayout(false);
			tabPageCourses.ResumeLayout(false);
			pnlCoursesTop.ResumeLayout(false);
			pnlCoursesTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
			tabPageTasks.ResumeLayout(false);
			pnlTasksTop.ResumeLayout(false);
			pnlTasksTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
			tabPageAvailability.ResumeLayout(false);
			pnlAvailabilityTop.ResumeLayout(false);
			pnlAvailabilityTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvCommitments).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvAvailability).EndInit();
			tabPageSchedule.ResumeLayout(false);
			pnlScheduleTop.ResumeLayout(false);
			pnlScheduleTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
			tlpAvailability.ResumeLayout(false);
			tlpSchedule.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TabControl tabControl1;
		private TabPage tabPageCourses;
		private TabPage tabPageTasks;
		private TabPage tabPageAvailability;
		private TabPage tabPageSchedule;
		private Button btnDeleteCourse;
		private Button btnEditCourse;
		private Button btnAddCourse;
		private DataGridView dgvCourses;
		private Button btnDeleteTask;
		private Button btnEditTask;
		private Button btnAddTask;
		private DataGridView dgvTasks;
		private Button btnDeleteCommitment;
		private Button btnAddCommitment;
		private Button btnDeleteAvailability;
		private Button btnAddAvailability;
		private DataGridView dgvCommitments;
		private DataGridView dgvAvailability;
		private ListBox lstWarnings;
		private DataGridView dgvSchedule;
		private Button btnRegenerateSchedule;
		private Button btnGenerateSchedule;
		private Panel pnlCoursesTop;
		private Panel pnlTasksTop;
		private Panel pnlAvailabilityTop;
		private Panel pnlScheduleTop;
		private TableLayoutPanel tlpAvailability;
		private TableLayoutPanel tlpSchedule;
	}
}
