namespace Langscape.Shared.Implementation
{
    public class PaginatedResult<T> : Result<T>
    {
        public PaginatedResult(T data = default, int count = 0, int pageNumber = 1, int pageSize = 10, params string[] messages)
        {
            Succeeded = true;
            Code = 200;
            Messages = messages ?? new string[0];
            Data = data;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResult<T> Success(T data, int count, int pageNumber, int pageSize, params string[] messages)
        {
            return new PaginatedResult<T>(data, count, pageNumber, pageSize, messages);
        }
    }
}