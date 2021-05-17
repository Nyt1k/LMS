using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.HoldModel
{
	public class SQLHoldRepository : IHoldRepository
	{
		private readonly AppDbContext _context;

		public SQLHoldRepository(AppDbContext context)
		{
			_context = context;
		}

		public Hold Add(Hold hold)
		{
			_context.Add(hold);
			_context.SaveChanges();

			return hold;
		}

		public Hold Delete(int id)
		{
			var hold = _context.Holds.Find(id);
			if (hold != null)
			{
				_context.Holds.Remove(hold);
				_context.SaveChanges();
			}

			return hold;
		}

		public IEnumerable<Hold> GetAll()
		{
			return _context.Holds
				.Include(h => h.Member)
				.Include(h => h.Book);
		}

		public Hold GetHold(int id)
		{
			return GetAll().FirstOrDefault(m => m.Id == id);
		}

		public Hold Update(Hold holdToChange)
		{
			var hold = _context.Holds.Attach(holdToChange);
			hold.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
			return holdToChange;
		}
	}
}
