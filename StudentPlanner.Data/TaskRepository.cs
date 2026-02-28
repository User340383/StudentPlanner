using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StudentPlanner.Data
{
	public class TaskRepository : ITaskRepository
	{
		private readonly string _connectionString;

		public TaskRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		private SqliteConnection OpenConnection()
		{
			var conn = new SqliteConnection(_connectionString);
			conn.Open();

			using var pragma = conn.CreateCommand();
			pragma.CommandText = "PRAGMA foreign_keys = ON;";
			pragma.ExecuteNonQuery();

			return conn;
		}

		private static string ToDbDate(DateTime dt)
			=> dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

		private static DateTime FromDbDate(string text)
			=> DateTime.Parse(text, CultureInfo.InvariantCulture);

		public List<TaskItem> GetAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, CourseId, Title, Deadline, EstimatedHours, Priority, IsCompleted
FROM Tasks
ORDER BY Deadline;
";

			var list = new List<TaskItem>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new TaskItem
				{
					Id = reader.GetInt32(0),
					CourseId = reader.GetInt32(1),
					Title = reader.GetString(2),
					Deadline = FromDbDate(reader.GetString(3)),
					EstimatedHours = reader.GetDouble(4),
					Priority = reader.GetInt32(5),
					IsCompleted = reader.GetInt32(6) == 1
				});
			}

			return list;
		}

		public List<TaskItem> GetByCourse(int courseId)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, CourseId, Title, Deadline, EstimatedHours, Priority, IsCompleted
FROM Tasks
WHERE CourseId = $courseId
ORDER BY Deadline;
";

			cmd.Parameters.AddWithValue("$courseId", courseId);

			var list = new List<TaskItem>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new TaskItem
				{
					Id = reader.GetInt32(0),
					CourseId = reader.GetInt32(1),
					Title = reader.GetString(2),
					Deadline = FromDbDate(reader.GetString(3)),
					EstimatedHours = reader.GetDouble(4),
					Priority = reader.GetInt32(5),
					IsCompleted = reader.GetInt32(6) == 1
				});
			}

			return list;
		}

		public int Add(TaskItem task)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO Tasks (CourseId, Title, Deadline, EstimatedHours, Priority, IsCompleted)
VALUES ($courseId, $title, $deadline, $hours, $priority, $completed);
SELECT last_insert_rowid();
";

			cmd.Parameters.AddWithValue("$courseId", task.CourseId);
			cmd.Parameters.AddWithValue("$title", task.Title);
			cmd.Parameters.AddWithValue("$deadline", ToDbDate(task.Deadline));
			cmd.Parameters.AddWithValue("$hours", task.EstimatedHours);
			cmd.Parameters.AddWithValue("$priority", task.Priority);
			cmd.Parameters.AddWithValue("$completed", task.IsCompleted ? 1 : 0);

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		public bool Update(TaskItem task)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
UPDATE Tasks
SET CourseId = $courseId,
    Title = $title,
    Deadline = $deadline,
    EstimatedHours = $hours,
    Priority = $priority,
    IsCompleted = $completed
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$courseId", task.CourseId);
			cmd.Parameters.AddWithValue("$title", task.Title);
			cmd.Parameters.AddWithValue("$deadline", ToDbDate(task.Deadline));
			cmd.Parameters.AddWithValue("$hours", task.EstimatedHours);
			cmd.Parameters.AddWithValue("$priority", task.Priority);
			cmd.Parameters.AddWithValue("$completed", task.IsCompleted ? 1 : 0);
			cmd.Parameters.AddWithValue("$id", task.Id);

			return cmd.ExecuteNonQuery() == 1;
		}

		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM Tasks
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}

		public bool SetCompleted(int id, bool isCompleted)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
UPDATE Tasks
SET IsCompleted = $completed
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$completed", isCompleted ? 1 : 0);
			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}
	}
}
