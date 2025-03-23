using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing.Models;

namespace RideSharing.Interfaces
{
    public interface IRideable
    {
        void RequestRide(string pickupLocation, string dropoffLocation);
        void AcceptRide(Ride ride);

    }
}
