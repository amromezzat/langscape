namespace Langscape.Shared.Implementation
{
    public class Result<T> : IResult<T>
    {
        public IReadOnlyList<string> Messages { get; protected set; } = new string[0];
        public bool Succeeded { get; protected set; }
        public T Data { get; protected set; }
        public Exception Exception { get; private set; }
        public int Code { get; protected set; }

        public void WithCode(int code)
        {
            Code = code;
        }

        public void WithException(Exception exception)
        {
            Exception = exception;
        }

        public static Result<T> Success()
        {
            return new Result<T> 
            { 
                Succeeded = true,
                Code = 200
            };
        }

        public static Result<T> Success(T data, params string[] messages)
        {
            return new Result<T>
            {
                Succeeded = true,
                Messages = messages ?? new string[0],
                Data = data
            };
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