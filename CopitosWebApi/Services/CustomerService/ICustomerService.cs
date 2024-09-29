using CopitosWebApi.Models.Data;
using CopitosWebApi.Models.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CopitosWebApi.Services.CustomerService;

public interface ICustomerService
{
    public Task<Results<Ok<bool>, ValidationProblem>> AddCustomer(AddCustomerRequest request);
    public Task<IEnumerable<Customer>> GetCustomers();
    public Task<Results<Ok<bool>, ValidationProblem, NotFound>> UpdateCustomer(Customer customer);
    public Task<Results<Ok<bool>, NotFound>> DeleteCustomer(Guid id);
}