using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExp.Web.Data.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(100, ErrorMessage = "The field {0} can not be more than {1} characteres")]
        [Display(Name = "City")]
        public string Name { get; set; }

        public Country Country { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}
