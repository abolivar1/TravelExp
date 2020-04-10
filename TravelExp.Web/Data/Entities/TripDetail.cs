using System;
using System.ComponentModel.DataAnnotations;

namespace TravelExp.Web.Data.Entities
{
    public class TripDetail
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(1000, ErrorMessage = "The field {0} can not be more than {1} characteres")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public Trip Trip { get; set; }

        public ExpenseType ExpenseType { get; set; }
    }
}
