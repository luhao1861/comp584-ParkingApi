using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingModel;

namespace Parking.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly ParkingContext _context; 

        public ParkingLotsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: api/ParkingLots
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ParkingLot>>> GetParkingLots()
        {
            return await _context.ParkingLots.ToListAsync();
        }

        // GET: api/ParkingLots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLot>> GetParkingLot(int id)
        {
            ParkingLot? parkingLot = await _context.ParkingLots.FindAsync(id);
            return parkingLot == null ? NotFound() : parkingLot;
        }

/*        [HttpGet("findParkingLot/{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetCountryPopulation(int id)
        {
            ParkingLotDto? parkingLotDto = await _context.ParkingLots.Where(c => c.Id == id)
                .Select(c => new ParkingLotDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Cars = c.Cars
                }).SingleOrDefaultAsync();
            return parkingLotDto is null ? NotFound() : parkingLotDto;
        }*/

        // PUT: api/ParkingLots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingLot(int id, ParkingLot parkingLot)
        {
            if (id != parkingLot.Id)
            {
                return BadRequest();
            }

            _context.Entry(parkingLot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingLotExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/ParkingLots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParkingLot>> PostParkingLot(ParkingLot parkingLot)
        {
            _context.ParkingLots.Add(parkingLot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingLot", new { id = parkingLot.Id }, parkingLot);
        }

        // DELETE: api/ParkingLots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingLot(int id)
        {
            ParkingLot? parkingLot = await _context.ParkingLots.FindAsync(id);
            if (parkingLot == null)
            {
                return NotFound();
            }

            _context.ParkingLots.Remove(parkingLot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingLotExists(int id) => _context.ParkingLots.Any(e => e.Id == id);
    }
}
