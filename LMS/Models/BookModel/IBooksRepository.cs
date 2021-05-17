using System.Collections.Generic;

namespace LMS.Models.BookModel
{
	public interface IBooksRepository
	{
		Book GetBook(int id);
		IEnumerable<Book> GetAll();
		IEnumerable<Book> GetBooksByName(string name);
		Book Add(Book book);
		Book Update(Book bookToChange);
		Book Delete(int id);

	}
}
