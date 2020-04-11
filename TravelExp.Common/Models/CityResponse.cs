using System;
using System.Collections.Generic;
using System.Text;

namespace TravelExp.Common.Models
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CountryResponse Country { get; set; }
    }
}
