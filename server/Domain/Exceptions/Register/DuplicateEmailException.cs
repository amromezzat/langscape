namespace Domain.Exceptions.Register
{
    public class DuplicateEmailException : RegisterException
    {
        public DuplicateEmailException() : base("Email already exists")
        {
        }
    }
}