using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {

            _userManager = userManager;
        }

        public async Task<BaseListResponse<UserDTO>> List(UserSearch search)
        {
            List<ApplicationUser> users = await _userManager.Users.Where(x =>
             (string.IsNullOrEmpty(search.Name) || x.FullName.Contains(search.Name)) &&
             (string.IsNullOrEmpty(search.MobileNumber) || x.MobileNumber.Contains(search.MobileNumber)) &&
             (string.IsNullOrEmpty(search.Address) || x.Address.Contains(search.Address)) &&
             (!search.IsActive.HasValue || x.IsActive == search.IsActive)).OrderByDescending(x=>x.Id).ToListAsync();
            int totalCount = _userManager.Users.Count();
            List<UserDTO> paginusersatedUsers = users.Skip(search.PageSize * (search.PageNumber - 1)).Take(search.PageSize).Select(user => new UserDTO
            {
                Address = user.Address,
                FullName = user.FullName,
                IsActive = user.IsActive,
                MobileNumber = user.MobileNumber,
                Id = user.Id
            }).ToList();

            BaseListResponse<UserDTO> BaseListResponse = new BaseListResponse<UserDTO>
            {
                entities = paginusersatedUsers,
                TotalCount = totalCount
            };
            return BaseListResponse;
        } 
        public async Task<List<UserDTO>> GetAll()
        {
            List<UserDTO> users = await _userManager.Users.Select(user => new UserDTO
            {
                Address = user.Address,
                FullName = user.FullName,
                IsActive = user.IsActive,
                MobileNumber = user.MobileNumber,
                Id = user.Id
            }).ToListAsync();
           
            return users;
        }

        public async Task<bool> UpdateStatus(long userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActive = !user.IsActive;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

       

    }
}
