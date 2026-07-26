using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models;

public class Schedule
{
    [Key] // primary key
    public Guid ScheduleId  { get; set; } = Guid.NewGuid();
    
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public Doctor? Doctor { get; set; }

    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }
    
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}