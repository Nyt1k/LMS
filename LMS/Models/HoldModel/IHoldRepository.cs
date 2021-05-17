using System.Collections.Generic;

namespace LMS.Models.HoldModel
{
	public interface IHoldRepository
	{
		Hold GetHold(int id);
		IEnumerable<Hold> GetAll();

		Hold Add(Hold hold);
		Hold Update(Hold holdToChange);
		Hold Delete(int id);

	}
}
