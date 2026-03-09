using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StudentPlanner.Data
{
	// Concrete implementation of IScheduleBlockRepository.
	// Persists generated schedule blocks to SQLite and reloads them into the UI.
	public class ScheduleBlockRepository : IScheduleBlockRepository
	{
		private readonly string _connectionString;

		// Allows optional injection of a custom connection string (useful for testing).
		public ScheduleBlockRepository(string? connectionString = null)
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

		// Store only the calendar date portion.
		private static string ToDbDate(DateTime date)
			=> date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

		// Parse date from SQLite storage format.
		private static DateTime FromDbDate(string text)
			=> DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

		// Store TimeSpan in stable 24-hour format.
		private static string ToDbTime(TimeSpan time)
			=> time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

		// Parse TimeSpan from stored format.
		private static TimeSpan FromDbTime(string text)
			=> TimeSpan.ParseExact(text, @"hh\:mm", CultureInfo.InvariantCulture);

		// Returns all schedule blocks ordered chronologically.
		public List<ScheduleBlock> GetAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, TaskId, Date, StartTime, EndTime, IsCompleted, IsLocked
FROM ScheduleBlocks
ORDER BY Date, StartTime;
";

			var list = new List<ScheduleBlock>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new ScheduleBlock
				{
					Id = reader.GetInt32(0),
					TaskId = reader.GetInt32(1),
					Date = FromDbDate(reader.GetString(2)),
					Start = FromDbTime(reader.GetString(3)),
					End = FromDbTime(reader.GetString(4)),
					IsCompleted = reader.GetInt32(5) == 1,
					IsLocked = reader.GetInt32(6) == 1
				});
			}

			return list;
		}

		// Returns schedule blocks within the specified inclusive date range.
		public List<ScheduleBlock> GetByDateRange(DateTime start, DateTime end)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
SELECT Id, TaskId, Date, StartTime, EndTime, IsCompleted, IsLocked
FROM ScheduleBlocks
WHERE Date >= $start AND Date <= $end
ORDER BY Date, StartTime;
";

			cmd.Parameters.AddWithValue("$start", ToDbDate(start.Date));
			cmd.Parameters.AddWithValue("$end", ToDbDate(end.Date));

			var list = new List<ScheduleBlock>();

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new ScheduleBlock
				{
					Id = reader.GetInt32(0),
					TaskId = reader.GetInt32(1),
					Date = FromDbDate(reader.GetString(2)),
					Start = FromDbTime(reader.GetString(3)),
					End = FromDbTime(reader.GetString(4)),
					IsCompleted = reader.GetInt32(5) == 1,
					IsLocked = reader.GetInt32(6) == 1
				});
			}

			return list;
		}

		// Inserts a single schedule block and returns the generated primary key.
		public int Add(ScheduleBlock block)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO ScheduleBlocks (TaskId, Date, StartTime, EndTime, IsCompleted, IsLocked)
VALUES ($taskId, $date, $start, $end, $completed, $locked);
SELECT last_insert_rowid();
";

			cmd.Parameters.AddWithValue("$taskId", block.TaskId);
			cmd.Parameters.AddWithValue("$date", ToDbDate(block.Date));
			cmd.Parameters.AddWithValue("$start", ToDbTime(block.Start));
			cmd.Parameters.AddWithValue("$end", ToDbTime(block.End));
			cmd.Parameters.AddWithValue("$completed", block.IsCompleted ? 1 : 0);
			cmd.Parameters.AddWithValue("$locked", block.IsLocked ? 1 : 0);

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

		// Inserts multiple schedule blocks.
		// Used after the scheduler generates a fresh plan.
		public void AddMany(List<ScheduleBlock> blocks)
		{
			foreach (var block in blocks)
			{
				Add(block);
			}
		}

		// Deletes one schedule block by primary key.
		public bool Delete(int id)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM ScheduleBlocks
WHERE Id = $id;
";

			cmd.Parameters.AddWithValue("$id", id);

			return cmd.ExecuteNonQuery() == 1;
		}

		// Deletes all persisted schedule blocks.
		// Used when regenerating the schedule from scratch.
		public void DeleteAll()
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
DELETE FROM ScheduleBlocks;
";

			cmd.ExecuteNonQuery();
		}
	}
}
