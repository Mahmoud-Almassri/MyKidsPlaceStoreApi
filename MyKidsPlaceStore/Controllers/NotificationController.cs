using Domains.Common;
using Domains.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class NotificationController : ControllerBase
    {
        private readonly IPushNotificationService _notificationService;
        public NotificationController(IPushNotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> SendSingleUserNotification(long userId, [FromBody] PushNotificationDTO pushNotificationDTO)
        {
            try
            {
                PushNotification notificationModel = new PushNotification();
                notificationModel.Title = pushNotificationDTO.Title;
                notificationModel.Body = pushNotificationDTO.Body;
                return Ok(await _notificationService.SendNotificationToUserAsync(userId, notificationModel));
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

        [HttpPost("{user}")]
        public async Task<IActionResult> PodcastAllUsersNotification(int user, [FromBody] PushNotificationDTO pushNotificationDTO)
        {
            try
            {
                PushNotification notificationModel = new PushNotification();
                notificationModel.Title = pushNotificationDTO.Title;
                notificationModel.Body = pushNotificationDTO.Body;
                return Ok(await _notificationService.SendNotificationToAllUsersAsync(notificationModel));
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
        public async Task<IActionResult> PodcastMultiUsersNotification([FromBody] PushNotificationDTO pushNotificationDTO)
        {
            try
            {
                PushNotification notificationModel = new PushNotification();
                notificationModel.Title = pushNotificationDTO.Title;
                notificationModel.Body = pushNotificationDTO.Body;
                notificationModel.UsersIds = pushNotificationDTO.UsersIds;
                return Ok(await _notificationService.SendNotificationToMultiUsersAsync(notificationModel));
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
        public async Task<IActionResult> PodcastToAdmin([FromBody] PushNotificationDTO pushNotificationDTO)
        {
            try
            {
                PushNotification notificationModel = new PushNotification();
                notificationModel.Title = pushNotificationDTO.Title;
                notificationModel.Body = pushNotificationDTO.Body;
                return Ok(await _notificationService.SendNotificationToAdminAsync(notificationModel));
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
    }
}