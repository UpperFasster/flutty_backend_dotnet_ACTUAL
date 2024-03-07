using fluttyBackend.Service.services.AuthService.signIn;
using fluttyBackend.Service.services.AuthService.signIn.DTO.request;
using fluttyBackend.Service.services.JwtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluttyBackend.Controller.Controllers.Auth
{
    [ApiController]
    [Route("auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAsyncSignInService signInService;
        private readonly IAsyncJwtService jwtService;

        public AuthController(
            IAsyncJwtService jwtAsyncService,
            IAsyncSignInService signInService)
        {
            this.jwtService = jwtAsyncService;
            this.signInService = signInService;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> GenerateToken([FromBody] UserSignInDTO userSignInDTO)
        {
            try
            {
                var result = await signInService.SignIn(userSignInDTO);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { Error = "Wrong password or email" });
            }
        }

        [HttpGet("validateToken/{token}")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            try
            {
                bool isValid = await jwtService.ValidateTokenAsync(token);

                if (isValid)
                {
                    return Ok(new { Message = "Token is valid." });
                }
                else
                {
                    return BadRequest(new { Error = "Token is not valid." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Failed to validate token.", Message = ex.Message });
            }
        }
    }
}