using System;

namespace PokerHands.Exceptions
{
    public class InvalidValueException : CardException
    {
        public InvalidValueException()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
