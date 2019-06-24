namespace BESL.Web.Helpers
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;

    public static class CloudinaryHelper
    {
        public static Cloudinary GetInstance(IConfiguration configuration)
        {
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
