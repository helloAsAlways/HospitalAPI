using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Diagnosis
{
    [Key]
    public Guid DiagnosisId { get; set; }
    public Guid AppointmentId { get; set; }
    [ForeignKey(nameof(AppointmentId))]
    public Appointment? Appointment { get; set; }
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public  Patient? Patient { get; set; }
    
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public Doctor? Doctor { get; set; }
    
    public DateTime DiagnosisDate { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = "";
    public string Severity { get; set; } = ""; // mild, moderate, severe, critical
    
    public ICollection<TreatmentPlan> TreatementPlans { get; set; } = new List<TreatmentPlan>();
}