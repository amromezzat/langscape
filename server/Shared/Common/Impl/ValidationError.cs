namespace Shared.Common.Impl
{
    public class ValidationError
    {
        public string Name { get; private set; }
        public string Error { get; private set; }

        public ValidationError(string name, string error)
        {
            Name = name;
            Error = error;
        }
    }
}