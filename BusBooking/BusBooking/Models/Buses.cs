using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Buses
    {
        public Buses()
        {
            Price = new HashSet<Price>();
        }

        public int BusId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public ICollection<Price> Price { get; set; }
    }
}
