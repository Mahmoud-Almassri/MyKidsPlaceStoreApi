using Domains.Common;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPushNotificationService
    {
        Task<bool> SendNotificationToAllUsersAsync(PushNotification notificationModel);
        Task<bool> SendNotificationToUserAsync(long? userId, PushNotification notificationModel);
        Task<bool> SendNotificationToMultiUsersAsync(PushNotification notificationModel);
        Task<bool> SendNotificationToAdminAsync(PushNotification notificationModel);
    }
}
