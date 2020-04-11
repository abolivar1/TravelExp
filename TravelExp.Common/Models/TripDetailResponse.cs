using System;
using System.Collections.Generic;
using System.Text;

namespace TravelExp.Common.Models
{
    public class TripDetailResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string PicturePath { get; set; }

        public string LogoFullPath => string.IsNullOrEmpty(PicturePath)
    ? "https://travelexpalex.azurewebsites.net//images/noimage.png"
    : $"https://travelexpalex.azurewebsites.net{PicturePath.Substring(1)}";

        public TripResponse Trip { get; set; }

        public ExpenseTypeResponse ExpenseType { get; set; }
    }
}
