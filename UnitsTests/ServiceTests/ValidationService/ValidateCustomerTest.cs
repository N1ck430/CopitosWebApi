using System.ComponentModel.DataAnnotations;
using CopitosWebApi.Models;

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
        ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname"
        });

        Assert.Pass();
    }

    [Test]
    public void GeburtsdatumInFutureTest()
    {
        var exception = Assert.Throws<ValidationException>(() => ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _invalidGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname"
        }));

        Assert.That(exception.Message, Is.EqualTo("Geburtsdatum muss in der Vergangenheit liegen."));
    }

    [Test]
    public void PlzInvalid()
    {
        var exception = Assert.Throws<ValidationException>(() => ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345a",
            Nachname = "Nachname",
            Vorname = "Vorname"
        }));

        Assert.That(exception.Message, Is.EqualTo("Plz nicht valide. Sie muss auf 5 numerischen Zeichen bestehen."));
    }

    [Test]
    public void LandInvalid()
    {
        var exception = Assert.Throws<ValidationException>(() => ValidationService.ValidateCustomer(new Customer
        {
            Geburtsdatum = _validGeburtsdatum,
            Plz = "12345",
            Nachname = "Nachname",
            Vorname = "Vorname",
            Land = "123"
        }));

        Assert.That(exception.Message, Is.EqualTo("Das Land darf nur aus 2 Zeichen bestehen."));
    }
}