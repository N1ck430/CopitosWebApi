using CopitosWebApi.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace CopitosWebApi.Models.DbContext;

public class CustomerDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}