namespace fluttyBackend.Service.exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : base("Access denied") { }
    }
}