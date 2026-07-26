using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Patient 
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name  { get; set; } = "";
    public int? Age { get; set; }
    public string? Contact { get; set; } 
    
    public string MedicalHistory { get; set; } = "";
    public string EmergencyContact { get; set; } = "";
    public string InsuranceInfo { get; set; } = "";
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}  