using PokerHands.Enums;

namespace PokerHands.Model
{
    public class Card
    {
        public Value Value { get; set; }

        public Suit Suit { get; set; }

        public override string ToString()
        {
            return string.Format("{0} of {1}", Value, Suit);
        }
    }
}
