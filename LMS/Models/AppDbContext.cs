using LMS.Models.BookModel;
using LMS.Models.BranchModel;
using LMS.Models.HoldModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LMS.Models
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
			: base(options)
		{

		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Member> Members { get; set; }

		public DbSet<Hold> Holds { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Seed();

			foreach(var foreignKey in modelBuilder.Model.GetEntityTypes()
				.SelectMany(e => e.GetForeignKeys()))
			{
				foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
			}

			modelBuilder.Entity<ApplicationUser>()
				.HasOne(a => a.Membership)
				.WithOne(b => b.User)
				.HasForeignKey<Member>(b => b.UserMembershipId);

			modelBuilder.Entity<Member>()
				.HasMany(a => a.Books)
				.WithOne(b => b.Member)
				.HasForeignKey(c => c.MemberId);

			modelBuilder.Entity<Member>()
				.HasMany(a => a.Holds)
				.WithOne(b => b.Member)
				.HasForeignKey(c => c.MemberId);

			modelBuilder.Entity<Hold>()
				.HasOne(a => a.Book)
				.WithOne(b => b.Hold)
				.HasForeignKey<Book>(c => c.HoldId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
