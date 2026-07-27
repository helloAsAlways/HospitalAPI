using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosesController : ControllerBase
    {
        private readonly MyAppContext _context;
 
        public DiagnosesController(MyAppContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnosis>>> GetDiagnoses()
        {
            return await _context.Diagnosis
                .Include(d => d.Appointment)
                .Include(d => d.TreatmentPlans)
                .ToListAsync();
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnosis>> GetDiagnosis(long id)
        {
            var diagnosis = await _context.Diagnosis
                .Include(d => d.Appointment)
                .Include(d => d.TreatmentPlans)
                .FirstOrDefaultAsync(d => d.Id == id);
 
            if (diagnosis == null) return NotFound();
            return diagnosis;
        }
 
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> CreateDiagnosis(Diagnosis diagnosis)
        {
            var appointmentExists = await _context.Appointment.AnyAsync(a => a.Id == diagnosis.AppointmentId);
            if (!appointmentExists) return BadRequest("AppointmentId does not exist.");
 
            // Enforces the one-to-one relationship at the application level too
            // (the database's unique constraint on appointment_id is the real guarantee).
            var alreadyHasDiagnosis = await _context.Diagnosis.AnyAsync(d => d.AppointmentId == diagnosis.AppointmentId);
            if (alreadyHasDiagnosis) return Conflict("This appointment already has a diagnosis.");
 
            _context.Diagnosis.Add(diagnosis);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetDiagnosis), new { id = diagnosis.Id }, diagnosis);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnosis(long id, Diagnosis diagnosis)
        {
            var existing = await _context.Diagnosis.FindAsync(id);
            if (existing == null) return NotFound();
 
            existing.Description = diagnosis.Description;
            existing.Date = diagnosis.Date;
 
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis(long id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);
            if (diagnosis == null) return NotFound();
 
            _context.Diagnosis.Remove(diagnosis);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
