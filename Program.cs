using System;
using System.Collections.Generic;
using System.Linq;
using RideSharing.Models;
using RideSharing.Services;

class Program
{
    static List<Passenger> Passengers = new List<Passenger>();
    static List<Driver> Drivers = new List<Driver>();
    static RideService RideService = new RideService();
    static PaymentService PaymentService = new PaymentService();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Ride-Sharing System!");
            Console.WriteLine("1. Register as Passenger");
            Console.WriteLine("2. Register as Driver");
            Console.WriteLine("3. Login");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterPassenger();
                    break;
                case "2":
                    RegisterDriver();
                    break;
                case "3":
                    Login();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void RegisterPassenger()
    {
        Console.Write("Enter Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Console.ReadLine();

        var passenger = new Passenger
        {
            Id = Passengers.Count + 1,
            Name = name,
            Email = email,
            Password = password
        };
        Passengers.Add(passenger);
        Console.WriteLine("Passenger registered successfully!");
    }

    static void RegisterDriver()
    {
        Console.Write("Enter Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Console.ReadLine();

        var driver = new Driver
        {
            Id = Drivers.Count + 1,
            Name = name,
            Email = email,
            Password = password,
      
            
        };
        Drivers.Add(driver);
        RideService.Drivers.Add(driver);
        Console.WriteLine("Driver registered successfully!");
    }

    static void Login()
    {
        Console.Write("Enter Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Console.ReadLine();

        var passenger = Passengers.FirstOrDefault(p => p.Email == email && p.Password == password);
        var driver = Drivers.FirstOrDefault(d => d.Email == email && d.Password == password);

        if (passenger != null)
        {
            PassengerMenu(passenger);
        }
        else if (driver != null)
        {
            DriverMenu(driver);
        }
        else
        {
            Console.WriteLine("Invalid email or password.");
        }
    }

    static void PassengerMenu(Passenger passenger)
    {
        while (true)
        {
            Console.WriteLine("Passenger Menu");
            Console.WriteLine("1. Request a Ride");
            Console.WriteLine("2. View Wallet Balance");
            Console.WriteLine("3. Add Funds to Wallet");
            Console.WriteLine("4. View Ride History");
            Console.WriteLine("5. Rate a Driver");
            Console.WriteLine("6. Logout");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Pickup Location: ");
                    var pickup = Console.ReadLine();
                    Console.Write("Enter Dropoff Location: ");
                    var dropoff = Console.ReadLine();
                    try
                    {
                        RideService.RequestRide(passenger, pickup, dropoff);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "2":
                    Console.WriteLine($"Wallet Balance: {passenger.WalletBalance}");
                    break;
                case "3":
                    Console.Write("Enter Amount to Add: ");
                    var amount = decimal.Parse(Console.ReadLine());
                    PaymentService.AddFunds(passenger, amount);
                    break;
                case "4":
                    Console.WriteLine("Ride History:");
                    foreach (var ride in passenger.RideHistory)
                    {
                        Console.WriteLine($"Ride ID: {ride.RideId}, Fare: {ride.Fare}, Status: {ride.Status}");
                    }
                    break;
                case "5":
                    Console.Write("Enter Ride ID to Rate: ");
                    var rideId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Rating (1-5): ");
                    var rating = double.Parse(Console.ReadLine());
                    var rideToRate = passenger.RideHistory.FirstOrDefault(r => r.RideId == rideId);
                    if (rideToRate != null)
                    {
                        rideToRate.Driver.Rating = (rideToRate.Driver.Rating + rating) / 2;
                        Console.WriteLine("Driver rated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Ride ID.");
                    }
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void DriverMenu(Driver driver)
    {
        while (true)
        {
            Console.WriteLine("Driver Menu");
            Console.WriteLine("1. View Available Ride Requests");
            Console.WriteLine("2. Accept a Ride");
            Console.WriteLine("3. Complete a Ride");
            Console.WriteLine("4. View Earnings");
            Console.WriteLine("5. Logout");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Available Ride Requests:");
                    foreach (var ride in RideService.AvailableRides)
                    {
                        Console.WriteLine($"Ride ID: {ride.RideId}, Pickup: {ride.PickupLocation}, Dropoff: {ride.DropoffLocation}, Fare: {ride.Fare}");
                    }
                    break;
                case "2":
                    Console.Write("Enter Ride ID to Accept: ");
                    var rideId = int.Parse(Console.ReadLine());
                    var rideToAccept = RideService.AvailableRides.FirstOrDefault(r => r.RideId == rideId);
                    if (rideToAccept != null)
                    {
                        rideToAccept.Driver = driver;
                        rideToAccept.Status = "Accepted";
                        driver.AvailabilityStatus = false;
                        Console.WriteLine("Ride accepted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Ride ID.");
                    }
                    break;
                case "3":
                    Console.Write("Enter Ride ID to Complete: ");
                    var rideIdComplete = int.Parse(Console.ReadLine());
                    var rideToComplete = RideService.AvailableRides.FirstOrDefault(r => r.RideId == rideIdComplete);
                    if (rideToComplete != null)
                    {
                        rideToComplete.Status = "Completed";
                        driver.Earnings += rideToComplete.Fare;
                        driver.TotalRides++;
                        driver.AvailabilityStatus = true;
                        rideToComplete.Passenger.RideHistory.Add(rideToComplete);
                        RideService.AvailableRides.Remove(rideToComplete);
                        Console.WriteLine("Ride completed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Ride ID.");
                    }
                    break;
                case "4":
                    Console.WriteLine($"Total Earnings: {driver.Earnings}");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}