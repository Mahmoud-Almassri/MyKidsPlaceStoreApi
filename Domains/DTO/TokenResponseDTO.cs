using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTO
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email  { get; set; }
        public IList<string> Roles { get; set; }
}
}
