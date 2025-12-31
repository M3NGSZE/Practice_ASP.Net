namespace SuperHeroAPI_DotNet6.Models.Reponses
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public object? Errors { get; set; }
        public string Path { get; set; } = null!;
    }
}
