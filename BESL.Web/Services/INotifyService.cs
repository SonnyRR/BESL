namespace BESL.Web.Services
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserSuccessNotificationAsync(string messageHeader, string message, string userId);

        Task SendUserFailiureNotificationAsync(string messageHeader, string message, string userId);
    }
}
