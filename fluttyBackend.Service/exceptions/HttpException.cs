namespace fluttyBackend.Service.exceptions
{
    public class HttpException : Exception
    {
        public int HttpStatusCode { get; }

        public HttpException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}