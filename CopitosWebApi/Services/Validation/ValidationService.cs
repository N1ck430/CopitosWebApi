using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CopitosWebApi.Models;
using CopitosWebApi.Services.DateProvider;

namespace CopitosWebApi.Services.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IDateProvider _dateProvider;
        private readonly Regex _plzRegex = new("^[0-9]{5}$");

        public ValidationService(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        /// <summary>
        /// Validated a customer and throws and ValidationException when validaiton was not successfull
        /// </summary>
        /// <param name="customer"></param>
        /// <exception cref="ValidationException"></exception>
        public void ValidateCustomer(Customer customer)
        {
            if (customer.Geburtsdatum >= _dateProvider.UtcNow)
            {
                throw new ValidationException("Geburtsdatum muss in der Vergangenheit liegen.");
            }

            if (!_plzRegex.IsMatch(customer.Plz))
            {
                throw new ValidationException("Plz nicht valide. Sie muss auf 5 numerischen Zeichen bestehen.");
            }

            if (string.IsNullOrEmpty(customer.Land))
            {
                return;
            }

            if (customer.Land.Length != 2)
            {
                throw new ValidationException("Das Land darf nur aus 2 Zeichen bestehen.");
            }
        }
    }
}
