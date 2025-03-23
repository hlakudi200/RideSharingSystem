using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing.Interfaces;
using RideSharing.Models;

namespace RideSharing.Services
{
    public class PaymentService : IPayable
    {
        public void AddFunds(Passenger passenger, decimal amount)
        {
            passenger.WalletBalance += amount;
            Console.WriteLine($"Added {amount} to wallet. New balance: {passenger.WalletBalance}");
        }

        public void DeductFare(Passenger passenger, decimal fare)
        {
            if (passenger.WalletBalance >= fare)
            {
                passenger.WalletBalance -= fare;
                Console.WriteLine($"Deducted {fare} from wallet. New balance: {passenger.WalletBalance}");
            }
            else
            {
                throw new Exception("Insufficient balance.");
            }
        }
    }
}
