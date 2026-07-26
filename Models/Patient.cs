namespace WebApplication2.Models;

public class Patient
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public string ContactNumber { get; set; } = "";
}