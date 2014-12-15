using System;

namespace PokerHands.Exceptions
{
    public class InvalidSuitException : CardException
    {
        public InvalidSuitException()
        {
        }

        public InvalidSuitException(string message) : base(message)
        {
        }

        public InvalidSuitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}