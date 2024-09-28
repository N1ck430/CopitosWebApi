namespace CopitosWebApi.Models.Data;

public class Customer
{
    public Guid Id { get; set; }
    public string? Anrede { get; set; }
    public string Vorname { get; set; } = null!;
    public string Nachname { get; set; } = null!;
    public DateTime Geburtsdatum { get; set; }
    public string? Adresse { get; set; }
    public string Plz { get; set; } = null!;
    public string? Ort { get; set; }
    public string? Land { get; set; }
}