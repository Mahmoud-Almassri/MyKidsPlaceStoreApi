using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Address { get; set; }
       

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
