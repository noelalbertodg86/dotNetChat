using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomizeException
{
    public class UserTakenException : Exception
    {
        public UserTakenException():
            base("Sorry this username is already taken.")
        {
        }

        public UserTakenException(string message)
            : base(message)
        {
        }

        public UserTakenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class UserOrPasswordIncorrectException : Exception
    {
        public UserOrPasswordIncorrectException()
            : base("User or password incorrect")
        {
        }

        public UserOrPasswordIncorrectException(string message)
            : base(message)
        {
        }

        public UserOrPasswordIncorrectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("Sorry we can not find the request user")
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class UserDisconectedException : Exception
    {
        public UserDisconectedException()
            : base()
        {
        }

        public UserDisconectedException(string message)
            : base(message)
        {
        }

        public UserDisconectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
