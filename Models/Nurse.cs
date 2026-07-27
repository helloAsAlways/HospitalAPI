using  System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Nurse
{
    // Same reasoning as Doctor: "PersonId" doesn't match the <ClassName>Id
    // convention, so [Key] is required here too.
    [Key]
    public long PersonId { get; set; }
    public Person? Person { get; set; }
 
    public string Department { get; set; } = string.Empty;
}