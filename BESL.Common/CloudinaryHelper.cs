namespace BESL.Common
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public static class CloudinaryHelper
    {
        public static Cloudinary GetInstance(IConfiguration configuration)
        {
            
            #warning Don't forget to enter your Cloudinary credentials.
            /*
             * dotnet user-secrets set "cloudinary-cloud" "your_cloud_id"
             * dotnet user-secrets set "cloudinary-apiKey" "your_api_key"
             * dotnet user-secrets set "cloudinary-apiSecret" "your_api_secret"
             */
            var cloud = configuration["cloudinary-cloud"];
            var apiKey = configuration["cloudinary-apiKey"];
            var apiSecret = configuration["cloudinary-apiSecret"];

            Account account = new Account(cloud, apiKey, apiSecret);

            Cloudinary instance = new Cloudinary(account);
            return instance;
        }

        public static async Task<string> UploadImage(Cloudinary cloudinary, 
            IFormFile fileForm, 
            string name, 
            Transformation transformation = null)
        {
            if (fileForm == null)
            {
                return null;
            }

            byte[] image;

            using (var memoryStream = new MemoryStream())
            {
                await fileForm.CopyToAsync(memoryStream);
                image = memoryStream.ToArray();
            }

            var stream = new MemoryStream(image);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream),
                Transformation = transformation
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            stream.Dispose();
            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
