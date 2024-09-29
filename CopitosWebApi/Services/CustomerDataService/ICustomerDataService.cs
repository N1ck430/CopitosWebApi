using CopitosWebApi.Models.Data;

namespace CopitosWebApi.Services.CustomerDataService;

public interface ICustomerDataService
{
    public Task<bool> AddCustomer(Customer customer);
    public Task<IEnumerable<Customer>> AllCustomers();
    public Task<Customer?> GetCustomer(Guid id);
    public Task<bool> UpdateCustomer(Customer customer);
    public Task<bool> DeleteCustomer(Customer customer);

}