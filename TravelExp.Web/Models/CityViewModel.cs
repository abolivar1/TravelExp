using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Web.Data.Entities;

namespace TravelExp.Web.Models
{
    public class CityViewModel : City
    {
        public int CountryId { get; set; }

        public int CityId { get; set; }
    }
}
