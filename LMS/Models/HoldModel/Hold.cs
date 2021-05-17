using LMS.Models.BookModel;
using System;
using System.Collections.Generic;

namespace LMS.Models.HoldModel
{
	public class Hold
	{
		public int Id { get; set; }

		public int MemberId { get; set; }
		public Member Member { get; set; }

		public DateTime HoldTime { get; set; }

		public int BookId { get; set; }
		public Book Book { get; set; }
	}
}
