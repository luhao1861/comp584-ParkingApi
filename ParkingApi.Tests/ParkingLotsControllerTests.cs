using Xunit;
using Microsoft.EntityFrameworkCore;
using ParkingModel;
using Parking.Controllers;
using System.Linq;
using System.Threading.Tasks;

public class ParkingLotsControllerTests
{
    private ParkingLotsController _controller;
    private ParkingContext _context;

    public ParkingLotsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ParkingContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") 
            .Options;

        _context = new ParkingContext(options);
        _controller = new ParkingLotsController(_context);
    }

    [Fact]
    public async Task GetParkingLots_ReturnsAllParkingLots()
    {
        // Arrange
        _context.ParkingLots.Add(new ParkingLot { Id = 10000, Name = "Test" });
        _context.SaveChanges();

        // Act
        var result = await _controller.GetParkingLots();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
    }

    [Fact]
    public async Task GetParkingLot_ReturnsCorrectParkingLot()
    {
        // Arrange
        var testParkingLot = new ParkingLot { Id = 10000, Name = "Test" }; 
        _context.ParkingLots.Add(testParkingLot);
        _context.SaveChanges();

        // Act
        var result = await _controller.GetParkingLot(10000);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal("Test", result.Value.Name);
    }

    [Fact]
    public async Task DeleteParkingLot_RemovesParkingLotFromDatabase()
    {
        // Arrange
        var testParkingLot = new ParkingLot { Id = 10000, Name = "Test" }; 
        _context.ParkingLots.Add(testParkingLot);
        _context.SaveChanges();

        // Act
        var result = await _controller.DeleteParkingLot(10000);
        var parkingLot = _context.ParkingLots.FirstOrDefault(p => p.Id == 10000);

        // Assert
        Assert.Null(parkingLot);
    }

}
