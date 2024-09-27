using CopitosWebApi.Services.DateProvider;
using CopitosWebApi.Services.Validation;
using Moq;

namespace UnitsTests.ServiceTests.ValidationService;

public class ValidationServiceTest
{
    protected IValidationService ValidationService;

    protected Mock<IDateProvider> DateProviderMock;

    [SetUp]
    public virtual void SetUp()
    {
        DateProviderMock = new Mock<IDateProvider>();

        ValidationService = new CopitosWebApi.Services.Validation.ValidationService(DateProviderMock.Object);
    }
}