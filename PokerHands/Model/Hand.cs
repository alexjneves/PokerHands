using System.Collections.Generic;
using System.Linq;
using PokerHands.Enums;

namespace PokerHands.Model
{
    public class Hand
    {
        public List<Card> Cards { get; private set; }

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
            if (OfAKind(HandType.FourOfAKind))
            {
                return;
            }

            if (Straight())
            {
                return;
            }

            if (OfAKind(HandType.ThreeOfAKind))
            {
                return;
            }

            if (Pairs())
            {
                return;
            }

            HighCard();
        }

        private bool Straight()
        {
            var values = Cards.Select(c => c.Value);
            var orderedValues = values.OrderBy(c => c).ToList();

            var detailsString = "Straight:";

            for (var i = 0; i < orderedValues.Count - 1; ++i)
            {
                var difference = orderedValues[i + 1] - orderedValues[i];

                if (difference != 1)
                {
                    return false;
                }

                detailsString += " " + (int) orderedValues[i];
            }

            _handType = HandType.Straight;
            _details = detailsString + " " + (int) orderedValues.Last();

            return true;
        }

        private bool Pairs()
        {
            var rankedByValue = Cards.OrderByDescending(c => c.Value);

            var pairs = new List<Value>(2);

            foreach (var card in rankedByValue)
            {
                var value = card.Value;

                var matches = Cards.Where(c => c.Value == value);

                if (matches.Count() == 2)
                {
                    pairs.Add(value);
                }
            }

            pairs = pairs.Distinct().ToList();

            switch (pairs.Count)
            {
                case 1:
                    SetProperties(HandType.Pair, pairs[0]);
                    return true;
                case 2:
                    _handType = HandType.TwoPair;
                    _details = string.Format("{0}: {1}s and {2}s", _handType, pairs[0], pairs[1]);
                    return true;
                default:
                    return false;
            }
        }

        private bool OfAKind(HandType handType)
        {
            var count = handType == HandType.ThreeOfAKind ? 3 : 4;

            var rankedByValue = Cards.OrderByDescending(c => c.Value);

            foreach (var card in rankedByValue)
            {
                var value = card.Value;

                var matches = Cards.Where(c => c.Value == value);

                if (matches.Count() == count)
                {
                    SetProperties(handType, value);
                }
            }

            return false;
        }

        private void HighCard()
        {
            var highestCard = Cards[0];

            for (var i = 1; i < Cards.Count; ++i)
            {
                var card = Cards[i];

                if (card.Value > highestCard.Value)
                {
                    highestCard = card;
                }
            }

            _handType = HandType.HighCard;
            _details = highestCard.ToString();
        }

        private void SetProperties(HandType handType, Value value)
        {
            _handType = handType;
            _details = string.Format("{0}: {1}s", handType, value);
        }
    }
}
