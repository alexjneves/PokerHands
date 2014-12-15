using System;
using System.Text.RegularExpressions;
using PokerHands.Exceptions;
using PokerHands.Model;

namespace PokerHands.Parsers
{
    public class HandParser
    {
        public string[] SplitHands(string handStrings)
        {
            var hands = new string[2];

            var regex = new Regex("Black: (.*) White: (.*)");

            var match = regex.Match(handStrings);

            if (!match.Success || match.Groups.Count != 3)
            {
                throw new InvalidHandException("Hand does not match pattern \"Black: <hand> White: <hand>\" ");
            }

            hands[0] = match.Groups[1].Value;
            hands[1] = match.Groups[2].Value;

            return hands;
        }

        public Hand ParseHand(string handString)
        {
            var cardStrings = SplitHand(handString);

            if (cardStrings.Length != 5)
            {
                throw new InvalidHandException();
            }

            var cardParser = new CardParser();
            var hand = new Hand();

            foreach (var cardString in cardStrings)
            {
                try
                {
                    var card = cardParser.Parse(cardString);
                    hand.Cards.Add(card);
                }
                catch (CardException e)
                {
                    throw new InvalidHandException("Invalid hand", e);
                }
            }

            return hand;
        }

        private static string[] SplitHand(string hand)
        {
            return hand.Split(' ');
        }
    }
}
