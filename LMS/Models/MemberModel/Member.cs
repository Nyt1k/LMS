using LMS.Models.BookModel;
using LMS.Models.HoldModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
	public class Member
	{
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
	
		public string PhoneNUmber { get; set; }

		public string UserMembershipId { get; set; }
		[Required]
		public ApplicationUser User { get; set; }

		public ICollection<Book> Books { get; set; }

		public IList<Hold> Holds { get; set; }
	}
}
