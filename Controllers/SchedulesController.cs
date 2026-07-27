using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly MyAppContext _context;
 
        public SchedulesController(MyAppContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _context.Schedules
                .Include(s => s.Doctor).ThenInclude(d => d!.Person)
                .ToListAsync();
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(long id)
        {
            var schedule = await _context.Schedules
                .Include(s => s.Doctor).ThenInclude(d => d!.Person)
                .FirstOrDefaultAsync(s => s.Id == id);
 
            if (schedule == null) return NotFound();
            return schedule;
        }
 
        [HttpPost]
        public async Task<ActionResult<Schedule>> CreateSchedule(Schedule schedule)
        {
            var doctorExists = await _context.Doctor.AnyAsync(d => d.PersonId == schedule.DoctorId);
            if (!doctorExists) return BadRequest("DoctorId does not exist.");
 
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, schedule);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(long id, Schedule schedule)
        {
            var existing = await _context.Schedules.FindAsync(id);
            if (existing == null) return NotFound();
 
            existing.Date = schedule.Date;
            existing.StartTime = schedule.StartTime;
            existing.EndTime = schedule.EndTime;
 
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(long id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return NotFound();
 
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}