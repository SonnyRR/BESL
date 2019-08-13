namespace BESL.Application.Interfaces
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserPushNotification(string header, string message, string type, string userId = null);

        Task Test();
    }
}
