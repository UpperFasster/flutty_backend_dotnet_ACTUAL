namespace fluttyBackend.Service.services.JwtService
{
    public interface IAsyncJwtUtil
    {
        Task<Guid> AsyncGetUserIdByAuthHeader(string authHeader);
    }
}