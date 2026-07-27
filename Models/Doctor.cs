using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Doctor
{
    [Key]
    public long PersonId { get; set; }
    public Person? Person { get; set; }
 
    public string Speciality { get; set; } = string.Empty;
 
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    public IEnumerable<Schedule>? Schedules { get; set; } = new  List<Schedule>();
}