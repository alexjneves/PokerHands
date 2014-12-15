using System;
using System.Runtime.InteropServices;
using PokerHands.Enums;
using PokerHands.Model;
using PokerHands.Parsers;

namespace PokerHands
{
    public class PokerHands
    {
        private readonly HandParser _handParser;

        public PokerHands()
        {
            _handParser = new HandParser();
        }

        public Winner CalculateWinner(string hands)
        {
            var handStrings = _handParser.SplitHands(hands);

            return CalculateWinner(handStrings[0], handStrings[1]);
        }

        private Winner CalculateWinner(string black, string white)
        {
            var blackHand = _handParser.ParseHand(black);
            var whiteHand = _handParser.ParseHand(white);

            var winningPlayer = DetermineWinner(blackHand, whiteHand);
            var winningHand = winningPlayer == Player.Black ? blackHand : whiteHand;

            return new Winner
            {
                Player = winningPlayer, 
                Hand = winningHand.HandType, 
                Details = winningHand.Details
            };
        }

        private Player DetermineWinner(Hand black, Hand white)
        {
            return black.HandType >= white.HandType ? Player.Black : Player.White;
        }
    }
}
