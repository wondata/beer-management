
using BeerManagement.Application.Interfaces;
using BeerManagement.Application.Models;
using BeerManagement.Domain.Services;
using BeerManagement.Infrastructure.Data;
using BeerManagement.Infrastructure.Repositories;

namespace BeerManagement.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BeerDbContext>();
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddTransient<IBeerService, BeerService>();

            builder.Services.AddControllers();
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
