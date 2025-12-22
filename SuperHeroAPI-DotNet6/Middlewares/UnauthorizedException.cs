namespace SuperHeroAPI_DotNet6.Middlewares
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
