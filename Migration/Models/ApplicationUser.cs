using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class ApplicationUser
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

        public long Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
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
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<UserCart> UserCart { get; set; }
        public virtual ICollection<UserClaims> UserClaims { get; set; }
        public virtual ICollection<UserLogins> UserLogins { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
