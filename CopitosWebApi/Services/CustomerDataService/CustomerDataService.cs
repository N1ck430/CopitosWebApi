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
        return await _dbContext.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer?> GetCustomer(Guid id)
    {
        return await _dbContext.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        _dbContext.Customers.Update(customer);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCustomer(Customer customer)
    {
        _dbContext.Customers.Remove(customer);

        return await _dbContext.SaveChangesAsync() > 0;
    }
}