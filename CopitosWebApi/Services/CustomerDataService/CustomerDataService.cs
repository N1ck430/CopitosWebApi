using CopitosWebApi.Models.Data;
using CopitosWebApi.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CopitosWebApi.Services.CustomerDataService;

public class CustomerDataService : ICustomerDataService
{
    private readonly CustomerDbContext _dbContext;

    public CustomerDataService(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddCustomer(Customer customer)
    {
        _ = await _dbContext.Customers.AddAsync(customer);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Customer>> AllCustomers()
    {
        return await _dbContext.Customers.ToListAsync();
    }
}