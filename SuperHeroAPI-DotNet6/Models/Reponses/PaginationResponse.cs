namespace SuperHeroAPI_DotNet6.Models.Reponses
{
    public class PaginationResponse
    {
        public long TotalElements { get; set; }      // total records in DB

        public int TotalPages { get; set; }          // total pages

        public int CurrentPage { get; set; }         // current page number

        public int PageSize { get; set; }            // number of items per page

        public int HasNextPage { get; set; }

        public int HasPreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int? PreviousPage { get; set; }

        public int? FirstPage { get; set; }

        public int? LastPage { get; set; }

        public static PaginationResponse CalculatePagination(int totalElements, int page, int size)
        {
            PaginationResponse pagination = new PaginationResponse();

            int TotalPages = (int)Math.Ceiling((double)totalElements / size);
            int CurrentPage = Math.Max(1, Math.Min(page, TotalPages));
            int HasNextPage = CurrentPage < TotalPages ? 1 : 0;
            int HasPreviousPage = CurrentPage > 1 ? 1 : 0;

            pagination.TotalElements = totalElements;
            pagination.PageSize = size;
            pagination.TotalPages = TotalPages;
            pagination.CurrentPage = CurrentPage;
            pagination.HasNextPage = HasNextPage;
            pagination.HasPreviousPage = HasPreviousPage;
            pagination.NextPage = (HasNextPage == 1 ? CurrentPage + 1 : null);
            pagination.PreviousPage = (HasPreviousPage == 1 ? CurrentPage - 1 : null);
            pagination.FirstPage = (TotalPages > 0 ? 1 : null);
            pagination.LastPage = (TotalPages > 0 ? TotalPages : null);


            return pagination;
        }

    }
}
