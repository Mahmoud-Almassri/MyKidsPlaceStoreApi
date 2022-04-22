using Domains.DTO;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuthService
    {
         Task<TokenResponseDTO> Login(LoginDTO model, bool isMobile);
        Task ChangePasswordAsync(string mobile, string NewPassword);
        Task<UserProfileDTO> GetUserProfileData();
         Task<ApplicationUser> Create(RegisterDTO model);
         Task<ApplicationUser> UpdateRegId(string regId);
        Task<UserProfileDTO> UpdateUserProfile(UserProfileDTO userProfile);
        Task<bool> ChangePassword(UpdatePasswordDTO updatePasswordDTO);
        List<DashboardDTO> GetDashboardData();
        Task ForgetPassword(string MobileNumber);
        Task<bool> ResetPassword(ResetPasswordDTO resetPassword);


    }
}
