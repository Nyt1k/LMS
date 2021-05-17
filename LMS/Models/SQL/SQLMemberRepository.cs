using LMS.Models.BookModel;
using LMS.Models.MemberModel;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Models.SQL
{
	public class SQLMemberRepository : IMembersRepository
	{
		private readonly AppDbContext _context;

		public SQLMemberRepository(AppDbContext context)
		{
			_context = context;
		}

		public Member Add(Member member)
		{
			_context.Add(member);
			_context.SaveChanges();

			return member;
		}

		public Member Delete(int id)
		{
			var member = _context.Members.Find(id);
			if (member != null)
			{
				_context.Members.Remove(member);
				_context.SaveChanges();
			}

			return member;
		}

		public IEnumerable<Member> GetAll()
		{
			return _context.Members;
		}

		public Member GetMember(int id)
		{
			return GetAll().FirstOrDefault(m => m.Id == id);
		}

		public Member Update(Member memberToChange)
		{
			var member = _context.Members.Attach(memberToChange);
			member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
			return memberToChange;
		}
		public Member GetMemberByEmail(string email)
		{
			var user = _context.Users.FirstOrDefault(m => m.Email == email);

			var member = _context.Members.FirstOrDefault(m => m.UserMembershipId == user.Id);

			if(member == null)
			{
				var newMember = new Member
				{
					User = user,
					UserMembershipId = user.Id,
				};

				return (newMember);
			}

			return member;
		}

		public Member UserIsMember(string email)
		{
			var user = _context.Users.FirstOrDefault(m => m.Email == email);

			var member = _context.Members.FirstOrDefault(m => m.UserMembershipId == user.Id);

			return member;
		}

		public IEnumerable<Book> GetAllMemberBooks(string email)
		{
			var user = _context.Users.FirstOrDefault(m => m.Email == email);

			var member = _context.Members.FirstOrDefault(m => m.UserMembershipId == user.Id);


			var books = _context.Books;

			var userBooks = books.Where(b => b.Id == member.Id).ToList();

			return userBooks;
		}
	}
}
