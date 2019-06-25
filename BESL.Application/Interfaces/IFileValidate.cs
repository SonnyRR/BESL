namespace BESL.Application.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IFileValidate
    {
        (bool Valid, string ErrorMessage) ValidateFile(IFormFile fileToValidate);
    }
}
