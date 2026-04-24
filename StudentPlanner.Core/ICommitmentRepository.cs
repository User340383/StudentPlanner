namespace StudentPlanner.Core
{
	// Persistence contract for fixed weekly commitments (e.g., work shifts, meetings).
	// Commitments will be used later by the scheduler to block out unavailable time.
	public interface ICommitmentRepository
	{
		List<Commitment> GetAll();               // Returns all commitments (typically day/time ordered)
		int Add(Commitment item);                // Inserts commitment; returns generated Id
		bool Delete(int id);                     // Deletes by Id; true if one row affected
	}
}
