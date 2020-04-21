using System;
using System.Collections.Generic;
using System.Text;
using TravelExp.Common.Enums;

namespace TravelExp.Common.Models
{
    public class EmployeeResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public string LogoFullPath => string.IsNullOrEmpty(PicturePath)
    ? "https://travelexpalex3.azurewebsites.net//images/noimage.png"
    : $"https://travelexpalex3.azurewebsites.net{PicturePath.Substring(1)}";

        public List<TripResponse> Trips { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}
