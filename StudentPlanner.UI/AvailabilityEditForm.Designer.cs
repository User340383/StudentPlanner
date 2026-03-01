namespace StudentPlanner.UI
{
	partial class AvailabilityEditForm
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
			cmbDay = new ComboBox();
			dtpStart = new DateTimePicker();
			dtpEnd = new DateTimePicker();
			btnOk = new Button();
			btnCancel = new Button();
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// cmbDay
			// 
			cmbDay.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbDay.FormattingEnabled = true;
			cmbDay.Location = new Point(169, 70);
			cmbDay.Name = "cmbDay";
			cmbDay.Size = new Size(250, 28);
			cmbDay.TabIndex = 0;
			// 
			// dtpStart
			// 
			dtpStart.Format = DateTimePickerFormat.Time;
			dtpStart.Location = new Point(169, 137);
			dtpStart.Name = "dtpStart";
			dtpStart.ShowUpDown = true;
			dtpStart.Size = new Size(250, 27);
			dtpStart.TabIndex = 1;
			// 
			// dtpEnd
			// 
			dtpEnd.Format = DateTimePickerFormat.Time;
			dtpEnd.Location = new Point(169, 207);
			dtpEnd.Name = "dtpEnd";
			dtpEnd.ShowUpDown = true;
			dtpEnd.Size = new Size(250, 27);
			dtpEnd.TabIndex = 2;
			// 
			// btnOk
			// 
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(169, 276);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(94, 29);
			btnOk.TabIndex = 3;
			btnOk.Text = "Ok";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(325, 276);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(94, 29);
			btnCancel.TabIndex = 4;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(117, 142);
			label1.Name = "label1";
			label1.Size = new Size(46, 20);
			label1.TabIndex = 5;
			label1.Text = "From:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(135, 212);
			label2.Name = "label2";
			label2.Size = new Size(28, 20);
			label2.TabIndex = 6;
			label2.Text = "To:";
			// 
			// AvailabilityEditForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(btnCancel);
			Controls.Add(btnOk);
			Controls.Add(dtpEnd);
			Controls.Add(dtpStart);
			Controls.Add(cmbDay);
			Name = "AvailabilityEditForm";
			Text = "AvailabilityEditForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cmbDay;
		private DateTimePicker dtpStart;
		private DateTimePicker dtpEnd;
		private Button btnOk;
		private Button btnCancel;
		private Label label1;
		private Label label2;
	}
}