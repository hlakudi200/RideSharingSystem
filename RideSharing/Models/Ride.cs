using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Models
{
 public class Ride
{
    public int RideId { get; set; }
    public string PickupLocation { get; set; }
    public string DropoffLocation { get; set; }
    public Passenger Passenger { get; set; }
    public Driver Driver { get; set; }
    public decimal Fare { get; set; }
    public string Status { get; set; } = "Pending";
}
}
