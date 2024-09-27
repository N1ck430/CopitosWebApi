using CopitosWebApi.Models;
using CopitosWebApi.Services.Validation;

namespace CopitosWebApi.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    private readonly IValidationService _validationService;

    public CustomerService(IValidationService validationService)
    {
        _validationService = validationService;
    }

    public void AddCustomer(Customer customer)
    {
        _validationService.ValidateCustomer(customer);

        _customers.Add(Guid.NewGuid(), customer);
    }

    public IEnumerable<CustomerResponse> GetCustomers()
    {
        return _customers.Select(kvp => new CustomerResponse(kvp.Key, kvp.Value));
    }
}