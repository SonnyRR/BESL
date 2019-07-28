namespace BESL.Infrastructure.Cloudinary
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;

    public class CloudinaryFactory
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
    }
}
