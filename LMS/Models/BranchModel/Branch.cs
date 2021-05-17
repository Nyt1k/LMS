using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.BranchModel
{
	public class Branch
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public string Description { get; set; }
		public DateTime OpenDate { get; set; }
		public string ImageUrl { get; set; }

		public virtual IEnumerable<Member> Members { get; set; }

	}
}
