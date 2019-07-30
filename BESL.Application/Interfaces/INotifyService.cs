namespace BESL.Application.Interfaces
{    
    using System.Threading.Tasks;

    public interface INotifyService
    {
        Task SendUserSuccessNotificationAsync(string header, string content);

        Task SendUserFailiureNotificationAsync();
    }
}
