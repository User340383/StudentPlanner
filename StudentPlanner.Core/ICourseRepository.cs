using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Core
{
	public interface ICourseRepository
	{
		List<Course> GetAll();
		int Add(string name);
		bool Update(int id, string newName);
		bool Delete(int id);
	}
}
