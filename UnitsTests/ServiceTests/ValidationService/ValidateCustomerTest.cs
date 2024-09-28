using CopitosWebApi.Models.Data;

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
            Assert.That(error.Value[0], Is.EqualTo("Geburtsdatum muss in der Vergangenheit liegen."));
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
            Assert.That(error.Value[0], Is.EqualTo("Plz nicht valide. Sie muss auf 5 numerischen Zeichen bestehen."));
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
            Assert.That(error.Value[0], Is.EqualTo("Wenn gefüllt, darf das Land nur aus 2 Zeichen bestehen."));
        });
    }
}