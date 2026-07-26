using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Doctor 
{
    public long Id { get; set; }
    public string Specialty { get; set; } = "";
    public string LicenseNumber { get; set; } = "";
    public string Department { get; set; } = "";
    public int YearsExperience { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}