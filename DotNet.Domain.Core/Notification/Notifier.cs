namespace DotNet.Domain.Core.Notification
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> notifications;

        public Notifier()
        {
            notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public void AddListNotification(List<Notification> notification)
        {
            notifications.AddRange(notification);
        }

        public List<Notification> GetAllNotifications()
        {
            return notifications;
        }

        public bool HasNotification()
        {
            return notifications.Any();
        }

        public bool HasAnyError()
        {
            return notifications.Where(x => x.NotificationType == ENotificationType.Error).Any();
        }

        public void ClearErrors()
        {
            notifications.Clear();
        }
    }
}