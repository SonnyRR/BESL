namespace BESL.Application.Interfaces
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserSuccessNotificationAsync(string messageHeader, string message, string userId);

        Task SendUserFailiureNotificationAsync(string messageHeader, string message, string userId);
    }
}
