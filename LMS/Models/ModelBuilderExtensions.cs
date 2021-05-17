using LMS.Models.BookModel;
using Microsoft.EntityFrameworkCore;

namespace LMS.Models
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					Id = 1,
					Title = "test",
					Author = "testat",
					Year = "1337",
					ISBN = 1234,
					NumberOfCopies = 10,
					ImageUrl = "/images/republic.jpg",
					Location = Bran.Lviv
				},
				new Book
				{
					Id = 2,
					Title = "test2",
					Author = "testat2",
					Year = "13372",
					ISBN = 12342,
					NumberOfCopies = 102,
					ImageUrl = "/images/1984.jpg",
					Location = Bran.Tryskavets
				},
				new Book
				{
					Id = 3,
					Title = "test3",
					Author = "testat3",
					Year = "13373",
					ISBN = 12343,
					NumberOfCopies = 103,
					ImageUrl = "/images/nobody.jpg",
					Location = Bran.Drohobych
				}
			);
		}
	}
}
