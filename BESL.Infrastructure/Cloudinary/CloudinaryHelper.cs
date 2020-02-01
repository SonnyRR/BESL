namespace BESL.Infrastructure.Cloudinary
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces;

    public class CloudinaryHelper : ICloudinaryHelper
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryHelper(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadImage(
            IFormFile fileForm,
            string name,
            Transformation transformation = null)
        {
            fileForm = fileForm ?? throw new ArgumentNullException(nameof(fileForm));
   
            byte[] image;

            using (var memoryStream = new MemoryStream())
            {
                await fileForm.CopyToAsync(memoryStream);
                image = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(image);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, imageStream),
                Transformation = transformation
            };

            var uploadResult = this.cloudinary.Upload(uploadParams);

            imageStream.Dispose();
            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
