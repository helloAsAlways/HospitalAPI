using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("doctor")]
public class Doctor: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}