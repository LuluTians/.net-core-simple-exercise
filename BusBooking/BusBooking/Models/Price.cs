using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Price
    {
        public int PriceId { get; set; }
        public int BusId { get; set; }
        public string TypeId { get; set; }
        public decimal Cost { get; set; }

        public Buses Bus { get; set; }
        public Type Type { get; set; }
    }
}
