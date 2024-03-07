using fluttyBackend.Service.exceptions;
using fluttyBackend.Service.services.AuthService.roleVerifier;
using fluttyBackend.Service.services.JwtService;

namespace fluttyBackend.Controller.Handler
{
    public class RoleHandlerMiddleware : IMiddleware
    {
        private readonly IAsyncRoleVerifierService roleVerifier;
        private readonly IAsyncJwtService jwtAsyncService;

        public RoleHandlerMiddleware(
            IAsyncRoleVerifierService roleVerifier,
            IAsyncJwtService jwtAsyncService)
        {
            this.roleVerifier = roleVerifier;
            this.jwtAsyncService = jwtAsyncService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Guid companyId;
            var companyIdString = context.Request.Path.Value?.Split('/')[2];
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeaderValue) &&
            authHeaderValue.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeaderValue.ToString()["Bearer ".Length..];

                var userId = (Guid)await jwtAsyncService.GetUserIdFromTokenAsync(token);

                // if (context.Request.RouteValues.TryGetValue("companyId", out var companyIdObj) && companyIdObj is Guid companyIdFromRoute)
                if (Guid.TryParse(companyIdString, out Guid companyIdGuid))
                {
                    companyId = companyIdGuid;
                    if (await roleVerifier.IsUserFounderOrEmployeeAsync(userId, companyId))
                    {
                        await next(context);
                        return;
                    }
                    else
                    {
                        throw new AccessDeniedException();
                    }
                }
            }
            throw new UnauthorizedException();
        }
    }
}