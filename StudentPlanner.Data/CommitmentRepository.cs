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
	public class CommitmentRepository : ICommitmentRepository
	{
		private readonly string _connectionString;

		public CommitmentRepository(string? connectionString = null)
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
