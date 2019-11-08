using System;
using System.Collections.Generic;

namespace BusBooking.Models
{
    public partial class Type
    {
        public Type()
        {
            Price = new HashSet<Price>();
        }

        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<Price> Price { get; set; }
    }
}
