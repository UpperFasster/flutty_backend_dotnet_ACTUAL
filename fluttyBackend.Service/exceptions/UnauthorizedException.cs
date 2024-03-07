namespace fluttyBackend.Service.exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Unauthorized") { }
    }
}