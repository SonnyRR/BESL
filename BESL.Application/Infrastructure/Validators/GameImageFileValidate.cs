namespace BESL.Application.Infrastructure.Validators
{
    using Microsoft.AspNetCore.Http;
    using BESL.Application.Interfaces;

    public class GameImageFileValidate : IFileValidate
    {
        private const int FILE_MAX_SIZE = 4194304;
        private const string FORMAT_MSG = "Image must be: .jpg, .jpeg or .png format!", FILE_MAX_SIZE_MSG = "Image must not be above {0}MB large!";

        public (bool Valid, string ErrorMessage) ValidateFile(IFormFile fileToValidate)
        {
            bool isValid = true;
            string message = null;

            string fileExtension = fileToValidate.ContentType;
           
            switch (fileExtension)
            {
                case "image/jpeg":
                case "image/png":
                    break;
                default:
                    isValid = false;
                    message = FORMAT_MSG;
                    return (isValid, message);
            }

            var size = fileToValidate.Length;
            if (size > FILE_MAX_SIZE)
            {
                isValid = false;
                message = string.Format(FILE_MAX_SIZE_MSG, FILE_MAX_SIZE / 1048576);
            }

            return (isValid, message);
        }
    }
}
