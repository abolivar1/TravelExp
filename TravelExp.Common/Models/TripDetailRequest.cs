using System;
using System.Collections.Generic;
using System.Text;

namespace TravelExp.Common.Models
{
    public class TripDetailRequest
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }

        public string PicturePath { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
