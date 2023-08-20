namespace Langscape.Shared
{
    public interface IResult<T>
    {
        IReadOnlyList<string> Messages { get; }
        bool Succeeded { get; }
        T Data { get; }
        Exception Exception { get; }
        int Code { get; }
    }
}