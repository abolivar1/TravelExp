using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExp.Web.Data.Entities
{
    public class Trip
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public Employee Employee { get; set; }

        public City City { get; set; }

        public ICollection<TripDetail> TripDetails { get; set; }

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; }
    }
}
