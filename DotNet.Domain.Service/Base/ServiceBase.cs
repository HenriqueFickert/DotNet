using DotNet.Domain.Core.Notification;

namespace DotNet.Domain.Service.Base
{
    public class ServiceBase
    {
        protected readonly INotifier _notifier;

        public ServiceBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void AddNotification(string messege)
        {
            _notifier.AddNotification(new Notification(messege));
        }

        protected void AddNotification(string messege, ENotificationType notificationType)
        {
            _notifier.AddNotification(new Notification(messege, notificationType));
        }
    }
}