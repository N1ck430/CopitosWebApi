using CopitosWebApi.Services.CustomerDataService;
using CopitosWebApi.Services.CustomerService;
using CopitosWebApi.Services.Validation;
using Moq;

namespace UnitsTests.ServiceTests.CustomerService;

public class CustomerServiceTest
{
    protected ICustomerService CustomerService;

    protected Mock<IValidationService> ValidationServiceMock;
    protected Mock<ICustomerDataService> CustomerDataServiceMock;

    [SetUp]
    public void SetUp()
    {
        ValidationServiceMock = new Mock<IValidationService>();
        CustomerDataServiceMock = new Mock<ICustomerDataService>();

        CustomerService =
            new CopitosWebApi.Services.CustomerService.CustomerService(ValidationServiceMock.Object,
                CustomerDataServiceMock.Object);
    }
}