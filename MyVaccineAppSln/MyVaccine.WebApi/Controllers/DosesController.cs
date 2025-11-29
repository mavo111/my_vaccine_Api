using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Data;
using MyVaccine.WebApi.Models;


namespace MyVaccine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DosesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/doses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dose>>> GetDoses()
        {
            return await _context.Doses
                .Include(d => d.Vaccine)
                .ToListAsync();
        }

        // GET: api/doses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dose>> GetDose(int id)
        {
            var dose = await _context.Doses
                .Include(d => d.Vaccine)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dose == null)
            {
                return NotFound();
            }

            return dose;
        }

        // POST: api/doses
        [HttpPost]
        public async Task<ActionResult<Dose>> PostDose(Dose dose)
        {
            _context.Doses.Add(dose);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDose", new { id = dose.Id }, dose);
        }

        // PUT: api/doses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDose(int id, Dose dose)
        {
            if (id != dose.Id)
            {
                return BadRequest();
            }

            _context.Entry(dose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Doses.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/doses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDose(int id)
        {
            var dose = await _context.Doses.FindAsync(id);
            if (dose == null)
            {
                return NotFound();
            }

            _context.Doses.Remove(dose);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
