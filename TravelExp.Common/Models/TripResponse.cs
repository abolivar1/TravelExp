﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelExp.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public EmployeeResponse Employee { get; set; }

        public CityResponse City { get; set; }

        public ICollection<TripDetailResponse> TripDetails { get; set; }

    }
}
