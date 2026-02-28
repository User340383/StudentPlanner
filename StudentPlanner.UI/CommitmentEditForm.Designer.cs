namespace StudentPlanner.UI
{
	partial class CommitmentEditForm
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
			txtDescription = new TextBox();
			btnOk = new Button();
			btnCancel = new Button();
			SuspendLayout();
			// 
			// cmbDay
			// 
			cmbDay.FormattingEnabled = true;
			cmbDay.Location = new Point(169, 70);
			cmbDay.Name = "cmbDay";
			cmbDay.Size = new Size(250, 28);
			cmbDay.TabIndex = 0;
			// 
			// dtpStart
			// 
			dtpStart.Location = new Point(169, 137);
			dtpStart.Name = "dtpStart";
			dtpStart.Size = new Size(250, 27);
			dtpStart.TabIndex = 1;
			// 
			// dtpEnd
			// 
			dtpEnd.Location = new Point(169, 207);
			dtpEnd.Name = "dtpEnd";
			dtpEnd.Size = new Size(250, 27);
			dtpEnd.TabIndex = 2;
			// 
			// txtDescription
			// 
			txtDescription.Location = new Point(169, 269);
			txtDescription.Name = "txtDescription";
			txtDescription.Size = new Size(250, 27);
			txtDescription.TabIndex = 3;
			// 
			// btnOk
			// 
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(169, 326);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(94, 29);
			btnOk.TabIndex = 4;
			btnOk.Text = "Ok";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(325, 326);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(94, 29);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// CommitmentEditForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(btnCancel);
			Controls.Add(btnOk);
			Controls.Add(txtDescription);
			Controls.Add(dtpEnd);
			Controls.Add(dtpStart);
			Controls.Add(cmbDay);
			Name = "CommitmentEditForm";
			Text = "CommitmentEditForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cmbDay;
		private DateTimePicker dtpStart;
		private DateTimePicker dtpEnd;
		private TextBox txtDescription;
		private Button btnOk;
		private Button btnCancel;
	}
}