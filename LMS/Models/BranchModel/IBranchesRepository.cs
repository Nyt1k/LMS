using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.BranchModel
{
	public interface IBranchesRepository
	{
		Branch GetBranch(int id);
		IEnumerable<Branch> GetAll(int id);
	}
}
