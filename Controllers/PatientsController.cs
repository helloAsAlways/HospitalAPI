
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace WebApplication2.Controllers
{
    // Request shape for creating a patient - same reasoning as CreateDoctorRequest:
    // a Patient can't exist without a Person, so the client sends both sets of fields
    // in one request and we build the Person + Patient together.
    public class CreatePatientRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? MedicalHistoryNotes { get; set; }
    }
 
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly MyAppContext _context;
 
        public PatientsController(MyAppContext context)
        {
            _context = context;
        }
 
        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patient
                .Include(p => p.Person)
                .ToListAsync();
        }
 
        // GET: api/patients/{id}
        // id here is the PersonId, since that's Patient's primary key.
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(long id)
        {
            var patient = await _context.Patient
                .Include(p => p.Person)
                .Include(p => p.Appointments)
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.PersonId == id);
 
            if (patient == null) return NotFound();
            return patient;
        }
 
        // POST: api/patients
        // Builds Person and Patient together in one transaction - same pattern as
        // CreateDoctor. doctor.Person = person (navigation, not id) lets EF Core
        // insert the Person first, then wire up the shared PersonId automatically.
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
 
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PersonId }, patient);
        }
 
        // PUT: api/patients/{id}
        // Only MedicalHistoryNotes is editable here - PersonId can't change (it's the
        // shared key), and Name/DateOfBirth/ContactNumber live on Person, not Patient,
        // so they'd need a separate PersonsController action if you want those editable too.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(long id, Patient patient)
        {
            var existing = await _context.Patient.FindAsync(id);
            if (existing == null) return NotFound();
 
            existing.MedicalHistoryNotes = patient.MedicalHistoryNotes;
 
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        // DELETE: api/patients/{id}
        // Deletes the Patient row only. The Person row stays (someone might still be a
        // person in the system even if they stop being a patient) - if you want deleting
        // a patient to also delete their Person record, that needs an explicit extra step.
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
}
 