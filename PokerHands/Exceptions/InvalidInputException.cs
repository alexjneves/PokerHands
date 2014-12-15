using System;

namespace PokerHands.Exceptions
{
    public class InvalidInputException : CardException
    {
        public InvalidInputException()
        {
        }

        public InvalidInputException(string message)
            : base(message)
        {
        }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}