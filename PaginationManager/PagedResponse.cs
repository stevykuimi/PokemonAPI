namespace PokemonAPI.PaginationManager
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, int pageNumber, PaginationQuery paginationQuery)
        {
            List<T> source = data as List<T> ?? throw new InvalidCastException("Unable to convert data to a list!");
            PageNumber = paginationQuery.PageNumber;
            PageSize = paginationQuery.PageSize;
            ////check previous & next pages
            PreviousPage = PageNumber > 1 ? "Yes" : "No";
            TotalPages = (int)Math.Ceiling(source.Count / (double)PageSize);
            NextPage = PageNumber < TotalPages ? "Yes" : "No";
            Data = source.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

        public IEnumerable<T> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

        public int TotalPages { get; set; }
    }
}
