namespace DotNet.Domain.Core.Notification
{
    public interface INotifier
    {
        List<Notification> GetAllNotifications();

        void AddNotification(Notification notificacao);

        bool HasNotification();

        bool HasAnyError();

        void ClearErrors();
    }
}