using System;
using System.Collections.Generic;
using PokerHands.Enums;

namespace PokerHands.Model
{
    public class Hand
    {
        public List<Card> Cards { get; set; }

        private HandType _handType;
        private string _details;

        public HandType HandType
        {
            get
            {
                InterpretHand();
                return _handType;
            }
        }

        public string Details
        {
            get
            {
                InterpretHand();
                return _details;
            }
        }

        public Hand()
        {
            Cards = new List<Card>();
        }

        private void InterpretHand()
        {
            if (HighCard())
            {
                return;
            }

            throw new NotImplementedException();
        }

        private bool HighCard()
        {
            _handType = HandType.HighCard;

            var highestCard = Cards[0];

            for (var i = 1; i < Cards.Count; ++i)
            {
                var card = Cards[i];

                if (card.Value > highestCard.Value)
                {
                    highestCard = card;
                }
            }

            _details = highestCard.ToString();

            return true;
        }
    }
}
