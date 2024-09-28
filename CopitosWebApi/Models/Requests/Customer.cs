using CopitosWebApi.Models.Data;

namespace CopitosWebApi.Models.Requests;

public class AddCustomerRequest
{
    public string? Anrede { get; set; }
    public string Vorname { get; set; } = null!;
    public string Nachname { get; set; } = null!;
    public DateTime Geburtsdatum { get; set; }
    public string? Adresse { get; set; }
    public string Plz { get; set; } = null!;
    public string? Ort { get; set; }
    public string? Land { get; set; }

    public Customer ToCustomer() => new()
    {
        Anrede = Anrede,
        Vorname = Vorname,
        Nachname = Nachname,
        Geburtsdatum = Geburtsdatum,
        Adresse = Adresse,
        Plz = Plz,
        Ort = Ort,
        Land = Land
    };
}