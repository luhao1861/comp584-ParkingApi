using Xunit;
using Microsoft.EntityFrameworkCore;
using ParkingModel;
using Parking.Controllers;

public class CarsControllerTests
{
    private CarsController _controller;
    private ParkingContext _context;

    public CarsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ParkingContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") 
            .Options;

        _context = new ParkingContext(options);
        _controller = new CarsController(_context);
    }

    [Fact]
    public async Task GetCars_ReturnsAllCars()
    {
        // Arrange
        _context.Cars.Add(new Car { Id = 10000, Brand = "Test", Year = 2014 });
        _context.SaveChanges();

        // Act
        var result = await _controller.GetCars();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
    }


}
