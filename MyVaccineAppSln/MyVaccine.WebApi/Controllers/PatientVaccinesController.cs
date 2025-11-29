using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Data;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientVaccinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientVaccinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientVaccines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientVaccine>>> GetPatientVaccines()
        {
            return await _context.PatientVaccines
                .Include(pv => pv.Patient)
                .Include(pv => pv.Vaccine)
                .Include(pv => pv.Dose)
                .ToListAsync();
        }

        // GET: api/PatientVaccines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientVaccine>> GetPatientVaccine(int id)
        {
            var pv = await _context.PatientVaccines
                .Include(pv => pv.Patient)
                .Include(pv => pv.Vaccine)
                .Include(pv => pv.Dose)
                .FirstOrDefaultAsync(pv => pv.Id == id);

            if (pv == null)
            {
                return NotFound();
            }

            return pv;
        }

        // POST: api/PatientVaccines
        [HttpPost]
        public async Task<ActionResult<PatientVaccine>> PostPatientVaccine(PatientVaccine patientVaccine)
        {
            _context.PatientVaccines.Add(patientVaccine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientVaccine", new { id = patientVaccine.Id }, patientVaccine);
        }

        // PUT: api/PatientVaccines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientVaccine(int id, PatientVaccine patientVaccine)
        {
            if (id != patientVaccine.Id)
            {
                return BadRequest();
            }

            _context.Entry(patientVaccine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientVaccineExists(id))
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

        // DELETE: api/PatientVaccines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientVaccine(int id)
        {
            var pv = await _context.PatientVaccines.FindAsync(id);
            if (pv == null)
            {
                return NotFound();
            }

            _context.PatientVaccines.Remove(pv);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientVaccineExists(int id)
        {
            return _context.PatientVaccines.Any(e => e.Id == id);
        }
    }
}
