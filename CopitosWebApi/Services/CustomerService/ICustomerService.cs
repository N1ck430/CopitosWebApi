using CopitosWebApi.Models;

namespace CopitosWebApi.Services.CustomerService;

public interface ICustomerService
{
    public void AddCustomer(Customer customer);
    public IEnumerable<CustomerResponse> GetCustomers();
}