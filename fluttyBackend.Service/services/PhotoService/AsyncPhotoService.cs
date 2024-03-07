using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace fluttyBackend.Service.services.PhotoService
{
    public class AsyncPhotoService : IAsyncPhotoService
    {
        private readonly string _uploadDir;

        public AsyncPhotoService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _uploadDir = Path.Combine(environment.ContentRootPath, configuration["PhotoPath"]);
        }

        public async Task<string> SavePhotoAsync(IFormFile img, Guid fileId)
        {
            string originalFileName = img.FileName;
            string fileExtension = Path.GetExtension(originalFileName);

            string newFileName = $"{fileId}{fileExtension}";
            string filePath = Path.Combine(_uploadDir, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await img.CopyToAsync(stream);
            }

            return newFileName;
        }

        public Task<Stream> GetPhotoStream(string fileName)
        {
            string filePath = Path.Combine(_uploadDir, fileName);
            if (File.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return Task.FromResult<Stream>(stream);
            }
            else
            {
                return Task.FromResult<Stream>(null);
            }
        }

        public Task<bool> DeletePhoto(string fileName)
        {
            string filePath = Path.Combine(_uploadDir, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

    }
}