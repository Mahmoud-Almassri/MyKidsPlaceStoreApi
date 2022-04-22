using Microsoft.AspNetCore.Http;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Service.Services
{
    public class LoggedInUserService: ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return long.Parse(userId);
        }

        public List<string> GetUserRoles()
        {
            var identity = (ClaimsPrincipal) Thread.CurrentPrincipal;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role);
            return roles.Select(r => r.Value).ToList();
        }
    }
}
