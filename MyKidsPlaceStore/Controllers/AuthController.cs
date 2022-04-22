using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domains.DTO;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public AuthController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }
     
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {

                return Ok(await _serviceUnitOfWork.Auth.Value.Login(loginDTO, true));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSignup([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Auth.Value.Create(registerDTO));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateRegId(string regId)
        {
            try
            {

                ApplicationUser applicationUser = await _serviceUnitOfWork.Auth.Value.UpdateRegId(regId);
                return Ok(true);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetDashboardData()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Auth.Value.GetDashboardData());
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDTO user)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Auth.Value.UpdateUserProfile(user));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfileData()
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Auth.Value.GetUserProfileData());
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] UpdatePasswordDTO updatePasswordDTO)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Auth.Value.ChangePassword(updatePasswordDTO));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string MobileNumber)
        {
            try
            {
                await _serviceUnitOfWork.Auth.Value.ForgetPassword(MobileNumber);
                return Ok();
            }
            catch (ValidationException e)
            {

                return BadRequest(e);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangePasswordFromAdmin([FromBody] ChangePasswordForAdminDTO changePasswordForAdminDTO)
        {
            try
            {
               await _serviceUnitOfWork.Auth.Value.ChangePasswordAsync(changePasswordForAdminDTO.MobileNumber, changePasswordForAdminDTO.NewPassword);
                return Ok();
            }
            catch (ValidationException e)
            {

                return BadRequest(e);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            try
            {
                 return Ok(await _serviceUnitOfWork.Auth.Value.ResetPassword(resetPassword));
            }
            catch(ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}