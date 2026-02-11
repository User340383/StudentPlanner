using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace StudentPlanner.Data
{
	public static class AppPaths
	{
		private const string AppFolderName = "StudentPlanner";
		private const string DbFileName = "studentplanner.db";

		public static string GetDatabaseFilePath()
		{
			string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			string appFolder = Path.Combine(localAppData, AppFolderName);

			Directory.CreateDirectory(appFolder);

			return Path.Combine(appFolder, DbFileName);
		}
	}

	public static class DbConfig
	{
		public static string GetConnectionString()
		{
			string dbPath = AppPaths.GetDatabaseFilePath();

			return $"Data Source={dbPath}";
		}
	}

	public static class DatabaseInitializer
	{
		public static void Initialize()
		{
			using var connection = new SqliteConnection(DbConfig.GetConnectionString());
			connection.Open();

			// Always enable FK constraints in SQLite (must be per-connection)
			using (var pragma = connection.CreateCommand())
			{
				pragma.CommandText = "PRAGMA foreign_keys = ON;";
				pragma.ExecuteNonQuery();
			}

			using var cmd = connection.CreateCommand();
			cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS Courses (
    Id   INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Tasks (
    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
    CourseId       INTEGER NOT NULL,
    Title          TEXT NOT NULL,
    Deadline       TEXT NOT NULL,
    EstimatedHours REAL NOT NULL CHECK (EstimatedHours >= 0),
    Priority       INTEGER NOT NULL CHECK (Priority BETWEEN 1 AND 5),
    IsCompleted    INTEGER NOT NULL DEFAULT 0 CHECK (IsCompleted IN (0,1)),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Availability (
    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
    DayOfWeek INTEGER NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6),
    StartTime TEXT NOT NULL,
    EndTime   TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Commitments (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    DayOfWeek   INTEGER NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6),
    StartTime   TEXT NOT NULL,
    EndTime     TEXT NOT NULL,
    Description TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS ScheduleBlocks (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    TaskId      INTEGER NOT NULL,
    Date        TEXT NOT NULL,
    StartTime   TEXT NOT NULL,
    EndTime     TEXT NOT NULL,
    IsCompleted INTEGER NOT NULL DEFAULT 0 CHECK (IsCompleted IN (0,1)),
    IsLocked    INTEGER NOT NULL DEFAULT 0 CHECK (IsLocked IN (0,1)),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_Tasks_CourseId ON Tasks(CourseId);
CREATE INDEX IF NOT EXISTS IX_Tasks_Deadline ON Tasks(Deadline);
CREATE INDEX IF NOT EXISTS IX_SBlocks_TaskId ON ScheduleBlocks(TaskId);
CREATE INDEX IF NOT EXISTS IX_SBlocks_Date ON ScheduleBlocks(Date);
";
			cmd.ExecuteNonQuery();
		}
	}
}
