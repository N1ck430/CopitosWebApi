using CopitosWebApi.Models.Data;
using CopitosWebApi.Services.DateProvider;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.RegularExpressions;

namespace CopitosWebApi.Services.Validation
{
    public partial class ValidationService : IValidationService
    {
        [GeneratedRegex("^[0-9]{5}$")]
        private static partial Regex PlzRegex();


        private readonly IDateProvider _dateProvider;
        private readonly Regex _plzRegex = PlzRegex();

        public ValidationService(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public ValidationProblem? ValidateCustomer(Customer customer)
        {
            var errorDictionary = new Dictionary<string, string[]>();

            if (customer.Geburtsdatum >= _dateProvider.UtcNow)
            {
                errorDictionary.Add(nameof(customer.Geburtsdatum), ["Geburtsdatum muss in der Vergangenheit liegen."]);
            }

            if (!_plzRegex.IsMatch(customer.Plz))
            {
                errorDictionary.Add(nameof(customer.Plz), ["Plz nicht valide. Sie muss auf 5 numerischen Zeichen bestehen."]);
            }

            if (!string.IsNullOrEmpty(customer.Land) && customer.Land.Length != 2)
            {
                errorDictionary.Add(nameof(customer.Land), ["Wenn gefüllt, darf das Land nur aus 2 Zeichen bestehen."]);
            }

            if (errorDictionary.Any())
            {
                return TypedResults.ValidationProblem(errorDictionary);
            }

            return null;
        }
    }
}
