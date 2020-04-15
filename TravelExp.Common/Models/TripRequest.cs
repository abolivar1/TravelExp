using System;
using System.Collections.Generic;
using System.Text;

namespace TravelExp.Common.Models
{
    public class TripRequest
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
