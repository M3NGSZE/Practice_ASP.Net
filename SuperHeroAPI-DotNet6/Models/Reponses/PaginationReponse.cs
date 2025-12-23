namespace SuperHeroAPI_DotNet6.Models.Reponses
{
    public class PaginationReponse
    {
        public long TotalElements { get; set; }      // total records in DB

        public int TotalPages { get; set; }          // total pages

        public int CurrentPage { get; set; }         // current page number

        public int PageSize { get; set; }            // number of items per page

        private int HasNextPage { get; set; }

        private int HasPreviousPage { get; set; }

        private int NextPage { get; set; }

        private int PreviousPage { get; set; }

        private int FirstPage { get; set; }

        private int LastPage { get; set; }



    }
}
