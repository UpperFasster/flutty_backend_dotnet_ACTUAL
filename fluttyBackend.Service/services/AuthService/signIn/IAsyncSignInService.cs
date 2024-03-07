using fluttyBackend.Service.services.AuthService.signIn.DTO.request;
using fluttyBackend.Service.services.AuthService.signIn.DTO.response;

namespace fluttyBackend.Service.services.AuthService.signIn
{
    public interface IAsyncSignInService
    {
        Task<SuccessfullySignInDTOResponse> SignIn(UserSignInDTO userSignIn);
    }
}