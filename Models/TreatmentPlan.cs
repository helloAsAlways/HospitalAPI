using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models;

public class TreatmentPlan
{
    [Key] // primary id
    public Guid TreatmentPlanId { get; set; }
    
    // stores Diagnosis ID and Object
    public Guid DiagnosisId { get; set; }
    [ForeignKey(nameof(DiagnosisId))]
    public Diagnosis? Diagnosis { get; set; }
    
    // stores Patient ID and Object
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }
    
    // stores Doctor ID and Object
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public Doctor? Doctor { get; set; }
    
    public string TreatmentType { get; set; } = ""; // medication, surgery, therapy, lifestyle
    public string Description { get; set; } = "";
}