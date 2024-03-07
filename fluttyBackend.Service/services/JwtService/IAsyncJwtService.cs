namespace fluttyBackend.Service.services.JwtService
{
    public interface IAsyncJwtService
    {
        Task<string> GenerateTokenAsync(string userId);
        Task<bool> ValidateTokenAsync(string token);
        Task<Guid> GetUserIdFromTokenAsync(string token);
    }
}