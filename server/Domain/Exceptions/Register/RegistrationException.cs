using System;

namespace Domain.Exceptions.Register
{
    public class RegisterException : Exception
    {
        public RegisterException() : base("Something went wrong while registering")
        {
        }

        public RegisterException(string message) : base(message)
        {
        }
    }
}