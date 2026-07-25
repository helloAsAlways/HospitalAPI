using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("medical_records")]
public class MedicalRecords: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}