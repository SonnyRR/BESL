namespace BESL.Application.Exceptions
{
    using System;

    public abstract class BaseCustomException : Exception
    {
        public BaseCustomException()
        {
        }

        public BaseCustomException(string message)
            :base(message)
        {
        }
    }
}
