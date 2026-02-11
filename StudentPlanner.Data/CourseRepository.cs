using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Data
{
	public class CourseRepository : ICourseRepository
	{
		private readonly string _connectionString;

		public CourseRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		private SqliteConnection OpenConnection()
		{
			var conn = new SqliteConnection(_connectionString);
			conn.Open();

			// Enforce foreign keys (SQLite requires this per connection)
			using var pragma = conn.CreateCommand();
			pragma.CommandText = "PRAGMA foreign_keys = ON;";
			pragma.ExecuteNonQuery();

			return conn;
		}

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

		public int Add(string name)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();
			cmd.CommandText = @"
INSERT INTO Courses (Name)
VALUES ($name);
SELECT last_insert_rowid();
";
			cmd.Parameters.AddWithValue("$name", name);

			// Returns the new Id
			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

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

			int rows = cmd.ExecuteNonQuery();
			return rows == 1;
		}

		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();
			cmd.CommandText = @"
DELETE FROM Courses
WHERE Id = $id;
";
			cmd.Parameters.AddWithValue("$id", id);

			int rows = cmd.ExecuteNonQuery();
			return rows == 1;
		}
	}
}
