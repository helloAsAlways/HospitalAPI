using WebApplication2.Models;
using WebApplication2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AppointmentsController(MyAppContext _context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return await _context.Appointment
            .Include(a => a.Patient).ThenInclude(p => p!.Person)
            .Include(a => a.Doctor).ThenInclude(d => d!.Person)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(long id)
    {
        var appointment = await _context.Appointment.Include(a => a.Patient).ThenInclude(p => p!.Person)
            .Include(a => a.Doctor).ThenInclude(d => d!.Person)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }
        return appointment;
}

    [HttpPost]
    public async Task<ActionResult<Appointment>> BookAppointment(Appointment appointment)
    {
        var patientExists = await _context.Patient.AnyAsync(p => p.PersonId == appointment.PatientId);
        if (!patientExists) return BadRequest("PatientId does not exist. ");
        
        var doctorExists = await _context.Doctor.AnyAsync(d => d.PersonId == appointment.DoctorId);
        if (!doctorExists) return BadRequest("DoctorId does not exist. ")

        appointment.Status = AppointmentStatus.Booked;
        appointment.CreatedAt = DateTimeOffset.UtcNow;
        
        _context.Appointment.Add(appointment);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
    }
}
    
