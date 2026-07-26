using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApplication2.Controllers;

public class CreateDoctorRequest
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? ContactNumber { get; set; }
    public string Speciality { get; set; } = string.Empty;
}

[ApiController]
[Route("api/[controller]")]
public class DoctorController(MyAppContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
    {
        return await context.Doctor.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(long id)
    {
        var doctor = await context.Doctor.FindAsync(id);

        if (doctor == null) return NotFound();

        return doctor;
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> CreateDoctor(CreateDoctorRequest request)
    {
        var person = new Person
        {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
            ContactNumber = request.ContactNumber,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var doctor = new Doctor
        {
            Person = person,
            Speciality = request.Speciality
        };

        context.Doctor.Add(doctor);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDoctor), new { id = doctor.PersonId }, doctor);
    }
}