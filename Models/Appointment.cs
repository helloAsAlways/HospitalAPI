using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Appointment
{
    public Guid AppointmentId { get; set; } = Guid.NewGuid();
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public Doctor? Doctor { get; set; }
    
    public DateTime ScheduledAt { get; set; }
    public string Status { get; set; } = "pending";
}