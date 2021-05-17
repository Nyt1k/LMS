using LMS.Models.HoldModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.BookModel
{
	public class Book
	{
		public Book()
		{
			Status = Status.Available;
		}

		[Key]
		[Required]
		public int Id { get; set; }
		public int ISBN { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Author { get; set; }
		public string Year { get; set; }
		[Required]
		public Status Status { get; set; }
		public string ImageUrl { get; set; }
		public int NumberOfCopies { get; set; }
		[Required]
		public Bran Location { get; set; }


		public int? MemberId { get; set; }
		public Member Member { get; set; }

		public int? HoldId { get; set; }
		public Hold Hold { get; set; }
	}
}
