using Microsoft.AspNetCore.Mvc;

namespace UnitsTests.ControllerTests.CustomerController;

public class GetCustomersTest : CustomerControllerTest
{
    [Test]
    public async Task SuccessTest()
    {
        var response = await CustomerController.GetCustomers();

        Assert.That(response, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task ExceptionTest()
    {
        _ = CustomerServiceMock.Setup(x => x.GetCustomers()).Throws(new Exception());

        var response = await CustomerController.GetCustomers();

        Assert.That(response, Is.InstanceOf<BadRequestResult>());
    }
}