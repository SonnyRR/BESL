namespace BESL.Application.Infrastructure.Cloudinary
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public interface ICloudinaryHelper
    {
        Cloudinary GetInstance(IConfiguration configuration);

        Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileForm, string name, Transformation transformation = null);
    }
}
