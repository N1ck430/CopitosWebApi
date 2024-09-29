using CopitosWebApi.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ServiceTests.CustomerService;

public class DeleteCustomerTest : CustomerServiceTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerDataServiceMock.Setup(x => x.GetCustomer(It.IsAny<Guid>())).ReturnsAsync(new Customer());

        var result = await CustomerService.DeleteCustomer(Guid.NewGuid());

        Assert.That(result.Result, Is.InstanceOf<Ok<bool>>());
    }

    [Test]
    public async Task NotFoundTest()
    {
        var result = await CustomerService.DeleteCustomer(Guid.NewGuid());

        Assert.That(result.Result, Is.InstanceOf<NotFound>());
    }
}