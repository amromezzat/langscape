using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Shared.Common.Impl;

namespace Langscape.Shared.Impl
{
    public class Result<T> : IResult<T>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; protected set; }
        [JsonIgnore]
        public bool Succeeded { get; protected set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; protected set; }
        [JsonIgnore]
        public Exception Exception { get; private set; }
        [JsonIgnore]
        public HttpStatusCode Code { get; protected set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyList<ValidationError> Errors { get; protected set; }

        public Result<T> WithCode(HttpStatusCode code)
        {
            Code = code;

            return this;
        }

        public Result<T> WithException(Exception exception)
        {
            Exception = exception;

            return this;
        }

        public Result<T> WithErrors(IReadOnlyList<ValidationError> errors)
        {
            Errors = errors;

            return this;
        }

        public Task<Result<T>> ToTask()
        {
            return Task.FromResult(this);
        }

        public static Result<T> Success(string title = null)
        {
            return new Result<T> 
            { 
                Succeeded = true,
                Code = HttpStatusCode.Accepted,
                Title = title
            };
        }

        public static Result<T> Success(T data, string title = null)
        {
            var result = Success(title);
            result.Data = data;
            return result;
        }

        public static Result<T> Failure(string title, params ValidationError[] errors)
        {
            return new Result<T>
            {
                Succeeded = false,
                Title = title,
                Code = HttpStatusCode.BadRequest,
                Errors = errors ?? Array.Empty<ValidationError>(),
            };
        }
    }
}