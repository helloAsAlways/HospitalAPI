namespace WebApplication2.Models;

public class Doctor
{
    public long Person_Id { get; set; } // int8 in Postgres = long in C#
    public string Specialty { get; set; }
}
