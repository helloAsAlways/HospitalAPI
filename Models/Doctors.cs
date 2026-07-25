using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace WebApplication2.Models;


[Table("doctors")]
public class Doctors: BaseModel
{
    [PrimaryKey("doctor_table_id", false)]
    public long Doctor_Table_Id { get; set; } // int8 in Postgres = long in C#
    [Column("speciality")]
    public string Speciality { get; set; } = string.Empty;
}
