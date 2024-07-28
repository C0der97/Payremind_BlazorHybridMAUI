using static PayRemind.Components.Pages.Home;

namespace PayRemind.Shared
{
    public class NotificationServiceBd
    {
        private readonly List<NotificationData> _notifications = new List<NotificationData>();

        public void AddNotification(NotificationData notification)
        {
            _notifications.Add(notification);
        }

        public List<NotificationData> GetNotifications()
        {
            return _notifications;
        }
    }

}
