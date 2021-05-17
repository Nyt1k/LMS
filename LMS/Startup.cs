using LMS.Models;
using LMS.Models.BookModel;
using LMS.Models.BranchModel;
using LMS.Models.HoldModel;
using LMS.Models.MemberModel;
using LMS.Models.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration configuration)
		{
			_config = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("LibraryDbConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				option.Password.RequiredLength = 6;
				option.Password.RequiredUniqueChars = 1;
				option.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<AppDbContext>()
			.AddRoles<IdentityRole>();

			services.AddMvc(option => {
				option.EnableEndpointRouting = false;
				var policy = new AuthorizationPolicyBuilder()
									.RequireAuthenticatedUser()
									.Build();
				option.Filters.Add(new AuthorizeFilter(policy));
				}
			);

			services.AddAuthorization(option =>
			{
				option.AddPolicy("DeleteRolePolicy",
					policy => policy.RequireClaim("Delete Role"));

				option.AddPolicy("EditRolePolicy",
					policy => policy.RequireClaim("Edit Role"));
				option.AddPolicy("AdminRolePolicy",
					policy => policy.RequireClaim("Admin"));
			});

			services.AddAuthentication()
				.AddGoogle(options =>
				{
					options.ClientId = "845177566481-o7j6i9spugtemedsimkvcvndkrrp85u2.apps.googleusercontent.com";
					options.ClientSecret = "N7XfXXRhhuT-MM3g9f5NSMXh";
				});

			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
			});

			services.AddScoped<IMembersRepository, SQLMemberRepository>();
			services.AddScoped<IBooksRepository, SQLBookRepository>();
			services.AddScoped<IHoldRepository, SQLHoldRepository>();

		}



		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseStatusCodePagesWithReExecute("/Error/{0}");
			}

			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
