using CopitosWebApi.Models.Data;
using CopitosWebApi.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace UnitsTests.ServiceTests.CustomerService;

public class AddCustomerTest : CustomerServiceTest
{
    [Test]
    public async Task SuccessTest()
    {
        var result = await CustomerService.AddCustomer(new AddCustomerRequest());

        Assert.That(result.Result, Is.InstanceOf<Ok<bool>>());
    }

    [Test]
    public async Task ValidationFailedTest()
    {
        _ = ValidationServiceMock.Setup(x => x.ValidateCustomer(It.IsAny<Customer>()))
            .Returns(TypedResults.ValidationProblem(new Dictionary<string, string[]>()));

        var result = await CustomerService.AddCustomer(new AddCustomerRequest());

        Assert.That(result.Result, Is.InstanceOf<ValidationProblem>());
    }
}