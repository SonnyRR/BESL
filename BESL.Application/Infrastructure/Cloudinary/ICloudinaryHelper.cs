namespace BESL.Application.Infrastructure.Cloudinary
{
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public interface ICloudinaryHelper
    {
        Cloudinary GetInstance(IConfiguration configuration);

        Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileForm, string name, Transformation transformation = null);
    }
}
