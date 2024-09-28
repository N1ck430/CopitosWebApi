using CopitosWebApi.Models.Data;

namespace CopitosWebApi.Services.CustomerDataService;

public interface ICustomerDataService
{
    public Task<bool> AddCustomer(Customer customer);
    public Task<IEnumerable<Customer>> AllCustomers();

}