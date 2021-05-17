using System.Collections.Generic;

namespace LMS.ViewModels
{
	public class UserClaimsViewModel
	{
		public UserClaimsViewModel()
		{
			Claims = new List<UserClaim>();
		}

		public string Id { get; set; }

		public List<UserClaim> Claims { get; set; }
	}
}
