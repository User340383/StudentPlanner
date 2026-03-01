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
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
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
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(117, 142);
			label1.Name = "label1";
			label1.Size = new Size(46, 20);
			label1.TabIndex = 6;
			label1.Text = "From:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(135, 212);
			label2.Name = "label2";
			label2.Size = new Size(28, 20);
			label2.TabIndex = 7;
			label2.Text = "To:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(75, 272);
			label3.Name = "label3";
			label3.Size = new Size(88, 20);
			label3.TabIndex = 8;
			label3.Text = "Description:";
			// 
			// CommitmentEditForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
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
		private Label label1;
		private Label label2;
		private Label label3;
	}
}