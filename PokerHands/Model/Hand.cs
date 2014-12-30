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
            var flush = Flush();
            var straight = Straight();

            if (flush && straight)
            {
                SetProperties(HandType.StraightFlush, "StraightFlush");
                return;
            }

            if (OfAKind(HandType.FourOfAKind))
            {
                return;
            }

            var threeOfAKind = OfAKind(HandType.ThreeOfAKind);
            var pair = Pairs();

            if (threeOfAKind && pair)
            {
                SetProperties(HandType.FullHouse, "FullHouse");
                return;
            }

            if (flush || straight)
            {
                return;
            }

            if (threeOfAKind)
            {
                return;
            }

            if (pair)
            {
                return;
            }

            HighCard();
        }

        private bool Flush()
        {
            var suit = Cards.First().Suit;

            var matches = Cards.Where(c => c.Suit == suit);

            if (matches.Count() == 5)
            {
                var details = string.Format("Flush: {0}", suit);
                SetProperties(HandType.Flush, details);
                return true;
            }

            return false;
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

            var details = detailsString + " " + (int) orderedValues.Last();

            SetProperties(HandType.Straight, details);

            return true;
        }

        private bool Pairs()
        {
            var pairs = new List<Value>(2);

            foreach (var card in Cards)
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
                    var details = string.Format("{0}: {1}s and {2}s", _handType, pairs[0], pairs[1]);
                    SetProperties(HandType.TwoPair, details);
                    return true;
                default:
                    return false;
            }
        }

        private bool OfAKind(HandType handType)
        {
            var count = handType == HandType.ThreeOfAKind ? 3 : 4;

            foreach (var card in Cards)
            {
                var value = card.Value;

                var matches = Cards.Where(c => c.Value == value);

                if (matches.Count() == count)
                {
                    SetProperties(handType, value);
                    return true;
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

            SetProperties(HandType.HighCard, highestCard.ToString());
        }

        private void SetProperties(HandType handType, Value value)
        {
            var details = string.Format("{0}: {1}s", handType, value);
            SetProperties(handType, details);
        }

        private void SetProperties(HandType handType, string details)
        {
            _handType = handType;
            _details = details;
        }
    }
}
