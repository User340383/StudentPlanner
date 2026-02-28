using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Core
{
	public interface IAvailabilityRepository
	{
		List<Availability> GetAll();
		int Add(Availability block);
		bool Delete(int id);
	}
}
