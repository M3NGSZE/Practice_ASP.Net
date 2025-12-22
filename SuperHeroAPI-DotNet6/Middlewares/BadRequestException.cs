namespace SuperHeroAPI_DotNet6.Middlewares
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
