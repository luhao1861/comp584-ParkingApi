using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingModel;

namespace Parking.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ParkingContext _context;

        public CarsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(car => car.ParkingLot).ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            Car? car = await _context.Cars.Include(car => car.ParkingLot).SingleOrDefaultAsync(car => car.Id == id);
            return car == null ? NotFound() : car;
        }



        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (car.ParkingLot != null && car.ParkingLot.Id != 0)
            {
                // If ParkingLot is not null and the Id is set (meaning it exists in the database),
                // then attach it to the DbContext so EntityFramework knows not to insert a new one.
                _context.ParkingLots.Attach(car.ParkingLot);
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }


        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            Car? car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id) => _context.Cars.Any(e => e.Id == id);
    }
}
