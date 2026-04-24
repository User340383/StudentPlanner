namespace StudentPlanner.Core
{
	// Represents an academic course.
	// A Course is the parent entity for TaskItem.
	public class Course
	{
		public int Id { get; set; }           // Primary key (DB)
		public string Name { get; set; } = ""; // Unique course name
	}

	// Represents a task or assignment associated with a course.
	public class TaskItem
	{
		public int Id { get; set; }              // Primary key
		public int CourseId { get; set; }        // FK → Course.Id

		public string Title { get; set; } = "";  // Short task description
		public DateTime Deadline { get; set; }   // Stored in DB as ISO datetime
		public double EstimatedHours { get; set; } // Expected effort
		public int Priority { get; set; }        // 1–5 (enforced in DB)
		public bool IsCompleted { get; set; }    // Completion flag
	}

	// Represents a recurring weekly availability window.
	public class Availability
	{
		public int Id { get; set; }              // Primary key
		public DayOfWeek Day { get; set; }       // 0–6 (stored as INTEGER)
		public TimeSpan Start { get; set; }      // Start of free window
		public TimeSpan End { get; set; }        // End of free window (must be > Start)
	}

	// Represents a fixed commitment that blocks availability.
	public class Commitment
	{
		public int Id { get; set; }              // Primary key
		public DayOfWeek Day { get; set; }       // Day of commitment
		public TimeSpan Start { get; set; }      // Start time
		public TimeSpan End { get; set; }        // End time
		public string Description { get; set; } = ""; // Human-readable label
	}

	// Represents a generated scheduled study block for a task.
	public class ScheduleBlock
	{
		public int Id { get; set; }              // Primary key
		public int TaskId { get; set; }          // Associated Task
		public DateTime Date { get; set; }       // Calendar date
		public TimeSpan Start { get; set; }      // Start time
		public TimeSpan End { get; set; }        // End time
		public bool IsCompleted { get; set; }    // Completion state
		public bool IsLocked { get; set; }       // Prevents auto-rescheduling
	}

	// Defines contract for scheduling algorithm implementations.
	public interface IScheduler
	{
		ScheduleResult GenerateWeeklySchedule(ScheduleInput input);
	}

	// Encapsulates all inputs required by the scheduling engine.
	public class ScheduleInput
	{
		public List<TaskItem> Tasks { get; set; } = new();
		public List<Availability> Availability { get; set; } = new();
		public List<Commitment> Commitments { get; set; } = new();
	}

	// Encapsulates the result of a scheduling run.
	public class ScheduleResult
	{
		public List<ScheduleBlock> ScheduleBlocks { get; set; } = new();
		public List<string> Warnings { get; set; } = new();
	}

	// Temporary placeholder scheduler used during development.
	// Allows UI and persistence layers to be built before algorithm logic.
	public class DummyScheduler : IScheduler
	{
		public ScheduleResult GenerateWeeklySchedule(ScheduleInput input)
		{
			return new ScheduleResult
			{
				ScheduleBlocks = new(),
				Warnings = new() { "Scheduler not implemented yet." }
			};
		}
	}
}
