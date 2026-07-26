namespace WebApplication2.Models;


public class MedicalRecord
{
    public long Id { get; set; }
 
    public long PatientId { get; set; }
    public Patient? Patient { get; set; }
 
    public long DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
 
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
