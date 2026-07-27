using WebApplication2.Models;
using WebApplication2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PatientsController(MyAppContext _context):  ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        return await _context.
            Patient
             .Include(p=>p.Person)
                 .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetOnePatient(long id)
    {
        var patient = await _context.Patient.Include(p => p.Person)
            .FirstOrDefaultAsync(p => p.PersonId == id);
        if (patient == null) 
        {
            return NotFound();
        }
        return patient;
    }
    public class CreatePatientRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? MedicalHistoryNotes { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(CreatePatientRequest request)
    {
        var person = new Person
        {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
            ContactNumber = request.ContactNumber,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var patient = new Patient
        {
            Person = person,
            MedicalHistoryNotes = request.MedicalHistoryNotes
        };

        _context.Patient.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOnePatient), new { id = patient.PersonId }, patient);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(long id, Patient patient)
    {
        var existing = await _context.Patient.FindAsync(id);
        if (existing == null) return NotFound();

        existing.MedicalHistoryNotes = patient.MedicalHistoryNotes;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(long id)
    {
        var patient = await _context.Patient.FindAsync(id);
        if (patient == null) return NotFound();

        _context.Patient.Remove(patient);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}