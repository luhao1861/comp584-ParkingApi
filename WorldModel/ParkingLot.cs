using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingModel;

namespace ParkingModel;

public class ParkingLot
{
    public int Id { get; set; }                
    
    [StringLength(50)]
    public string Name { get; set; } = null!;         
    public int Capacity { get; set; }          
    public int AvailableSpaces { get; set; }  
    //public virtual ICollection<Car> Cars { get; } = new List<Car>();
}