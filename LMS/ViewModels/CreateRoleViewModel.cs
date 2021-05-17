using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public	string RoleName { get; set; }
	}
}
