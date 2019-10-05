namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Http;

    public class FileHelper
    {
        public static IFormFile GetFile(string path, string contentType)
        {
            var isPathValid = Path.IsPathFullyQualified(path);

            if (!isPathValid)
            {
                throw new ArgumentException("Path is not fully qualified!");
            }

            else if (!File.Exists(path))
            {
                throw new FileNotFoundException("File does not exist!");
            }

            using (var stream = File.OpenRead(path))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };

                return file;
            }
        }
    }
}
