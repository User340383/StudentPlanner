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
	public partial class AvailabilityEditForm : Form
	{
		public Availability Result { get; private set; } = new Availability();

		public AvailabilityEditForm(string title)
		{
			InitializeComponent();
			Text = title;

			cmbDay.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbDay.DataSource = Enum.GetValues(typeof(DayOfWeek));

			dtpStart.Format = DateTimePickerFormat.Time;
			dtpStart.ShowUpDown = true;

			dtpEnd.Format = DateTimePickerFormat.Time;
			dtpEnd.ShowUpDown = true;

			// sensible defaults
			cmbDay.SelectedItem = DayOfWeek.Monday;
			dtpStart.Value = DateTime.Today.AddHours(9);
			dtpEnd.Value = DateTime.Today.AddHours(10);

			btnOk.Click += BtnOk_Click;
		}

		private void BtnOk_Click(object? sender, EventArgs e)
		{
			var day = (DayOfWeek)cmbDay.SelectedItem!;
			TimeSpan start = dtpStart.Value.TimeOfDay;
			TimeSpan end = dtpEnd.Value.TimeOfDay;

			if (end <= start)
			{
				MessageBox.Show("End time must be after start time.");
				DialogResult = DialogResult.None;
				return;
			}

			Result = new Availability
			{
				Day = day,
				Start = start,
				End = end
			};
		}
	}
}
