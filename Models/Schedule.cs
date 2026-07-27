namespace WebApplication2.Models;

public class Schedule

{
    public long Id { get; set; }
 
    public long DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
 
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}