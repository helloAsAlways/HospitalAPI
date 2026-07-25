using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("appointments")]
public class Appointments: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}