using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing.Models;

namespace RideSharing.Interfaces
{
    public interface IPayable
    {
        void AddFunds(Passenger passenger, decimal amount);
        void DeductFare(Passenger passenger, decimal fare);
    }
}
