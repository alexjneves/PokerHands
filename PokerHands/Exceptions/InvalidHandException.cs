using System;

namespace PokerHands.Exceptions
{
    public class InvalidHandException : Exception
    {
        public InvalidHandException()
        {
        }

        public InvalidHandException(string message) : base(message)
        {
        }

        public InvalidHandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}