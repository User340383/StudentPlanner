using StudentPlanner.Core;

namespace StudentPlanner.UI
{
	partial class TaskEditForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			cmbCourse = new ComboBox();
			label2 = new Label();
			txtTitle = new TextBox();
			label3 = new Label();
			dtpDeadline = new DateTimePicker();
			label4 = new Label();
			nudHours = new NumericUpDown();
			label5 = new Label();
			nudPriority = new NumericUpDown();
			btnOk = new Button();
			btnCancel = new Button();
			((System.ComponentModel.ISupportInitialize)nudHours).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudPriority).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(60, 38);
			label1.Name = "label1";
			label1.Size = new Size(54, 20);
			label1.TabIndex = 0;
			label1.Text = "Course";
			// 
			// cmbCourse
			// 
			cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbCourse.FormattingEnabled = true;
			cmbCourse.Location = new Point(134, 35);
			cmbCourse.Name = "cmbCourse";
			cmbCourse.Size = new Size(262, 28);
			cmbCourse.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(60, 84);
			label2.Name = "label2";
			label2.Size = new Size(38, 20);
			label2.TabIndex = 2;
			label2.Text = "Title";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(135, 81);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(261, 27);
			txtTitle.TabIndex = 3;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(60, 130);
			label3.Name = "label3";
			label3.Size = new Size(69, 20);
			label3.TabIndex = 4;
			label3.Text = "Deadline";
			// 
			// dtpDeadline
			// 
			dtpDeadline.CustomFormat = "yyyy-MM-dd HH:mm";
			dtpDeadline.Format = DateTimePickerFormat.Custom;
			dtpDeadline.Location = new Point(135, 125);
			dtpDeadline.Name = "dtpDeadline";
			dtpDeadline.Size = new Size(261, 27);
			dtpDeadline.TabIndex = 5;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(60, 171);
			label4.Name = "label4";
			label4.Size = new Size(48, 20);
			label4.TabIndex = 6;
			label4.Text = "Hours";
			// 
			// nudHours
			// 
			nudHours.DecimalPlaces = 1;
			nudHours.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
			nudHours.Location = new Point(135, 169);
			nudHours.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
			nudHours.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
			nudHours.Name = "nudHours";
			nudHours.Size = new Size(99, 27);
			nudHours.TabIndex = 7;
			nudHours.Value = new decimal(new int[] { 5, 0, 0, 65536 });
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(60, 214);
			label5.Name = "label5";
			label5.Size = new Size(56, 20);
			label5.TabIndex = 8;
			label5.Text = "Priority";
			// 
			// nudPriority
			// 
			nudPriority.Location = new Point(135, 214);
			nudPriority.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
			nudPriority.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudPriority.Name = "nudPriority";
			nudPriority.Size = new Size(99, 27);
			nudPriority.TabIndex = 9;
			nudPriority.Value = new decimal(new int[] { 3, 0, 0, 0 });
			// 
			// btnOk
			// 
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(60, 286);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(94, 29);
			btnOk.TabIndex = 10;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(191, 286);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(94, 29);
			btnCancel.TabIndex = 11;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// TaskEditForm
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(800, 450);
			Controls.Add(btnCancel);
			Controls.Add(btnOk);
			Controls.Add(nudPriority);
			Controls.Add(label5);
			Controls.Add(nudHours);
			Controls.Add(label4);
			Controls.Add(dtpDeadline);
			Controls.Add(label3);
			Controls.Add(txtTitle);
			Controls.Add(label2);
			Controls.Add(cmbCourse);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "TaskEditForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "TaskEditForm";
			((System.ComponentModel.ISupportInitialize)nudHours).EndInit();
			((System.ComponentModel.ISupportInitialize)nudPriority).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private ComboBox cmbCourse;
		private Label label2;
		private TextBox txtTitle;
		private Label label3;
		private DateTimePicker dtpDeadline;
		private Label label4;
		private NumericUpDown nudHours;
		private Label label5;
		private NumericUpDown nudPriority;
		private Button btnOk;
		private Button btnCancel;
	}
}
