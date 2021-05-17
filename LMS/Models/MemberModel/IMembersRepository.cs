using LMS.Models.BookModel;
using System.Collections.Generic;

namespace LMS.Models.MemberModel
{
	public interface IMembersRepository
	{
		Member GetMember(int id);
		IEnumerable<Member> GetAll();

		Member Add(Member member);
		Member Update(Member memberToChange);
		Member Delete(int id);
		Member GetMemberByEmail(string email);
		Member UserIsMember(string email);

		IEnumerable<Book> GetAllMemberBooks(string email);
	}
}
