using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Patient
{
    // "PersonId" does NOT match the <ClassName>Id convention (Patient -> would need
    // "PatientId") and isn't plain "Id" either, so EF Core can't guess this is the
    // primary key on its own. [Key] is required here - this is the real use case
    // for the attribute, not decoration.
    [Key]
    public long PersonId { get; set; }
    public Person? Person { get; set; }
 
    public string? MedicalHistoryNotes { get; set; }
 
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}