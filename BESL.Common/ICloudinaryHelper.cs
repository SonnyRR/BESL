using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BESL.Common
{
    public interface ICloudinaryHelper
    {
        Cloudinary GetInstance(IConfiguration configuration);

        Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileForm, string name, Transformation transformation = null);
    }
}
