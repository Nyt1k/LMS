using LMS.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.SQL
{
	public class SQLBookRepository : IBooksRepository
	{
		private readonly AppDbContext _context;

		public SQLBookRepository(AppDbContext context)
		{
			_context = context;
		}
		public Book Add(Book book)
		{
			_context.Add(book);
			_context.SaveChanges();
			return book;
		}

		public Book Delete(int id)
		{
			var book = _context.Books.Find(id);
			if(book != null)
			{
				_context.Books.Remove(book);
				_context.SaveChanges();
			}

			return book;
		}

		public IEnumerable<Book> GetAll()
		{
			return _context.Books;
		}

		public Book GetBook(int id)
		{
			return GetAll().FirstOrDefault(b => b.Id == id);
		}

		public IEnumerable<Book> GetBooksByName(string name)
		{	

			if(string.IsNullOrEmpty(name))
			{
				return GetAll();
			}
			else
			{
				return  GetAll().Where(b => b.Title.ToLower().Contains(name) || name == null);
			}
		}

		public Book Update(Book bookToChange)
		{
			var book = _context.Books.Attach(bookToChange);
			book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
			return bookToChange;
		}
	}
}
