using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using MyVaccine.WebApi.Data;

using MyVaccine.WebApi.Models;



namespace MyVaccine.WebApi.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class PatientsController : ControllerBase

    {

        private readonly ApplicationDbContext _context;



        public PatientsController(ApplicationDbContext context)

        {

            _context = context;

        }



        // GET: api/patients

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()

        {

            return await _context.Patients.ToListAsync();

        }



        // GET: api/patients/5

        [HttpGet("{id}")]

        public async Task<ActionResult<Patient>> GetPatient(int id)

        {

            var patient = await _context.Patients.FindAsync(id);



            if (patient == null)

            {

                return NotFound();

            }



            return patient;

        }



        // POST: api/patients

        [HttpPost]

        public async Task<ActionResult<Patient>> PostPatient(Patient patient)

        {

            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();



            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);

        }



        // PUT: api/patients/5

        [HttpPut("{id}")]

        public async Task<IActionResult> PutPatient(int id, Patient patient)

        {

            if (id != patient.Id)

            {

                return BadRequest();

            }



            _context.Entry(patient).State = EntityState.Modified;



            try

            {

                await _context.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException)

            {

                if (!_context.Patients.Any(e => e.Id == id))

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



        // DELETE: api/patients/5

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePatient(int id)

        {

            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)

            {

                return NotFound();

            }



            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();



            return NoContent();

        }

    }

}