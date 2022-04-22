using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces.Common
{
    public interface IApplicationExceptionsService
    {
        ApplicationExceptions WriteException(Exception ex);
    }
}
