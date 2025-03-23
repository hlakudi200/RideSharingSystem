using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Models
{
   public class Driver:User
    {
        public bool AvailabilityStatus { get; set; } = true;
        public decimal Earnings { get; set; }
        public double Rating { get; set; }
        public int TotalRides { get; internal set; }
    }
}
