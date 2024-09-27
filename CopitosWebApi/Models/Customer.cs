using System.ComponentModel.DataAnnotations;

namespace CopitosWebApi.Models;

public class Customer
{
    public string? Anrede { get; set; }
    [Required] public string Vorname { get; set; } = null!;
    [Required] public string Nachname { get; set; } = null!;
    [Required] public DateTime Geburtsdatum { get; set; }
    public string? Adresse { get; set; }
    [Required] public string Plz { get; set; } = null!;
    public string? Ort { get; set; }
    public string? Land { get; set; }
}