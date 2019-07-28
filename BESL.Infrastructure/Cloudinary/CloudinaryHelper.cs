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
            if (fileForm == null)
            {
                throw new ArgumentNullException(nameof(fileForm));
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

            var uploadResult = this.cloudinary.Upload(uploadParams);

            stream.Dispose();
            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
