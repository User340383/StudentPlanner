using System;
using System.Collections.Generic;

namespace StudentPlanner.Core
{
	// Persistence contract for generated schedule blocks.
	// Schedule blocks are the stored output of the scheduling algorithm.
	public interface IScheduleBlockRepository
	{
		List<ScheduleBlock> GetAll();                               // Returns all persisted schedule blocks
		List<ScheduleBlock> GetByDateRange(DateTime start, DateTime end); // Returns blocks within a date range

		int Add(ScheduleBlock block);                              // Inserts one block; returns generated Id
		void AddMany(List<ScheduleBlock> blocks);                  // Inserts multiple blocks in sequence

		bool Update(ScheduleBlock block);                          // Updates all fields of a block
		bool SetLocked(int id, bool isLocked);                     // Updates only the lock flag
		bool SetCompleted(int id, bool isCompleted);               // Updates only the completion flag

		bool Delete(int id);                                       // Deletes one block by Id
		void DeleteAll();                                          // Clears all schedule blocks
		void DeleteUnlockedAndIncomplete();
	}
}
