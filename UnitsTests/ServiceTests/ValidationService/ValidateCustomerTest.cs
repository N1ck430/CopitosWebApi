using CopitosWebApi.Models.Data;
using CopitosWebApi.Resources;

namespace UnitsTests.ServiceTests.ValidationService;

public class ValidateCustomerTest : ValidationServiceTest
{
    private readonly DateTime _validGeburtsdatum = new(2024, 9, 26);
    private readonly DateTime _invalidGeburtsdatum = new(2024, 9, 28);
    private readonly DateTime _currentTime = new(2024, 9, 27);

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        DateProviderMock.Setup(x => x.UtcNow).Returns(_currentTime);
    }

    [Test]
    public void SuccessTest()
    {
        var result = ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname"
        });

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GeburtsdatumInFutureTest()
    {
        var result = ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _invalidGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname"
        });

        Assert.That(result, Is.Not.Null);

        var error = result.ProblemDetails.Errors.First();
        
        Assert.Multiple(() =>
        {
            Assert.That(error.Key, Is.EqualTo(nameof(Customer.Geburtsdatum)));
            Assert.That(error.Value[0], Is.EqualTo(Texts.GeburtsdatumValidationError));
        });
    }

    [Test]
    public void PlzInvalid()
    {
        var result = ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345a",
            Nachname = "Nachname",
            Vorname = "Vorname"
        });

        Assert.That(result, Is.Not.Null);

        var error = result.ProblemDetails.Errors.First();

        Assert.Multiple(() =>
        {
            Assert.That(error.Key, Is.EqualTo(nameof(Customer.Plz)));
            Assert.That(error.Value[0], Is.EqualTo(Texts.PlzValidationError));
        });
    }

    [Test]
    public void LandInvalid()
    {
        var result = ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname",
            Land = "123"
        });

        Assert.That(result, Is.Not.Null);

        var error = result.ProblemDetails.Errors.First();

        Assert.Multiple(() =>
        {
            Assert.That(error.Key, Is.EqualTo(nameof(Customer.Land)));
            Assert.That(error.Value[0], Is.EqualTo(Texts.LandValidationError));
        });
    }
}