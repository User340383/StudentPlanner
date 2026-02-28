using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Core
{
	public interface ICommitmentRepository
	{
		List<Commitment> GetAll();
		int Add(Commitment item);
		bool Delete(int id);
	}
}
