using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Common
{
    public interface IApplicationExceptionsRepository
    {
        public ApplicationExceptions WriteException(ApplicationExceptions ex);
       
    }
}
