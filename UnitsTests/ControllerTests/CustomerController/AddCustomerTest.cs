using CopitosWebApi.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ControllerTests.CustomerController;

public class AddCustomerTest : CustomerControllerTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerServiceMock.Setup(x => x.AddCustomer(It.IsAny<AddCustomerRequest>()))
            .ReturnsAsync(TypedResults.Ok(true));

        var result = await CustomerController.AddCustomer(new AddCustomerRequest());

        Assert.That(result, Is.InstanceOf<Results<Ok<bool>, ValidationProblem>>());
    }

    [Test]
    public async Task ExceptionTest()
    {
        _ = CustomerServiceMock.Setup(x => x.AddCustomer(It.IsAny<AddCustomerRequest>())).Throws(new Exception());

        var result = await CustomerController.AddCustomer(new AddCustomerRequest());

        Assert.That(result, Is.InstanceOf<BadRequest>());
    }
}