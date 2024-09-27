namespace CopitosWebApi.Models;

public class CustomerResponse : Customer
{
    public Guid Id { get; set; }

    public CustomerResponse(Guid id, Customer customer)
    {
        Id = id;
        Anrede = customer.Anrede;
        Vorname = customer.Vorname;
        Nachname = customer.Nachname;
        Geburtsdatum = customer.Geburtsdatum;
        Adresse = customer.Adresse;
        Plz = customer.Plz;
        Ort = customer.Ort;
        Land = customer.Land;
    }
}