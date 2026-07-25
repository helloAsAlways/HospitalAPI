using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("person")]
public class Person: BaseModel
{
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}