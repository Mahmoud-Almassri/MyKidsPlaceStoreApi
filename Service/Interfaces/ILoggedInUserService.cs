using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ILoggedInUserService
    {
        long GetUserId();

        List<string> GetUserRoles();
    }
}
