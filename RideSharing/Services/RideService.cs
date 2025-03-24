using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing.Models;

namespace RideSharing.Services
{
    public class RideService
    {
        public List<Ride> AvailableRides { get; set; } = new List<Ride>();
        public List<Driver> Drivers { get; set; } = new List<Driver>();

        public void RequestRide(Passenger passenger, string pickupLocation, string dropoffLocation)
        {    
            //The request ride method creates an instance of ride with the provided params,and the calculatedFare Method  provided that there is any availabe drivers  
            var availableDrivers = Drivers.Where(d => d.AvailabilityStatus).ToList();

            if (availableDrivers.Any())
            {
                var ride = new Ride
                {
                    RideId = AvailableRides.Count + 1,
                    Passenger = passenger,
                    PickupLocation = pickupLocation,
                    DropoffLocation = dropoffLocation,
                    Fare = CalculateFare(pickupLocation, dropoffLocation),
                    Status = "Pending"
                };
                AvailableRides.Add(ride);
                Console.WriteLine("Ride requested successfully. Waiting for a driver to accept...");
            }
            else
            {
                throw new Exception("No available drivers.");
            }
        }

        private decimal CalculateFare(string pickupLocation, string dropoffLocation)
        {
          //This methods simulates a fare that will be charged based on the Km of a ride, it uses the ramdon class to generate an anmount
            Random random = new Random();
            return random.Next(10,500);
        
        }
    }
}
