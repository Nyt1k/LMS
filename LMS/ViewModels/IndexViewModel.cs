using LMS.Models.BookModel;
using System.Collections.Generic;

namespace LMS.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<Book> Books { get; set; }
		public string SearchBook { get; set; }
	}
}
