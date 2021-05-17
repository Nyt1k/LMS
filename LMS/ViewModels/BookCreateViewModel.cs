using LMS.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
	public class BookCreateViewModel
	{

		public int ISBN { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Author { get; set; }
		public string Year { get; set; }
		//[Required]
		//public Status Status { get; set; }
		public IFormFile Image { get; set; }
		public int NumberOfCopies { get; set; }
		[Required]
		public Bran Location { get; set; }
	}
}
