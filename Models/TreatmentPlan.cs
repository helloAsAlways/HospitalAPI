namespace WebApplication2.Models;

public class TreatmentPlan
{
    public long Id { get; set; }
 
    public long DiagnosisId { get; set; }
    public Diagnosis? Diagnosis { get; set; }
 
    public string? Description { get; set; }
    public string? Duration { get; set; }
}