using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public enum AppointmentStatus
{
    Pending,
    Booked,
    Cancelled,
    Completed
}
 
public class Appointment
{
    public long Id { get; set; }
 
    public long PatientId { get; set; }
    public Patient? Patient { get; set; }
 
    public long DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
 
    public DateTimeOffset ScheduledTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; }
    public Diagnosis? Diagnosis { get; set; }
}
