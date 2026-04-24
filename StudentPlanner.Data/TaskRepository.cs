using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System.Globalization;

namespace StudentPlanner.Data
{
	// Concrete implementation of ITaskRepository.
	// Responsible for persistence and retrieval of TaskItem entities.
	// This class translates between domain models and SQLite storage format.
	public class TaskRepository : ITaskRepository
	{
		private readonly string _connectionString;

		// Allows optional injection of a custom connection string (useful for testing).
		public TaskRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		// Opens a SQLite connection and ensures foreign key constraints are enabled.
		// NOTE: SQLite requires PRAGMA foreign_keys = ON per connection.
		private SqliteConnection OpenConnection()
		{
			var conn = new SqliteConnection(_connectionString);
			conn.Open();

			using var pragma = conn.CreateCommand();
			pragma.CommandText = "PRAGMA foreign_keys = ON;";
			pragma.ExecuteNonQuery();

			return conn;
		}

		// Converts DateTime to a stable ISO-like format for SQLite storage.
		// InvariantCulture prevents localization issues.
		private static string ToDbDate(DateTime dt)
			=> dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

		// Parses DateTime from stored SQLite string format.
		private static DateTime FromDbDate(string text)
			=> DateTime.Parse(text, CultureInfo.InvariantCulture);

		// Returns all tasks ordered by deadline (earliest first).
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
					IsCompleted = reader.GetInt32(6) == 1 // SQLite stores bool as INTEGER (0/1)
				});
			}

			return list;
		}

		// Returns all tasks belonging to a specific course.
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

			// Parameterized query prevents SQL injection.
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

		// Inserts a new task and returns the generated primary key.
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

			// SQLite returns rowid as long.
			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		// Updates an existing task. Returns true if exactly one row was modified.
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

		// Deletes a task by primary key.
		// Cascading deletes will remove related ScheduleBlocks automatically.
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

		// Updates only the completion state of a task.
		// Used to toggle task completion without modifying other fields.
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
