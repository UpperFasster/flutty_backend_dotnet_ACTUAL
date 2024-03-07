using System.Net;
using System.Text.Json;
using fluttyBackend.Service.exceptions;

namespace fluttyBackend.Controller.Middlware
{
    public class GlobalExceptionHandlerMiddlware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddlware> logger;

        public GlobalExceptionHandlerMiddlware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddlware> logger)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                // todo for exception like access denied and unauth remove logger 
                logger.LogError(
                    e.Message.ToString(),
                    e.StackTrace.ToString(),
                    e.GetType()
                );
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ResponseModel exceptionModel = new();

            switch (exception)
            {
                case HttpException ex:
                    exceptionModel.statusCode = ex.HttpStatusCode;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = ex.Message;
                    response.StatusCode = ex.HttpStatusCode;
                    // (int)HttpStatusCode.BadRequest;
                    break;
                case ApplicationException ex:
                    exceptionModel.statusCode = (int)HttpStatusCode.BadRequest;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = "Application Exception Occured, please retry after sometime.";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case FileNotFoundException ex:
                    exceptionModel.statusCode = (int)HttpStatusCode.NotFound;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = "The requested resource is not found.";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case AccessDeniedException ex:
                    exceptionModel.statusCode = (int)HttpStatusCode.Forbidden;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case UnauthorizedException ex:
                    exceptionModel.statusCode = (int)HttpStatusCode.Unauthorized;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    exceptionModel.statusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionModel.path = context.Request.Path;
                    exceptionModel.message = "Internal Server Error, Please retry after sometime";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var exResult = JsonSerializer.Serialize(exceptionModel);
            await context.Response.WriteAsync(exResult);
        }
    }
}