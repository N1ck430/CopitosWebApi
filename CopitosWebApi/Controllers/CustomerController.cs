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
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
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
    }
}
