using System.Diagnostics.CodeAnalysis;
using CopitosWebApi.Models.DbContext;
using CopitosWebApi.Services.CustomerDataService;
using CopitosWebApi.Services.CustomerService;
using CopitosWebApi.Services.DateProvider;
using CopitosWebApi.Services.Validation;
using Microsoft.EntityFrameworkCore;

namespace CopitosWebApi;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CustomerDbContext>(options => options.UseInMemoryDatabase("MemoryDb"));

        builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IValidationService, ValidationService>();
        builder.Services.AddSingleton<IDateProvider, DateProvider>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}