using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTO
{
    public class UpdatePasswordDTO
    {
        
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
    
    public class ChangePasswordForAdminDTO
    {
        
        public string MobileNumber { get; set; }
        public string NewPassword { get; set; }
    }
}
