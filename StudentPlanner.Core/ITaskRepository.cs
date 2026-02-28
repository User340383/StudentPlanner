using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Core
{
	public interface ITaskRepository
	{
		List<TaskItem> GetAll();
		List<TaskItem> GetByCourse(int courseId);

		int Add(TaskItem task);
		bool Update(TaskItem task);
		bool Delete(int id);

		bool SetCompleted(int id, bool isCompleted);
	}
}
