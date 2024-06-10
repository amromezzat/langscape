using System;
using System.Net;

namespace Langscape.Shared.Impl
{
    public class PaginatedResult<T> : Result<T>
    {
        public PaginatedResult(T data = default, int count = 0, int pageNumber = 1, int pageSize = 10, string message = null)
        {
            Succeeded = true;
            Code = HttpStatusCode.OK;
            Title = message;
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

        public static PaginatedResult<T> Success(T data, int count, int pageNumber, int pageSize, string message = null)
        {
            return new PaginatedResult<T>(data, count, pageNumber, pageSize, message);
        }
    }
}