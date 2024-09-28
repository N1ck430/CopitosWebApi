using CopitosWebApi.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CopitosWebApi.Services.Validation;

public interface IValidationService
{
    public ValidationProblem? ValidateCustomer(Customer customer);
}