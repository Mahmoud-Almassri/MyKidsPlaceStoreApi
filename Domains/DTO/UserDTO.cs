using System;

namespace Domains.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
        public bool? IsActive { get; set; }
        public string Address { get; set; }
    }
}
