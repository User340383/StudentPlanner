using System;
using System.Collections.Generic;

namespace StudentPlanner.Core
{
	// Persistence contract for generated schedule blocks.
	public interface IScheduleBlockRepository
	{
		List<ScheduleBlock> GetAll();
		List<ScheduleBlock> GetByDateRange(DateTime start, DateTime end);

		int Add(ScheduleBlock block);
		void AddMany(List<ScheduleBlock> blocks);

		bool Delete(int id);
		void DeleteAll();
	}
}
