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
			tableLayoutPanel1 = new TableLayoutPanel();
			dgvCourses = new DataGridView();
			pnlCoursesTop = new Panel();
			txtCourseName = new TextBox();
			btnAddCourse = new Button();
			btnDeleteCourse = new Button();
			btnEditCourse = new Button();
			tabPageTasks = new TabPage();
			tableLayoutPanel2 = new TableLayoutPanel();
			pnlTasksTop = new Panel();
			btnAddTask = new Button();
			btnDeleteTask = new Button();
			btnEditTask = new Button();
			dgvTasks = new DataGridView();
			tabPageAvailability = new TabPage();
			tlpAvailability = new TableLayoutPanel();
			pnlAvailabilityTop = new Panel();
			btnAddAvailability = new Button();
			btnDeleteAvailability = new Button();
			btnDeleteCommitment = new Button();
			btnAddCommitment = new Button();
			dgvCommitments = new DataGridView();
			dgvAvailability = new DataGridView();
			tabPageSchedule = new TabPage();
			tlpSchedule = new TableLayoutPanel();
			pnlScheduleTop = new Panel();
			btnToggleComplete = new Button();
			btnToggleLock = new Button();
			btnGenerateSchedule = new Button();
			btnRegenerateSchedule = new Button();
			lstWarnings = new ListBox();
			dgvSchedule = new DataGridView();
			tabPageReports = new TabPage();
			btnExportReport = new Button();
			btnGenerateReport = new Button();
			rtbReport = new RichTextBox();
			tabControl1.SuspendLayout();
			tabPageCourses.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
			pnlCoursesTop.SuspendLayout();
			tabPageTasks.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			pnlTasksTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
			tabPageAvailability.SuspendLayout();
			tlpAvailability.SuspendLayout();
			pnlAvailabilityTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvCommitments).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvAvailability).BeginInit();
			tabPageSchedule.SuspendLayout();
			tlpSchedule.SuspendLayout();
			pnlScheduleTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
			tabPageReports.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(tabPageCourses);
			tabControl1.Controls.Add(tabPageTasks);
			tabControl1.Controls.Add(tabPageAvailability);
			tabControl1.Controls.Add(tabPageSchedule);
			tabControl1.Controls.Add(tabPageReports);
			tabControl1.Dock = DockStyle.Fill;
			tabControl1.Location = new Point(0, 0);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(800, 450);
			tabControl1.TabIndex = 0;
			tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
			// 
			// tabPageCourses
			// 
			tabPageCourses.Controls.Add(tableLayoutPanel1);
			tabPageCourses.Location = new Point(4, 29);
			tabPageCourses.Name = "tabPageCourses";
			tabPageCourses.Padding = new Padding(3);
			tabPageCourses.Size = new Size(792, 417);
			tabPageCourses.TabIndex = 0;
			tabPageCourses.Text = "Courses";
			tabPageCourses.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(dgvCourses, 0, 1);
			tableLayoutPanel1.Controls.Add(pnlCoursesTop, 0, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(786, 411);
			tableLayoutPanel1.TabIndex = 5;
			// 
			// dgvCourses
			// 
			dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvCourses.Dock = DockStyle.Fill;
			dgvCourses.Location = new Point(3, 58);
			dgvCourses.Name = "dgvCourses";
			dgvCourses.RowHeadersWidth = 51;
			dgvCourses.Size = new Size(780, 350);
			dgvCourses.TabIndex = 0;
			// 
			// pnlCoursesTop
			// 
			pnlCoursesTop.Controls.Add(txtCourseName);
			pnlCoursesTop.Controls.Add(btnAddCourse);
			pnlCoursesTop.Controls.Add(btnDeleteCourse);
			pnlCoursesTop.Controls.Add(btnEditCourse);
			pnlCoursesTop.Dock = DockStyle.Fill;
			pnlCoursesTop.Location = new Point(3, 3);
			pnlCoursesTop.Name = "pnlCoursesTop";
			pnlCoursesTop.Padding = new Padding(5);
			pnlCoursesTop.Size = new Size(780, 49);
			pnlCoursesTop.TabIndex = 4;
			// 
			// txtCourseName
			// 
			txtCourseName.Location = new Point(365, 13);
			txtCourseName.Name = "txtCourseName";
			txtCourseName.Size = new Size(407, 27);
			txtCourseName.TabIndex = 4;
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
			// tabPageTasks
			// 
			tabPageTasks.Controls.Add(tableLayoutPanel2);
			tabPageTasks.Location = new Point(4, 29);
			tabPageTasks.Name = "tabPageTasks";
			tabPageTasks.Padding = new Padding(3);
			tabPageTasks.Size = new Size(792, 417);
			tabPageTasks.TabIndex = 1;
			tabPageTasks.Text = "Tasks";
			tabPageTasks.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(pnlTasksTop, 0, 0);
			tableLayoutPanel2.Controls.Add(dgvTasks, 0, 1);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new Size(786, 411);
			tableLayoutPanel2.TabIndex = 5;
			// 
			// pnlTasksTop
			// 
			pnlTasksTop.Controls.Add(btnAddTask);
			pnlTasksTop.Controls.Add(btnDeleteTask);
			pnlTasksTop.Controls.Add(btnEditTask);
			pnlTasksTop.Dock = DockStyle.Fill;
			pnlTasksTop.Location = new Point(3, 3);
			pnlTasksTop.Name = "pnlTasksTop";
			pnlTasksTop.Padding = new Padding(5);
			pnlTasksTop.Size = new Size(780, 49);
			pnlTasksTop.TabIndex = 4;
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
			// dgvTasks
			// 
			dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvTasks.Dock = DockStyle.Fill;
			dgvTasks.Location = new Point(3, 58);
			dgvTasks.Name = "dgvTasks";
			dgvTasks.RowHeadersWidth = 51;
			dgvTasks.Size = new Size(780, 350);
			dgvTasks.TabIndex = 0;
			dgvTasks.CellDoubleClick += dgvTasks_CellDoubleClick;
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
			// btnDeleteAvailability
			// 
			btnDeleteAvailability.AutoSize = true;
			btnDeleteAvailability.Location = new Point(185, 8);
			btnDeleteAvailability.Name = "btnDeleteAvailability";
			btnDeleteAvailability.Size = new Size(156, 30);
			btnDeleteAvailability.TabIndex = 3;
			btnDeleteAvailability.Text = "Delete availability";
			btnDeleteAvailability.UseVisualStyleBackColor = true;
			btnDeleteAvailability.Click += btnDeleteAvailability_Click;
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
			// pnlScheduleTop
			// 
			pnlScheduleTop.Controls.Add(btnToggleComplete);
			pnlScheduleTop.Controls.Add(btnToggleLock);
			pnlScheduleTop.Controls.Add(btnGenerateSchedule);
			pnlScheduleTop.Controls.Add(btnRegenerateSchedule);
			pnlScheduleTop.Dock = DockStyle.Fill;
			pnlScheduleTop.Location = new Point(3, 3);
			pnlScheduleTop.Name = "pnlScheduleTop";
			pnlScheduleTop.Padding = new Padding(5);
			pnlScheduleTop.Size = new Size(786, 49);
			pnlScheduleTop.TabIndex = 4;
			// 
			// btnToggleComplete
			// 
			btnToggleComplete.Location = new Point(508, 8);
			btnToggleComplete.Name = "btnToggleComplete";
			btnToggleComplete.Size = new Size(132, 29);
			btnToggleComplete.TabIndex = 3;
			btnToggleComplete.Text = "Toggle complete";
			btnToggleComplete.UseVisualStyleBackColor = true;
			btnToggleComplete.Click += btnToggleComplete_Click;
			// 
			// btnToggleLock
			// 
			btnToggleLock.Location = new Point(646, 8);
			btnToggleLock.Name = "btnToggleLock";
			btnToggleLock.Size = new Size(132, 29);
			btnToggleLock.TabIndex = 2;
			btnToggleLock.Text = "Toggle lock";
			btnToggleLock.UseVisualStyleBackColor = true;
			btnToggleLock.Click += btnToggleLock_Click;
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
			dgvSchedule.CellFormatting += dgvSchedule_CellFormatting;
			// 
			// tabPageReports
			// 
			tabPageReports.Controls.Add(btnExportReport);
			tabPageReports.Controls.Add(btnGenerateReport);
			tabPageReports.Controls.Add(rtbReport);
			tabPageReports.Location = new Point(4, 29);
			tabPageReports.Name = "tabPageReports";
			tabPageReports.Padding = new Padding(3);
			tabPageReports.Size = new Size(792, 417);
			tabPageReports.TabIndex = 4;
			tabPageReports.Text = "Reports";
			tabPageReports.UseVisualStyleBackColor = true;
			// 
			// btnExportReport
			// 
			btnExportReport.Location = new Point(414, 350);
			btnExportReport.Name = "btnExportReport";
			btnExportReport.Size = new Size(164, 29);
			btnExportReport.TabIndex = 2;
			btnExportReport.Text = "Export report";
			btnExportReport.UseVisualStyleBackColor = true;
			btnExportReport.Click += btnExportReport_Click;
			// 
			// btnGenerateReport
			// 
			btnGenerateReport.Location = new Point(216, 350);
			btnGenerateReport.Name = "btnGenerateReport";
			btnGenerateReport.Size = new Size(164, 29);
			btnGenerateReport.TabIndex = 1;
			btnGenerateReport.Text = "Generate report";
			btnGenerateReport.UseVisualStyleBackColor = true;
			btnGenerateReport.Click += btnGenerateReport_Click;
			// 
			// rtbReport
			// 
			rtbReport.Location = new Point(39, 39);
			rtbReport.Name = "rtbReport";
			rtbReport.ReadOnly = true;
			rtbReport.Size = new Size(710, 268);
			rtbReport.TabIndex = 0;
			rtbReport.Text = "";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(tabControl1);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			tabControl1.ResumeLayout(false);
			tabPageCourses.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
			pnlCoursesTop.ResumeLayout(false);
			pnlCoursesTop.PerformLayout();
			tabPageTasks.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			pnlTasksTop.ResumeLayout(false);
			pnlTasksTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
			tabPageAvailability.ResumeLayout(false);
			tlpAvailability.ResumeLayout(false);
			pnlAvailabilityTop.ResumeLayout(false);
			pnlAvailabilityTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvCommitments).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvAvailability).EndInit();
			tabPageSchedule.ResumeLayout(false);
			tlpSchedule.ResumeLayout(false);
			pnlScheduleTop.ResumeLayout(false);
			pnlScheduleTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
			tabPageReports.ResumeLayout(false);
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
		private TextBox txtCourseName;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private Button btnToggleComplete;
		private Button btnToggleLock;
		private TabPage tabPageReports;
		private Button btnExportReport;
		private Button btnGenerateReport;
		private RichTextBox rtbReport;
	}
}
