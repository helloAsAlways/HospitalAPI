using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentPlansController : ControllerBase
    {
        private readonly MyAppContext _context;
 
        public TreatmentPlansController(MyAppContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentPlan>>> GetTreatmentPlans()
        {
            return await _context.TreatmentPlans.Include(t => t.Diagnosis).ToListAsync();
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentPlan>> GetTreatmentPlan(long id)
        {
            var plan = await _context.TreatmentPlans
                .Include(t => t.Diagnosis)
                .FirstOrDefaultAsync(t => t.Id == id);
 
            if (plan == null) return NotFound();
            return plan;
        }
 
        [HttpPost]
        public async Task<ActionResult<TreatmentPlan>> CreateTreatmentPlan(TreatmentPlan plan)
        {
            var diagnosisExists = await _context.Diagnosis.AnyAsync(d => d.Id == plan.DiagnosisId);
            if (!diagnosisExists) return BadRequest("DiagnosisId does not exist.");
 
            _context.TreatmentPlans.Add(plan);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetTreatmentPlan), new { id = plan.Id }, plan);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatmentPlan(long id, TreatmentPlan plan)
        {
            var existing = await _context.TreatmentPlans.FindAsync(id);
            if (existing == null) return NotFound();
 
            existing.Description = plan.Description;
            existing.Duration = plan.Duration;
 
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentPlan(long id)
        {
            var plan = await _context.TreatmentPlans.FindAsync(id);
            if (plan == null) return NotFound();
 
            _context.TreatmentPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
