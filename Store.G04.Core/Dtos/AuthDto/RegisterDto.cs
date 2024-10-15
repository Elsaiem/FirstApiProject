using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.AuthDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage="Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "DisplayName is Required")]

        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Phone number is Required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is Required")]

        public string Password { get; set; }


    }
}
