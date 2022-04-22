using Domains.Common;
using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository.UnitOfWork;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Service.Services
{
    public class AuthService: IAuthService
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly LoggedInUserService _loggedInUserService;
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        AppConfiguration _appConfiguration = new AppConfiguration();

        public AuthService(
             UserManager<ApplicationUser> userManager,
            
              IRepositoryUnitOfWork repositoryUnitOfWork,
            SignInManager<ApplicationUser> signInManager,
             LoggedInUserService loggedInUserService
            )
        {
           _loggedInUserService = loggedInUserService;
            _userManager = userManager;
            _repositoryUnitOfWork = repositoryUnitOfWork;

            _signInManager = signInManager;
        }

        public async Task<TokenResponseDTO> Login(LoginDTO model, bool isMobile)
        {
          

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (signInResult.Succeeded)
            {
                //Todo: ApplicationUser is not linked correctly with other entities. Fix this and filter user when mobile is true to bring drivers and clients only 
                ApplicationUser user = _userManager.Users.FirstOrDefault(r => r.UserName == model.Username);
                if (user == null)
                {
                    throw new ValidationException(ErrorMessages.LoginPasswordWrong.GetHashCode().ToString());
                }
                TokenResponseDTO tokenResponse = BuildUserLoginObject(user, await BuildClaims(user), await _userManager.GetRolesAsync(user));
                return tokenResponse;
            }
            else
            {
                throw new ValidationException(ErrorMessages.LoginPasswordWrong.GetHashCode().ToString());
            }
           
        }


        public async Task<ApplicationUser> Create(RegisterDTO model)
        {
            // var userId = _principalService.GetUserId();
            bool existMobileNumber = _userManager.Users.FirstOrDefault(x => x.MobileNumber == model.MobileNumber) == null ? false : true;
           
            if (existMobileNumber)
            {
                throw new ValidationException(ErrorMessages.MobileNumberIsUsed.GetHashCode().ToString());
            }

            ApplicationUser user = new ApplicationUser
            {
                CreatedAt = DateTime.Now,
                // CreatedBy = userId,
                Email = model.Email,
                FullName = model.FullName,
                IsActive = true,
                MobileNumber = model.MobileNumber,
                UserName = model.MobileNumber,
                Address = model.Address
            };

            
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await CreateRoleIfNotExistAsync("User", user.Id);
                await CreateUserCartAsync(user.Id);
                return user;
            }
            throw new ValidationException(ErrorMessages.ErrorCreatingAccount.GetHashCode().ToString());
        }

        public async Task<UserProfileDTO> UpdateUserProfile(UserProfileDTO userProfile)
        {
            long userId = _loggedInUserService.GetUserId();

            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            user.Email = userProfile.Email;
            user.UserName = userProfile.MobileNumber;
            user.MobileNumber = userProfile.MobileNumber;
            user.FullName = userProfile.FullName;
            user.Address = userProfile.Address;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return userProfile;
            }
            throw new Exception(ErrorMessages.ErrorCreatingAccount.ToString());
        }

        public async Task<UserProfileDTO> GetUserProfileData()
        {
            long userId = _loggedInUserService.GetUserId();

            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            UserProfileDTO userProfileDTO = new UserProfileDTO
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                MobileNumber = user.MobileNumber
            };

            return userProfileDTO;
        }
        public async Task<bool> ChangePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            long userId = _loggedInUserService.GetUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            IdentityResult result = await _userManager.ChangePasswordAsync(user, updatePasswordDTO.OldPassword, updatePasswordDTO.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else if (result.Errors.Any())
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);

            }
            throw new System.Exception();
        }

        public List<DashboardDTO> GetDashboardData() {
            IEnumerable<Orders> orders = _repositoryUnitOfWork.Order.Value.Find(x=>x.Id >= 1423).ToList();
            int applicationsUsers = _userManager.Users.Count();
            IEnumerable<CartItem> cartItems = _repositoryUnitOfWork.CartItem.Value.Find(x => x.Status == (int)GlobalStatusEnum.InCart, x => x.Item);
            List<int> orderedSubCategories = _repositoryUnitOfWork.CartItem.Value.Find(x => x.Status == (int)GlobalStatusEnum.InMyOrders, x => x.Item).OrderBy(x=>x.ItemId).Select(x => x.Item.SubCategoryId).OrderBy(x=>x).ToList();
            var mostOrderedSubCategory = (from numbers in orderedSubCategories
                    group numbers by numbers into grouped
                    select new { Number = grouped.Key, Freq = grouped.Count() }).First();
            SubCategory subCategory = _repositoryUnitOfWork.SubCategory.Value.Find(x => x.Id == mostOrderedSubCategory.Number).FirstOrDefault();
            List<DashboardDTO> dashboards = new List<DashboardDTO>
            {
                new DashboardDTO { Name = "Total orders", Value = orders.Count().ToString() },
                new DashboardDTO { Name = "Total order amount", Value =  orders.Sum(x=>x.TotalAmount).ToString() + " JOD"},
                new DashboardDTO { Name = "Items added to cart", Value = cartItems.Count().ToString() },
                new DashboardDTO { Name = "registered users", Value =  applicationsUsers.ToString()},
                new DashboardDTO { Name = "Today orders", Value =  orders.Where(x=>x.CreatedDate.Value.Date == DateTime.Now.Date).Count().ToString()},
                new DashboardDTO { Name = "Total revenue", Value =  orders.Where(x=>x.Status == (int) GlobalStatusEnum.Delivered).Sum(x=>x.TotalAmount).ToString() + " JOD"},
                new DashboardDTO { Name = "Most Sub Category Ordered", Value =  subCategory.Name +" : " + mostOrderedSubCategory.Freq.ToString() }
            };
            return dashboards;
        }
        public async Task<ApplicationUser> UpdateRegId(string regId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(_loggedInUserService.GetUserId().ToString());
            if (user != null)
            {
                user.RegId = regId;
                await _userManager.UpdateAsync(user);
                return user;
            }
            else
            {
                throw new ValidationException(ErrorMessages.ErrorCreatingAccount.GetHashCode().ToString());
            }
        }


            private async Task<IList<Claim>> BuildClaims(ApplicationUser user)
             {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(user.Id.ToString())? "":user.Id.ToString()),
                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.FullName)? "":user.FullName),
                new Claim(ClaimTypes.MobilePhone, string.IsNullOrEmpty(user.MobileNumber)? "":user.MobileNumber),
                new Claim(ClaimTypes.StreetAddress,string.IsNullOrEmpty(user.Address)? "":user.Address)
            };
            foreach (var item in roles)
            {
                var roleclaim = new Claim(ClaimTypes.Role, item);
                claims.Add(roleclaim);
            }


            return claims;
        }

        public async Task CreateRoleIfNotExistAsync(string roleName, long userId)
        {
            Roles role = await _repositoryUnitOfWork.Roles.Value.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                Roles roleObj = new Roles
                {
                    Id = 0,
                    Name = roleName
                };

                role = await _repositoryUnitOfWork.Roles.Value.AddAsync(roleObj);
            }
            UserRoles userRole = new UserRoles
            {
                RoleId = role.Id,
                UserId = userId
            };
            userRole = await _repositoryUnitOfWork.UserRole.Value.AddAsync(userRole);
        }

        public async Task CreateUserCartAsync(long userId)
        {
            await _repositoryUnitOfWork.UserCart.Value.AddAsync(new UserCart
            {
                UserId = userId,
                Status = (int)GlobalStatusEnum.Active
            });

        }

        
        //change this please
        private TokenResponseDTO BuildUserLoginObject(ApplicationUser user, IList<Claim> claims, IList<string> roles)
        {
            TokenResponseDTO response = new TokenResponseDTO {
             AccessToken = WriteToken(claims)
            };
           
            return response;
        }
        private string WriteToken(IList<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration.JWTKey));

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                    issuer: _appConfiguration.Issuer,
                    audience: _appConfiguration.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddYears(100),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public async Task ChangePasswordAsync(string mobile, string NewPassword)
        {
            var user =  _userManager.Users.FirstOrDefault(x=>x.UserName == mobile);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);
        }

        public async Task ForgetPassword(string MobileNumber)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.MobileNumber == MobileNumber);

            if (user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound.GetHashCode().ToString());
            }

            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, MobileNumber);

            string accountSid =_appConfiguration.AccountSid;
            string authToken =_appConfiguration.AuthToken;

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(MobileNumber);
            var from = new PhoneNumber(_appConfiguration.MobileNumber);

            var message = MessageResource.Create(
                body: "Your OTP Number is " + code.ToString(),
                from: from,
                to: to
            );

        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPassword)
        {
            ApplicationUser user =  _userManager.Users.FirstOrDefault(x => x.MobileNumber == resetPassword.MobileNumber);

            if(user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound.GetHashCode().ToString());
            }

            bool valid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, resetPassword.code, resetPassword.MobileNumber);
            return valid;
        }

        
    }
}
