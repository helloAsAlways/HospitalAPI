namespace WebApplication2.Models;

public class Nurse : Person
{
    public string Department { get; set; } = "";
    public string Shift { get; set; } = "";
    public string LicenseNumber { get; set; } = "";
}