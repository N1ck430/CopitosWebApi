using CopitosWebApi.Models.Data;
using CopitosWebApi.Models.Requests;
using CopitosWebApi.Services.CustomerDataService;
using CopitosWebApi.Services.Validation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CopitosWebApi.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly IValidationService _validationService;
    private readonly ICustomerDataService _customerDataService;

    public CustomerService(IValidationService validationService, ICustomerDataService customerDataService)
    {
        _validationService = validationService;
        _customerDataService = customerDataService;
    }

    public async Task<Results<Ok<bool>, ValidationProblem>> AddCustomer(AddCustomerRequest request)
    {
        var customer = request.ToCustomer();
        var validationResult = _validationService.ValidateCustomer(customer);
        if (validationResult is not null)
        {
            return validationResult;
        }


        var result = await _customerDataService.AddCustomer(customer);
        return TypedResults.Ok(result);
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _customerDataService.AllCustomers();
    }

    public async Task<Results<Ok<bool>, ValidationProblem, NotFound>> UpdateCustomer(Customer customer)
    {
        var validationResult = _validationService.ValidateCustomer(customer);

        if (validationResult is not null)
        {
            return validationResult;
        }

        var customerFromDb = await _customerDataService.GetCustomer(customer.Id);

        if (customerFromDb is null)
        {
            return TypedResults.NotFound();
        }

        var result = await _customerDataService.UpdateCustomer(customer);
        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<bool>, NotFound>> DeleteCustomer(Guid id)
    {
        var customerFromDb = await _customerDataService.GetCustomer(id);

        if (customerFromDb is null)
        {
            return TypedResults.NotFound();
        }

        var result = await _customerDataService.DeleteCustomer(customerFromDb);
        return TypedResults.Ok(result);
    }
}