using Microsoft.Data.Sqlite;
using StudentPlanner.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Data
{
	public class AvailabilityRepository : IAvailabilityRepository
	{
		private readonly string _connectionString;

		public AvailabilityRepository(string? connectionString = null)
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

		private static string ToDbTime(TimeSpan time)
			=> time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

		private static TimeSpan FromDbTime(string text)
			=> TimeSpan.ParseExact(text, @"hh\:mm", CultureInfo.InvariantCulture);

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

		public int Add(Availability block)
		{
			using var conn = OpenConnection();
			using var cmd = conn.CreateCommand();

			cmd.CommandText = @"
INSERT INTO Availability (DayOfWeek, StartTime, EndTime)
VALUES ($day, $start, $end);
SELECT last_insert_rowid();
";

			cmd.Parameters.AddWithValue("$day", (int)block.Day);
			cmd.Parameters.AddWithValue("$start", ToDbTime(block.Start));
			cmd.Parameters.AddWithValue("$end", ToDbTime(block.End));

			long newId = (long)cmd.ExecuteScalar();
			return (int)newId;
		}

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
