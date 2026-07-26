namespace WebApplication2.Models;

public class Doctors : Person
{
    public string Specialty { get; set; } = "";
    public string LicenseNumber { get; set; } = "";
    public string Department { get; set; } = "";
    public int YearsExperience { get; set; }
}