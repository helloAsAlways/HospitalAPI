using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("persons")]
public class People: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}