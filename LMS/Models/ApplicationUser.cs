using Microsoft.AspNetCore.Identity;

namespace LMS.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Member Membership { get; set; }
	}
}
