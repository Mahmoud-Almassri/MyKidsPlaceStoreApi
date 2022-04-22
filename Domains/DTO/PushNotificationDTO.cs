using System;
using System.Collections.Generic;

namespace Domains.DTO
{
    public class PushNotificationDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<long> UsersIds { get; set; }
    }
}
