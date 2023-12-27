namespace Domain.Exceptions.Register
{
    public class DuplicateUsernameException : RegisterException
    {
        public DuplicateUsernameException() : base("Username already exists")
        {
        }
    }
}