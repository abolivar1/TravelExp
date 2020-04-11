using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExp.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} is not a valid email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MinLength(6, ErrorMessage = "The field {0} can not be less than {1} characteres")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
