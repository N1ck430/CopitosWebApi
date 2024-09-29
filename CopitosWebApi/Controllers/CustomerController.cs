using CopitosWebApi.Models.Data;
using CopitosWebApi.Models.Requests;
using CopitosWebApi.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CopitosWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IResult> AddCustomer(AddCustomerRequest request)
        {
            try
            {
                var result = await _customerService.AddCustomer(request);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "AddCustomer | Could not add Customer {customerRequest}",
                    JsonSerializer.Serialize(request));
   
                return TypedResults.BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                return Ok(await _customerService.GetCustomers());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetCustomers | Could not get customers");
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateCustomer(Customer request)
        {
            try
            {
                var result = await _customerService.UpdateCustomer(request);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UpdateCustomer | Could not update Customer {customer}",
                    JsonSerializer.Serialize(request));
                return TypedResults.BadRequest();
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteCustomer(Guid id)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(id);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "DeleteCustomer | Could not delete Customer {id}", id);
                return TypedResults.BadRequest();
            }
        }
    }
}
