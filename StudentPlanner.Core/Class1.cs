namespace StudentPlanner.Core
{
	public class Course
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class TaskItem
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public string Title { get; set; }
		public DateTime Deadline { get; set; }
		public double EstimatedHours { get; set; }
		public int Priority { get; set; }
	}
	public class Availability
	{
		public DayOfWeek Day { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
	}
	public class Commitment
	{
		public DayOfWeek Day { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
		public string Description { get; set; }
	}
	public class ScheduleBlock
	{
		public int TaskId { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
		public bool IsCompleted { get; set; }
		public bool IsLocked { get; set; }
	}
	public interface IScheduler
	{
		ScheduleResult GenerateWeeklySchedule(ScheduleInput input);
	}
	public class ScheduleInput
	{
		public List<TaskItem> Tasks { get; set; }
		public List<Availability> Availability { get; set; }
		public List<Commitment> Commitments { get; set; }
	}
	public class ScheduleResult
	{
		public List<ScheduleBlock> ScheduleBlocks { get; set; }
		public List<string> Warnings { get; set; }
	}
	public class DummyScheduler : IScheduler
	{
		public ScheduleResult GenerateWeeklySchedule(ScheduleInput input)
		{
			return new ScheduleResult
			{
				ScheduleBlocks = new List<ScheduleBlock>(),
				Warnings = new List<string> { "Scheduler not implemented yet." }
			};
		}
	}
}
