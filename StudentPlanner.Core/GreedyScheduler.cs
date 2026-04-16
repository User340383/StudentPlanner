using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPlanner.Core
{
	// Simple week scheduler:
	// - Builds free time from availability minus commitments
	// - Schedules tasks by earliest deadline, then priority
	// - Splits tasks into fixed-length blocks (e.g., 60 minutes)
	public class GreedyScheduler : IScheduler
	{
		private readonly TimeSpan _blockSize;

		public GreedyScheduler(TimeSpan? blockSize = null)
		{
			_blockSize = blockSize ?? TimeSpan.FromMinutes(60);
		}

		public ScheduleResult GenerateWeeklySchedule(ScheduleInput input)
		{
			var result = new ScheduleResult();

			// Basic validation
			if (input.Tasks == null || input.Availability == null || input.Commitments == null)
			{
				result.Warnings.Add("Schedule input is missing required lists.");
				return result;
			}

			// Look ahead into the future instead of only using the current calendar week
			DateTime startFrom = DateTime.Now;
			int daysToLookAhead = 14;

			// Only schedule tasks that are not completed
			var tasks = input.Tasks
				.Where(t => !t.IsCompleted)
				.OrderBy(t => t.Deadline)
				.ThenByDescending(t => t.Priority)
				.ToList();

			// Build free slots for the next N days
			var freeSlots = BuildFreeSlots(startFrom, daysToLookAhead, input.Availability, input.Commitments);

			// Convert free slots into a cursor that we fill with blocks
			foreach (var task in tasks)
			{
				double hoursRemaining = task.EstimatedHours;

				while (hoursRemaining > 0.0001)
				{
					// Find earliest slot with room for at least one block
					int slotIndex = freeSlots.FindIndex(s => s.Duration >= _blockSize);
					if (slotIndex < 0)
					{
						result.Warnings.Add(
							$"Not enough free time to fully schedule: '{task.Title}' (remaining ~{hoursRemaining:0.0}h).");
						break;
					}

					var slot = freeSlots[slotIndex];

					// Schedule one block (or a smaller partial block if < blockSize remains)
					var minutesRemaining = (int)Math.Round(hoursRemaining * 60.0);
					var desired = _blockSize;
					var actual = TimeSpan.FromMinutes(Math.Min(desired.TotalMinutes, minutesRemaining));

					// Ensure actual fits slot (guard)
					if (actual > slot.Duration)
					{
						actual = slot.Duration;
					}

					// Create schedule block
					result.ScheduleBlocks.Add(new ScheduleBlock
					{
						TaskId = task.Id,
						Date = slot.Start.Date,
						Start = slot.Start.TimeOfDay,
						End = (slot.Start + actual).TimeOfDay,
						IsCompleted = false,
						IsLocked = false
					});

					// Consume time from slot
					var newStart = slot.Start + actual;
					if (newStart >= slot.End)
					{
						freeSlots.RemoveAt(slotIndex);
					}
					else
					{
						freeSlots[slotIndex] = new TimeSlot(newStart, slot.End);
					}

					hoursRemaining -= actual.TotalHours;

					// Stop scheduling past task deadline (simple safety)
					// If the slot is after the task deadline, warn and stop scheduling that task
					if (slot.Start > task.Deadline || newStart > task.Deadline)
					{
						result.Warnings.Add(
							$"Some blocks for '{task.Title}' may occur after its deadline ({task.Deadline:g}).");
						break;
					}
				}
			}

			// Sort output for display
			result.ScheduleBlocks = result.ScheduleBlocks
				.OrderBy(b => b.Date)
				.ThenBy(b => b.Start)
				.ToList();

			if (result.ScheduleBlocks.Count == 0 && result.Warnings.Count == 0)
			{
				result.Warnings.Add("No schedule blocks generated. Check availability and tasks.");
			}

			return result;
		}

		// --------- helpers ---------

		private static List<TimeSlot> BuildFreeSlots(DateTime startFrom,int daysToLookAhead,List<Availability> availability, List<Commitment> commitments)
		{
			var slots = new List<TimeSlot>();

			// Step 1: add availability windows for each of the next N days
			for (int i = 0; i < daysToLookAhead; i++)
			{
				DateTime day = startFrom.Date.AddDays(i);
				var dayAvail = availability.Where(a => a.Day == day.DayOfWeek);

				foreach (var a in dayAvail)
				{
					DateTime start = day.Date + a.Start;
					DateTime end = day.Date + a.End;

					// Skip invalid ranges
					if (end <= start)
					{
						continue;
					}

					// Trim away past time on the first day
					if (end <= startFrom)
					{
						continue;
					}

					if (start < startFrom)
					{
						start = startFrom;
					}

					if (end > start)
					{
						slots.Add(new TimeSlot(start, end));
					}
				}
			}

			// Step 2: subtract commitments from those slots
			for (int i = 0; i < daysToLookAhead; i++)
			{
				DateTime day = startFrom.Date.AddDays(i);
				var dayCommit = commitments.Where(c => c.Day == day.DayOfWeek).ToList();

				if (dayCommit.Count == 0)
				{
					continue;
				}

				foreach (var c in dayCommit)
				{
					DateTime cStart = day.Date + c.Start;
					DateTime cEnd = day.Date + c.End;

					slots = SubtractWindow(slots, cStart, cEnd);
				}
			}

			return slots.OrderBy(s => s.Start).ToList();
		}

		// Removes [cutStart, cutEnd] from the list of slots, splitting slots if needed.
		private static List<TimeSlot> SubtractWindow(List<TimeSlot> slots, DateTime cutStart, DateTime cutEnd)
		{
			var output = new List<TimeSlot>();

			foreach (var s in slots)
			{
				// No overlap
				if (cutEnd <= s.Start || cutStart >= s.End)
				{
					output.Add(s);
					continue;
				}

				// Overlap exists: keep left part if any
				if (cutStart > s.Start)
				{
					output.Add(new TimeSlot(s.Start, cutStart));
				}

				// Keep right part if any
				if (cutEnd < s.End)
				{
					output.Add(new TimeSlot(cutEnd, s.End));
				}
			}
			return output;
		}

		// Internal helper type for time math
		private readonly struct TimeSlot
		{
			public DateTime Start { get; }
			public DateTime End { get; }
			public TimeSpan Duration => End - Start;

			public TimeSlot(DateTime start, DateTime end)
			{
				Start = start;
				End = end;
			}
		}
	}
}
