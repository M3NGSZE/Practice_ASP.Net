using System.Net;

namespace SuperHeroAPI_DotNet6.Models.Reponse
{
    public record ApiResponse <T>(
        string message = "Success",
        int statusCode = 200,                 // Use int instead of HttpStatusCode enum (easier for JSON/clients)
        T? payload = default,
        DateTimeOffset timestamp = default    // We'll set default below
    )
    {
        public DateTimeOffset timestamp { get; init; } = timestamp == default ? DateTimeOffset.UtcNow : timestamp;
    }
}
