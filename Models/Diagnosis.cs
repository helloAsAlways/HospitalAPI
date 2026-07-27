namespace WebApplication2.Models;

public class Diagnosis
{
    public long Id { get; set; }
 
    // One-to-one with Appointment - patient/doctor are reached via
    // diagnosis.Appointment.Patient / diagnosis.Appointment.Doctor,
    // not duplicated here.
    public long AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
 
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
 
    public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
}