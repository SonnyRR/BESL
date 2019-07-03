namespace BESL.Common
{
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class CloudinaryHelperWrapper
    {
        public Cloudinary GetInstance(IConfiguration configuration)
        {
            return CloudinaryHelper.GetInstance(configuration);
        }

        public async Task<string> UploadImage(Cloudinary cloudinary,
            IFormFile fileForm,
            string name,
            Transformation transformation = null)
        {
            return await CloudinaryHelper.UploadImage(cloudinary, fileForm, name, transformation);
        }

    }
}
