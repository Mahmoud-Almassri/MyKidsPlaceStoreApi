using System;
using System.Collections.Generic;

namespace Domain.Models.Common
{
    public partial class ApplicationExceptions
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string LoggedInUser { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
