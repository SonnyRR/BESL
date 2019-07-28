namespace BESL.Application.Interfaces
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryHelper
    {
        Task<string> UploadImage(IFormFile fileForm, string name, Transformation transformation = null);
    }
}
