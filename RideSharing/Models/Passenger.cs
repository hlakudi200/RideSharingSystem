using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Models
{
    public class Passenger:User
    {
        public decimal WalletBalance { get; set; }
        public List<Ride> RideHistory { get; set; } = new List<Ride>();

    }
}
