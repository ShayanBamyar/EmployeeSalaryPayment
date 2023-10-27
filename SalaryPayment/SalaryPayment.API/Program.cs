using Microsoft.EntityFrameworkCore;
using SalaryPayment.Repository.Context;

namespace SalaryPayment.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
			var dbName = Environment.GetEnvironmentVariable("DB_NAME");
			var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
			var connectionString = $"Data Server={dbHost};Initial Catalog={dbName};User Id=sa;Password={dbPassword};";	
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}