using Microsoft.AspNetCore.Http;

namespace fluttyBackend.Service.services.PhotoService
{
    public interface IAsyncPhotoService
    {
        Task<string> SavePhotoAsync(IFormFile img, Guid fileId);
        Task<Stream> GetPhotoStream(string fileName);
        Task<bool> DeletePhoto(string fileName);
    }
}