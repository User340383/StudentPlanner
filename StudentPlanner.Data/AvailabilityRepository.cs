using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StudentPlanner.Data
{
	// Concrete implementation of IAvailabilityRepository.
	// Persists and retrieves weekly availability windows from SQLite.
	//
	// DB mapping notes:
	// - Day is stored as INTEGER (0..6) matching System.DayOfWeek
	// - Start/End are stored as TEXT in 24-hour "HH:mm" format
	public class AvailabilityRepository : IAvailabilityRepository
	{
		private readonly string _connectionString;

		// Allows optional injection of a custom connection string (useful for testing).
		public AvailabilityRepository(string? connectionString = null)
		{
			_connectionString = connectionString ?? DbConfig.GetConnectionString();
		}

		// Opens a SQLite connection and enables FK enforcement.
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

		// Store times in a stable, culture-independent format.
		// Using 24-hour "HH:mm" avoids locale and AM/PM issues.
		private static string ToDbTime(TimeSpan time)
			=> time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

		// Parse times back from "HH:mm".
		private static TimeSpan FromDbTime(string text)
			=> TimeSpan.ParseExact(text, @"hh\:mm", CultureInfo.InvariantCulture);

		// Returns all availability blocks ordered by day then start time.
		public List<Availability> GetAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, DayOfWeek, StartTime, EndTime
FROM Availability
ORDER BY DayOfWeek, StartTime;
";

			var list = new List<Availability>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new Availability
				{
					Id = reader.GetInt32(0),
					Day = (DayOfWeek)reader.GetInt32(1),
					Start = FromDbTime(reader.GetString(2)),
					End = FromDbTime(reader.GetString(3))
				});
			}

			return list;
		}

		// Inserts a new availability window and returns the generated primary key.
		// Validation such as End > Start is typically enforced in the UI layer.
		public int Add(Availability block)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO Availability (DayOfWeek, StartTime, EndTime)
VALUES ($day, $start, $end);
SELECT last_insert_rowid();
";

			// Parameterized query prevents SQL injection.
			cmd.Parameters.AddWithValue("$day", (int)block.Day);
			cmd.Parameters.AddWithValue("$start", ToDbTime(block.Start));
			cmd.Parameters.AddWithValue("$end", ToDbTime(block.End));

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		// Deletes an availability window by primary key.
		// Returns true if exactly one row was removed.
		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM Availability
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}
	}
}
