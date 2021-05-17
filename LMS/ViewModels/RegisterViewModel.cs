using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailInUse",controller:"Account")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Must be the same as password fild.")]
		public string ConfirmPassword { get; set; }
	}
}
