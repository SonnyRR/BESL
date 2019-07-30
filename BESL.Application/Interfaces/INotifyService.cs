namespace BESL.Application.Interfaces
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserSuccessNotificationAsync(string header, string content, string userId = null);

        Task SendUserFailiureNotificationAsync(string header, string message, string userId = null);
    }
}
