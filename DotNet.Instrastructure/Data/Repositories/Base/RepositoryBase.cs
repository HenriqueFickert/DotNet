using DotNet.Domain.Core.Notification;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DotNet.Instrastructure.Data.Repositories.Base
{
    public class RepositoryBase
    {
        protected SqlConnection sqlConnection;
        protected readonly INotifier _notifier;
        protected IConfiguration _configuration;

        public RepositoryBase(IConfiguration configuration, INotifier notifier)
        {
            _configuration = configuration;
            _notifier = notifier;
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection"));
        }

        protected void AddNotification(string messege)
        {
            _notifier.AddNotification(new Notification(messege));
        }

        protected void AddNotification(string messege, ENotificationType notificationType)
        {
            _notifier.AddNotification(new Notification(messege, notificationType));
        }

        protected void RequisitionError(Exception ex)
        {
            AddNotification("Erro ao realizar a requisição.");
        }
    }
}