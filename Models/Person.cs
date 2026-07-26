using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public string ContactNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string Address { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}