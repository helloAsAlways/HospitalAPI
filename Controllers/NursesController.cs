using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace WebApplication2.Controllers
{
    public class CreateNurseRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string Department { get; set; } = string.Empty;
    }
 
    [ApiController]
    [Route("api/[controller]")]
    public class NursesController : ControllerBase
    {
        private readonly MyAppContext _context;
 
        public NursesController(MyAppContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nurse>>> GetNurses()
        {
            return await _context.Nurses.Include(n => n.Person).ToListAsync();
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<Nurse>> GetNurse(long id)
        {
            var nurse = await _context.Nurses
                .Include(n => n.Person)
                .FirstOrDefaultAsync(n => n.PersonId == id);
 
            if (nurse == null) return NotFound();
            return nurse;
        }
 
        [HttpPost]
        public async Task<ActionResult<Nurse>> CreateNurse(CreateNurseRequest request)
        {
            var person = new Person
            {
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                ContactNumber = request.ContactNumber,
                CreatedAt = DateTimeOffset.UtcNow
            };
 
            var nurse = new Nurse
            {
                Person = person,
                Department = request.Department
            };
 
            _context.Nurses.Add(nurse);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetNurse), new { id = nurse.PersonId }, nurse);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNurse(long id, Nurse nurse)
        {   
            var existing = await _context.Nurses.FindAsync(id);
            if (existing == null) return NotFound();
 
            existing.Department = nurse.Department;
 
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurse(long id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse == null) return NotFound();
 
            _context.Nurses.Remove(nurse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
