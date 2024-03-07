namespace fluttyBackend.Service.services.AuthService.roleVerifier
{
    public interface IAsyncRoleVerifierService
    {
        // public Task<bool> roleVerify(Guid user_id, string path);
        public Task<bool> IsUserFounderOrEmployeeAsync(Guid userId, Guid companyId);
    }
}