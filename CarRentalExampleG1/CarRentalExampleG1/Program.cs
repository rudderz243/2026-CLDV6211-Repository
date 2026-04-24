using CarRentalExampleG1.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalExampleG1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// add a NEW SERVICE to our web application
			// service type = DbContext<Name of DbContext file>
			// we then specify what options to use - primarly to use SQL Server rather than another database type
			// finally we task the builder to look for a connection string named ConnString in our app.settings file (Configuration.GetConnectionString)
			builder.Services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

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
