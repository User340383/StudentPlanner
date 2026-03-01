using System.Collections.Generic;

namespace StudentPlanner.Core
{
	// Persistence contract for TaskItem entities.
	// Implementations (e.g., SQLite) live in the Data layer; UI depends only on this abstraction.
	public interface ITaskRepository
	{
		// Query operations
		List<TaskItem> GetAll();                 // Returns all tasks (typically sorted by deadline)
		List<TaskItem> GetByCourse(int courseId); // Returns tasks for a specific course (FK)

		// CRUD operations
		int Add(TaskItem task);                  // Inserts task; returns generated Id
		bool Update(TaskItem task);              // Updates existing task; true if one row affected
		bool Delete(int id);                     // Deletes by Id; true if one row affected

		// Targeted update used for UI toggles (avoids rewriting full row)
		bool SetCompleted(int id, bool isCompleted);
	}
}
