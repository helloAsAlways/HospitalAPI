using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("patients")]
public class Patients: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}