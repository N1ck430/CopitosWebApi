using System.ComponentModel.DataAnnotations;
using CopitosWebApi.Models;

namespace CopitosWebApi.Services.Validation;

public interface IValidationService
{
    /// <summary>
    /// Validated a customer and throws and ValidationException when validaiton was not successfull
    /// </summary>
    /// <param name="customer"></param>
    /// <exception cref="ValidationException"></exception>
    public void ValidateCustomer(Customer customer);
}