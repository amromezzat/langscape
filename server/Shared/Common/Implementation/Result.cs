using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Langscape.Shared.Implementation
{
    public class Result<T> : IResult<T>
    {
        public IReadOnlyList<string> Messages { get; protected set; } = new string[0];
        public bool Succeeded { get; protected set; }
        public T Data { get; protected set; }
        public Exception Exception { get; private set; }
        public int Code { get; protected set; }

        public Result<T> WithCode(int code)
        {
            Code = code;

            return this;
        }

        public Result<T> WithException(Exception exception)
        {
            Exception = exception;

            return this;
        }

        public Task<Result<T>> ToTask()
        {
            return Task.FromResult(this);
        }

        public static Result<T> Success(params string[] messages)
        {
            return new Result<T> 
            { 
                Succeeded = true,
                Code = 200,
                Messages = messages ?? new string[0]
            };
        }

        public static Result<T> Success(T data, params string[] messages)
        {
            var result = Success(messages);
            result.Data = data;
            return result;
        }

        public static Result<T> Failure(params string[] messages)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = messages ?? new string[0],
                Code = 400
            };
        }
    }
}