namespace BESL.Web.Services
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserSuccessNotificationAsync(string message, string userId);

        Task SendUserFailiureNotificationAsync(string message, string userId);
    }
}
