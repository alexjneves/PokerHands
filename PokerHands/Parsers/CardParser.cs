using System;
using PokerHands.Enums;
using PokerHands.Exceptions;
using PokerHands.Model;

namespace PokerHands.Parsers
{
    public class CardParser
    {
        public Card Parse(string card)
        {
            var length = card.Length;

            if (length < 2 || length > 3)
            {
                throw new InvalidInputException();
            }

            var suitChar = card[length - 1];
            var valueString = card.Substring(0, length - 1);

            var suit = SuitFromChar(suitChar);
            var value = ValueFromString(valueString);

            return new Card
            {
                Value = value,
                Suit = suit
            };
        }

        private Value ValueFromString(string valueString)
        {
            int parsedValue;

            if (!Int32.TryParse(valueString, out parsedValue))
            {
                switch (valueString)
                {
                    case "J":
                        return Value.Jack;
                    case "Q":
                        return Value.Queen;
                    case "K":
                        return Value.King;
                    case "A":
                        return Value.Ace;
                    default:
                        throw new InvalidValueException();
                }
            }

            if (parsedValue < 2 || parsedValue > 10)
            {
                throw new InvalidValueException();
            }

            return (Value) parsedValue;

        }

        private Suit SuitFromChar(char suit)
        {
            switch (suit)
            {
                case 'D':
                    return Suit.Diamonds;
                case 'S':
                    return Suit.Spades;
                case 'H':
                    return Suit.Hearts;
                case 'C':
                    return Suit.Clubs;
                default:
                    throw new InvalidSuitException();
            }
        }
    }
}
