using CopitosWebApi.Models.Data;
using Moq;

namespace UnitsTests.ServiceTests.CustomerService;

public class GetCustomersTest : CustomerServiceTest
{
    [Test]
    public async Task SuccessTest()
    {
        _ = CustomerDataServiceMock.Setup(x => x.AllCustomers()).ReturnsAsync([new Customer()]);

        var result = await CustomerService.GetCustomers();

        Assert.That(result.Count(), Is.EqualTo(1));
    }
}