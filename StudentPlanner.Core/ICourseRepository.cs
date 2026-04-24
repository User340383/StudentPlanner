namespace StudentPlanner.Core
{
	// Persistence contract for Course entities.
	// Courses are the parent entity of tasks (Tasks.CourseId FK).
	public interface ICourseRepository
	{
		List<Course> GetAll();                   // Returns all courses (typically alphabetical)
		int Add(string name);                    // Inserts course; returns generated Id
		bool Update(int id, string newName);     // Renames course; true if one row affected
		bool Delete(int id);                     // Deletes course; true if one row affected (may cascade)
	}
}
