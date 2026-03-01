using System.Collections.Generic;

namespace StudentPlanner.Core
{
	// Persistence contract for weekly availability windows.
	// Availability defines when the student is allowed to schedule study blocks.
	public interface IAvailabilityRepository
	{
		List<Availability> GetAll();             // Returns all availability windows (typically day/time ordered)
		int Add(Availability block);             // Inserts window; returns generated Id
		bool Delete(int id);                     // Deletes by Id; true if one row affected
	}
}
