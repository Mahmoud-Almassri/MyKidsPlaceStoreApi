using Domains.Common;
using Domains.Models;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Identity;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private UserManager<ApplicationUser> _userManager;


        public PushNotificationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<bool> SendNotificationToAllUsersAsync(PushNotification notificationModel)
        {
            List<string> deviceTokens = _userManager.Users.Where(x => !string.IsNullOrEmpty(x.RegId) && x.IsActive == true)
                                                          .Select(x => x.RegId).ToList();
            await SendPushNotification(deviceTokens, notificationModel);
            return true;
        }
        public async Task<bool> SendNotificationToMultiUsersAsync(PushNotification notificationModel)
        {
            if (notificationModel.UsersIds.Count > 0)
            {
                List<string> deviceTokens = _userManager.Users.Where(x => !string.IsNullOrEmpty(x.RegId) && x.IsActive == true && notificationModel.UsersIds.Any(u => u == x.Id))
                                                         .Select(x => x.RegId).ToList();
                await SendPushNotification(deviceTokens, notificationModel);
            }

            return true;
        }
        public async Task<bool> SendNotificationToUserAsync(long? userId, PushNotification notificationModel)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            if (!string.IsNullOrEmpty(user.RegId))
            {
                notificationModel.DeviceToken = user.RegId;
                await SendPushNotification(notificationModel);
            }



            return true;
        }
        public async Task<bool> SendNotificationToAdminAsync(PushNotification notificationModel)
        {
            List<ApplicationUser> admins =  _userManager.Users.Where(x=>x.UserRoles.Any(y=>y.RoleId == 3)).ToList();
            foreach (var user in admins)
            {
                if (!string.IsNullOrEmpty(user.RegId))
                {
                    notificationModel.DeviceToken = user.RegId;
                    await SendPushNotification(notificationModel);
                }
            }            
            return true;
        }


        private async Task<bool> SendPushNotification(PushNotification notificationModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(notificationModel.DeviceToken))
                {
                    var message = new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = notificationModel.Title,
                            Body = notificationModel.Body
                        },
                        Token = notificationModel.DeviceToken
                    };

                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                }
                return true;
            }
            catch (System.Exception)
            {
                return true;
            }

        }

        private async Task<bool> SendPushNotification(List<string> deviceTokens, PushNotification notificationModel)
        {
            try
            {
                List<Message> messages = new List<Message>();

                foreach (string token in deviceTokens)
                {
                    var message = new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = notificationModel.Title,
                            Body = notificationModel.Body
                        },
                        Token = token
                    };
                    messages.Add(message);
                }

                var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
                if (response.SuccessCount > 0)
                {
                    return true;
                }
                return true;
            }
            catch (System.Exception)
            {
                return true;
            }
            return true;
        }
    }
}
