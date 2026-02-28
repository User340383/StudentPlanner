using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentPlanner.UI
{
	public partial class CommitmentEditForm : Form
	{
		public Commitment Result { get; private set; } = new Commitment();

		public CommitmentEditForm(string title)
		{
			InitializeComponent();
			Text = title;

			cmbDay.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbDay.DataSource = Enum.GetValues(typeof(DayOfWeek));

			dtpStart.Format = DateTimePickerFormat.Time;
			dtpStart.ShowUpDown = true;

			dtpEnd.Format = DateTimePickerFormat.Time;
			dtpEnd.ShowUpDown = true;

			cmbDay.SelectedItem = DayOfWeek.Monday;
			dtpStart.Value = DateTime.Today.AddHours(12);
			dtpEnd.Value = DateTime.Today.AddHours(13);

			btnOk.Click += BtnOk_Click;
		}

		private void BtnOk_Click(object? sender, EventArgs e)
		{
			string desc = txtDescription.Text.Trim();
			if (string.IsNullOrWhiteSpace(desc))
			{
				MessageBox.Show("Description cannot be empty.");
				DialogResult = DialogResult.None;
				return;
			}

			var day = (DayOfWeek)cmbDay.SelectedItem!;
			TimeSpan start = dtpStart.Value.TimeOfDay;
			TimeSpan end = dtpEnd.Value.TimeOfDay;

			if (end <= start)
			{
				MessageBox.Show("End time must be after start time.");
				DialogResult = DialogResult.None;
				return;
			}

			Result = new Commitment
			{
				Day = day,
				Start = start,
				End = end,
				Description = desc
			};
		}
	}
}
