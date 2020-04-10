using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExp.Web.Data.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(100, ErrorMessage = "The field {0} can not be more than {1} characteres")]
        [Display(Name = "Expense Type")]
        public string Name { get; set; }

        public ICollection<TripDetail> TripDetails { get; set; }
    }
}
