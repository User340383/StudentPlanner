using Microsoft.Data.Sqlite;

namespace StudentPlanner.Data
{
	/// <summary>
	/// Provides centralized logic for determining where the SQLite database file
	/// is stored on the local machine.
	///
	/// The database is placed inside the user's LocalApplicationData directory,
	/// ensuring:
	/// - No administrator privileges are required.
	/// - The database is user-specific.
	/// - The file persists across application restarts.
	/// </summary>
	public static class AppPaths
	{
		// Folder name under %LOCALAPPDATA%
		private const string AppFolderName = "StudentPlanner";

		// SQLite file name
		private const string DbFileName = "studentplanner.db";

		/// <summary>
		/// Returns the full absolute file path to the SQLite database file.
		/// Creates the application directory if it does not already exist.
		/// </summary>
		public static string GetDatabaseFilePath()
		{
			// Retrieve OS-specific local application data directory.
			string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			// Combine into: %LOCALAPPDATA%\StudentPlanner
			string appFolder = Path.Combine(localAppData, AppFolderName);

			// Ensure directory exists before returning the file path.
			Directory.CreateDirectory(appFolder);

			return Path.Combine(appFolder, DbFileName);
		}
	}

	/// <summary>
	/// Central configuration provider for SQLite connection strings.
	/// Keeps database configuration logic in one place.
	/// </summary>
	public static class DbConfig
	{
		/// <summary>
		/// Builds and returns the SQLite connection string.
		/// </summary>
		public static string GetConnectionString()
		{
			string dbPath = AppPaths.GetDatabaseFilePath();

			// SQLite uses a simple Data Source format.
			return $"Data Source={dbPath}";
		}
	}

	/// <summary>
	/// Responsible for creating and initializing the database schema.
	///
	/// This method is idempotent:
	/// - It can be safely executed multiple times.
	/// - Tables are only created if they do not already exist.
	///
	/// It should be called once at application startup.
	/// </summary>
	public static class DatabaseInitializer
	{
		/// <summary>
		/// Creates tables and indexes if they do not already exist.
		/// Also enables SQLite foreign key enforcement.
		/// </summary>
		public static void Initialize()
		{
			using var connection = new SqliteConnection(DbConfig.GetConnectionString());
			connection.Open();

			// IMPORTANT:
			// SQLite does NOT enforce foreign keys by default.
			// This must be enabled for every new connection.
			using (var pragma = connection.CreateCommand())
			{
				pragma.CommandText = "PRAGMA foreign_keys = ON;";
				pragma.ExecuteNonQuery();
			}

			// Execute schema creation script.
			using var cmd = connection.CreateCommand();
			cmd.CommandText = @"
-- ===========================
-- Courses
-- ===========================
CREATE TABLE IF NOT EXISTS Courses (
    Id   INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE
);

-- ===========================
-- Tasks
-- ===========================
CREATE TABLE IF NOT EXISTS Tasks (
    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
    CourseId       INTEGER NOT NULL,
    Title          TEXT NOT NULL,
    Deadline       TEXT NOT NULL,  -- Stored as ISO datetime string
    EstimatedHours REAL NOT NULL CHECK (EstimatedHours >= 0),
    Priority       INTEGER NOT NULL CHECK (Priority BETWEEN 1 AND 5),
    IsCompleted    INTEGER NOT NULL DEFAULT 0 CHECK (IsCompleted IN (0,1)),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE
);

-- ===========================
-- Availability (User free time)
-- ===========================
CREATE TABLE IF NOT EXISTS Availability (
    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
    DayOfWeek INTEGER NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6),
    StartTime TEXT NOT NULL,  -- Stored as HH:mm
    EndTime   TEXT NOT NULL
);

-- ===========================
-- Commitments (Fixed obligations)
-- ===========================
CREATE TABLE IF NOT EXISTS Commitments (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    DayOfWeek   INTEGER NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6),
    StartTime   TEXT NOT NULL,
    EndTime     TEXT NOT NULL,
    Description TEXT NOT NULL
);

-- ===========================
-- ScheduleBlocks (Generated output)
-- ===========================
CREATE TABLE IF NOT EXISTS ScheduleBlocks (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    TaskId      INTEGER NOT NULL,
    Date        TEXT NOT NULL,   -- Stored as ISO date
    StartTime   TEXT NOT NULL,
    EndTime     TEXT NOT NULL,
    IsCompleted INTEGER NOT NULL DEFAULT 0 CHECK (IsCompleted IN (0,1)),
    IsLocked    INTEGER NOT NULL DEFAULT 0 CHECK (IsLocked IN (0,1)),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE
);

-- ===========================
-- Performance indexes
-- ===========================
CREATE INDEX IF NOT EXISTS IX_Tasks_CourseId ON Tasks(CourseId);
CREATE INDEX IF NOT EXISTS IX_Tasks_Deadline ON Tasks(Deadline);
CREATE INDEX IF NOT EXISTS IX_SBlocks_TaskId ON ScheduleBlocks(TaskId);
CREATE INDEX IF NOT EXISTS IX_SBlocks_Date ON ScheduleBlocks(Date);
";

			cmd.ExecuteNonQuery();
		}
	}
}
