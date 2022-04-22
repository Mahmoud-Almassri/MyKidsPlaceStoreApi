using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTO
{
    public partial class ResetPasswordDTO
    {

        public string MobileNumber { get; set; }
        public string code { get; set; }
    }
}
