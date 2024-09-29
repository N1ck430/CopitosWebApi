using CopitosWebApi.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ServiceTests.CustomerService;

public class UpdateCustomerTest : CustomerServiceTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerDataServiceMock.Setup(x => x.GetCustomer(It.IsAny<Guid>())).ReturnsAsync(new Customer());

        var result = await CustomerService.UpdateCustomer(new Customer());

        Assert.That(result.Result, Is.InstanceOf<Ok<bool>>());
    }

    [Test]
    public async Task NotFoundTest()
    {
        var result = await CustomerService.UpdateCustomer(new Customer());

        Assert.That(result.Result, Is.InstanceOf<NotFound>());
    }

    [Test]
    public async Task ValidationErrorTest()
    {
        _ = ValidationServiceMock.Setup(x => x.ValidateCustomer(It.IsAny<Customer>()))
            .Returns(TypedResults.ValidationProblem(new Dictionary<string, string[]>()));

        var result = await CustomerService.UpdateCustomer(new Customer());

        Assert.That(result.Result, Is.InstanceOf<ValidationProblem>());
    }
}