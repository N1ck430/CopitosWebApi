using CopitosWebApi.Services.CustomerService;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitsTests.ControllerTests.CustomerController;

public class CustomerControllerTest
{
    protected CopitosWebApi.Controllers.CustomerController CustomerController;

    protected Mock<ILogger<CopitosWebApi.Controllers.CustomerController>> LoggerMock;
    protected Mock<ICustomerService> CustomerServiceMock;

    [SetUp]
    public virtual void SetUp()
    {
        LoggerMock = new Mock<ILogger<CopitosWebApi.Controllers.CustomerController>>();
        CustomerServiceMock = new Mock<ICustomerService>();

        CustomerController =
            new CopitosWebApi.Controllers.CustomerController(LoggerMock.Object, CustomerServiceMock.Object);
    }
}