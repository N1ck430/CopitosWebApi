using CopitosWebApi.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ControllerTests.CustomerController;

public class UpdateCustomerTest : CustomerControllerTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerServiceMock.Setup(x => x.UpdateCustomer(It.IsAny<Customer>())).ReturnsAsync(TypedResults.Ok(true));

        var response = await CustomerController.UpdateCustomer(new Customer());

        Assert.That(response, Is.InstanceOf<Results<Ok<bool>, ValidationProblem, NotFound>>());
    }

    [Test]
    public async Task ExceptionTest()
    {
        _ = CustomerServiceMock.Setup(x => x.UpdateCustomer(It.IsAny<Customer>())).Throws(new Exception());

        var response = await CustomerController.UpdateCustomer(new Customer());

        Assert.That(response, Is.InstanceOf<BadRequest>());
    }
}