using LMS.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
	public class MemberEditViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		
		public string PhoneNUmber { get; set; }

		public string UserMembershipId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
