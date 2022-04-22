using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class ApplicationUser : IdentityUser<long>
    {
        public ApplicationUser()
        {
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Orders = new HashSet<Orders>();
            UserCart = new HashSet<UserCart>();
            UserClaims = new HashSet<UserClaims>();
            UserLogins = new HashSet<UserLogins>();
            UserRoles = new HashSet<UserRoles>();
        }

        public string MobileNumber { get; set; }
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string RegId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int DeviceType { get; set; }
        public bool AllowNotifications { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public string Address { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    
        public virtual ICollection<UserCart> UserCart { get; set; }
        public virtual ICollection<UserClaims> UserClaims { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<UserLogins> UserLogins { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }


    }
}
