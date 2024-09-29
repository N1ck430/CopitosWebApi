using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ControllerTests.CustomerController;

public class DeleteCustomerTest : CustomerControllerTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerServiceMock.Setup(x => x.DeleteCustomer(It.IsAny<Guid>())).ReturnsAsync(TypedResults.Ok(true));

        var response = await CustomerController.DeleteCustomer(Guid.NewGuid());

        Assert.That(response, Is.InstanceOf<Results<Ok<bool>, NotFound>>());
    }

    [Test]
    public async Task ExceptionTest()
    {
        _ = CustomerServiceMock.Setup(x => x.DeleteCustomer(It.IsAny<Guid>())).Throws(new Exception());

        var response = await CustomerController.DeleteCustomer(Guid.NewGuid());

        Assert.That(response, Is.InstanceOf<BadRequest>());
    }
}