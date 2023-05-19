using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ParkingModel;

public class Car
{
    public int Id { get; set; }
    [StringLength(32)]
    public string LicensePlate { get; set; } = null!;
    [StringLength(32)]
    public string Brand { get; set; } = null!;
    [StringLength(32)]
    public string Model { get; set; } = null!;
    public int Year { get; set; }    
    
    [StringLength(32)]
    public string Color { get; set; } = null!;      
    public ParkingLot ParkingLot { get; set; } = null!;     
}