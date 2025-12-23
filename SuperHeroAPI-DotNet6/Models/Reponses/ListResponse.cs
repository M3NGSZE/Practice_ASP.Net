namespace SuperHeroAPI_DotNet6.Models.Reponses
{
    public class ListResponse <T>
    {
        public List<T> Elements { get; set; } = new();

        public PaginationResponse? PaginationResponse { get; set; }
    }
}
