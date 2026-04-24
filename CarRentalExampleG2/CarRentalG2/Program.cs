

using CarRentalG2.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalG2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			// 1st - tell the web app builder that we want to add a new service (in this case, DbContext - the translation layer between code and db)
			// 2nd - we tell it that we want to give it a list of options, in this case, we tell it we want to use Microsoft SQL Server
			// 3rd - in order to TALK to the SQL server, use a specific connection string, in this case, one called Conn
			builder.Services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseAuthorization();

			app.MapStaticAssets();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}
