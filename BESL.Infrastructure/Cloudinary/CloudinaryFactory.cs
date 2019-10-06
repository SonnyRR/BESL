namespace BESL.Infrastructure.Cloudinary
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;

    public class CloudinaryFactory
    {
        public static Cloudinary GetInstance(IConfiguration configuration)
        {
            var cloud = configuration["cloudinary-cloud"];
            var apiKey = configuration["cloudinary-apiKey"];
            var apiSecret = configuration["cloudinary-apiSecret"];

            Account account = new Account(cloud, apiKey, apiSecret);

            Cloudinary instance = new Cloudinary(account);
            return instance;
        }
    }
}
