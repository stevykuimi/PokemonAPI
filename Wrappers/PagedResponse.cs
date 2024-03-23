namespace PokemonAPI.Wrappers
{
    public class PagedResponse<T>
    {
        public bool Succeeded { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool PreviousPage { get; set; }
        public bool NextPage { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string Code { get; set; }
        public List<T> Data { get; set; }


        public PagedResponse()
        {

        }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, int count)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PreviousPage = PageNumber > 1;
            NextPage = PageNumber < TotalPages;
            Message = null;
            Errors = null;
            Code = null;
            TotalItems = data.Count;

            Data = data
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            Succeeded = Data.Count >= 0;
            //if (Data.Count == 0)
            //    throw Exception( "No data found!");
        }
    }
}
