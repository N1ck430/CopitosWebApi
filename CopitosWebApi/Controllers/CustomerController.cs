using CopitosWebApi.Models;
using CopitosWebApi.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer(Customer customer)
        {
            try
            {
                _customerService.AddCustomer(customer);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AddCustomer | Could not add Customer {JsonSerializer.Serialize(customer)}");
                if (e is ValidationException ve)
                {
                    return BadRequest(ve.Message);
                }
                return BadRequest("An error occurred");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]

        public IActionResult GetCustomers()
        {
            try
            {
                return Ok(_customerService.GetCustomers());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetCustomers | Could not get customers");
                return BadRequest();
            }
        }
    }
}
