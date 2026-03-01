using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StudentPlanner.Data
{
	// Concrete implementation of ICommitmentRepository.
	// Persists and retrieves fixed weekly commitments (e.g., work, meetings).
	//
	// DB mapping notes:
	// - DayOfWeek stored as INTEGER (0..6)
	// - StartTime / EndTime stored as TEXT in 24-hour "HH:mm" format
	public class CommitmentRepository : ICommitmentRepository
	{
		private readonly string _connectionString;

		// Allows optional injection of a custom connection string (useful for testing).
		public CommitmentRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		// Opens a SQLite connection and enables foreign key enforcement.
		// SQLite requires PRAGMA foreign_keys = ON for every connection.
		private SqliteConnection OpenConnection()
		{
			var conn = new SqliteConnection(_connectionString);
			conn.Open();

			using var pragma = conn.CreateCommand();
			pragma.CommandText = "PRAGMA foreign_keys = ON;";
			pragma.ExecuteNonQuery();

			return conn;
		}

		// Convert TimeSpan to stable 24-hour format for storage.
		// InvariantCulture prevents localization issues.
		private static string ToDbTime(TimeSpan time)
			=> time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

		// Parse TimeSpan from stored "HH:mm" format.
		private static TimeSpan FromDbTime(string text)
			=> TimeSpan.ParseExact(text, @"hh\:mm", CultureInfo.InvariantCulture);

		// Returns all commitments ordered by day then start time.
		public List<Commitment> GetAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, DayOfWeek, StartTime, EndTime, Description
FROM Commitments
ORDER BY DayOfWeek, StartTime;
";

			var list = new List<Commitment>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new Commitment
				{
					Id = reader.GetInt32(0),
					Day = (DayOfWeek)reader.GetInt32(1),
					Start = FromDbTime(reader.GetString(2)),
					End = FromDbTime(reader.GetString(3)),
					Description = reader.GetString(4)
				});
			}

			return list;
		}

		// Inserts a new commitment and returns the generated primary key.
		// Parameterized queries protect against SQL injection.
		public int Add(Commitment item)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO Commitments (DayOfWeek, StartTime, EndTime, Description)
VALUES ($day, $start, $end, $desc);
SELECT last_insert_rowid();
";

			cmd.Parameters.AddWithValue("$day", (int)item.Day);
			cmd.Parameters.AddWithValue("$start", ToDbTime(item.Start));
			cmd.Parameters.AddWithValue("$end", ToDbTime(item.End));
			cmd.Parameters.AddWithValue("$desc", item.Description);

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		// Deletes a commitment by primary key.
		// Returns true if exactly one row was removed.
		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM Commitments
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}
	}
}
