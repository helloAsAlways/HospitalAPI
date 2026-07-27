using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Person
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? ContactNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
 
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public Nurse? Nurse { get; set; }
}