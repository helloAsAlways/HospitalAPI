using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController(MyAppContext _context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient).ThenInclude(p => p!.Person)
                .Include(m => m.Doctor).ThenInclude(d => d!.Person)
                .ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(long id)
        {
            var record = await _context.MedicalRecords
                .Include(m => m.Patient).ThenInclude(p => p!.Person)
                .Include(m => m.Doctor).ThenInclude(d => d!.Person)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (record == null) return NotFound();

            return record;
        }
        
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord(MedicalRecord record)
        {
            var patientExists = await _context.Patient.AnyAsync(p => p.PersonId == record.PatientId);
            if (!patientExists) return BadRequest("PatientId does not exist.");

            var doctorExists = await _context.Doctor.AnyAsync(d => d.PersonId == record.DoctorId);
            if (!doctorExists) return BadRequest("DoctorId does not exist.");

            record.CreatedAt = DateTimeOffset.UtcNow;

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = record.Id }, record);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(long id, MedicalRecord record)
        {
            var existing = await _context.MedicalRecords.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Notes = record.Notes;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}