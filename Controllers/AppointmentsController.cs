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
          appointment.Status = AppointmentStatus.Booked;
        appointment.CreatedAt = DateTimeOffset.UtcNow;

        _context.Appointment.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(long id, Appointment appointment)
    {
        var existing = await _context.Appointment.FindAsync(id);
        if (existing == null) return NotFound();
        
        existing.ScheduledTime = appointment.ScheduledTime;
        existing.Status = appointment.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }
     [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return NotFound();
 
            appointment.Status = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(long id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return NotFound();
 
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

}
    
