using Microsoft.Data.Sqlite;
using StudentPlanner.Core;

namespace StudentPlanner.Data
{
	// Concrete implementation of ICourseRepository.
	// Responsible for persistence and retrieval of Course entities.
	// This class maps between the Course domain model and the SQLite table.
	public class CourseRepository : ICourseRepository
	{
		private readonly string _connectionString;

		// Allows optional injection of a custom connection string (useful for testing).
		public CourseRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		// Opens a SQLite connection and enables foreign key enforcement.
		// SQLite requires PRAGMA foreign_keys = ON per connection.
		private SqliteConnection OpenConnection()
		{
			var conn = new SqliteConnection(_connectionString);
			conn.Open();

			using var pragma = conn.CreateCommand();
			pragma.CommandText = "PRAGMA foreign_keys = ON;";
			pragma.ExecuteNonQuery();

			return conn;
		}

		// Returns all courses ordered alphabetically.
		public List<Course> GetAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, Name
FROM Courses
ORDER BY Name;
";

			var results = new List<Course>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				results.Add(new Course
				{
					Id = reader.GetInt32(0),
					Name = reader.GetString(1)
				});
			}

			return results;
		}

		// Inserts a new course and returns the generated primary key.
		// The UNIQUE constraint on Name prevents duplicates.
		public int Add(string name)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO Courses (Name)
VALUES ($name);
SELECT last_insert_rowid();
";

			// Parameterized query prevents SQL injection.
			cmd.Parameters.AddWithValue("$name", name);

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		// Updates the name of an existing course.
		// Returns true if exactly one row was modified.
		public bool Update(int id, string newName)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
UPDATE Courses
SET Name = $name
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$name", newName);
			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}

		// Deletes a course by primary key.
		// Due to ON DELETE CASCADE in the Tasks table,
		// deleting a course will automatically remove its associated tasks.
		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM Courses
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}
	}
}
