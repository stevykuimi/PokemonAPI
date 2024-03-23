namespace PokemonAPI.PaginationManager
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize;
        }

        private const int MAX_PAGE_SIZE = 20;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
