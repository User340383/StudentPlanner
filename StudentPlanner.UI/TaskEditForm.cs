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
	public partial class TaskEditForm : Form
	{
		private readonly List<Course> _courses;

		// Result after OK
		public TaskItem ResultTask { get; private set; } = new TaskItem();

		public TaskEditForm(List<Course> courses, string title, TaskItem? existing = null)
		{
			InitializeComponent();

			_courses = courses;

			Text = title;

			cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbCourse.DataSource = _courses;
			cmbCourse.DisplayMember = "Name";
			cmbCourse.ValueMember = "Id";

			// Defaults for "Add"
			dtpDeadline.Value = DateTime.Now.AddDays(1);
			nudHours.Value = 1;
			nudPriority.Value = 3;

			// If editing, prefill fields
			if (existing != null)
			{
				SelectCourse(existing.CourseId);
				txtTitle.Text = existing.Title;
				dtpDeadline.Value = existing.Deadline;
				nudHours.Value = (decimal)existing.EstimatedHours;
				nudPriority.Value = existing.Priority;
			}

			btnOk.Click += BtnOk_Click;
		}

		private void SelectCourse(int courseId)
		{
			// Set SelectedValue safely
			if (_courses.Any(c => c.Id == courseId))
				cmbCourse.SelectedValue = courseId;
			else if (_courses.Count > 0)
				cmbCourse.SelectedIndex = 0;
		}

		private void BtnOk_Click(object? sender, EventArgs e)
		{
			string title = txtTitle.Text.Trim();
			if (string.IsNullOrWhiteSpace(title))
			{
				MessageBox.Show("Task title cannot be empty.");
				DialogResult = DialogResult.None; // prevents dialog from closing
				return;
			}

			if (cmbCourse.SelectedValue == null)
			{
				MessageBox.Show("Please choose a course.");
				DialogResult = DialogResult.None;
				return;
			}

			int courseId = (int)cmbCourse.SelectedValue;

			ResultTask = new TaskItem
			{
				CourseId = courseId,
				Title = title,
				Deadline = dtpDeadline.Value,
				EstimatedHours = (double)nudHours.Value,
				Priority = (int)nudPriority.Value,
			};
		}
	}
}
