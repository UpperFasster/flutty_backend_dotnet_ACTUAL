namespace fluttyBackend.Service.services.JwtService
{
    public class AsyncJwtUtil : IAsyncJwtUtil
    {
        private readonly IAsyncJwtService jwtService;

        public AsyncJwtUtil(IAsyncJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        public async Task<Guid> AsyncGetUserIdByAuthHeader(string authHeader)
        {
            string token = authHeader["Bearer ".Length..].Trim();
            return await jwtService.GetUserIdFromTokenAsync(token);
        }
    }
}