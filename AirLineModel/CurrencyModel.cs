using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirLine.Models
{
    public class CurrencyModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public int Value { get; set; }
    }
}