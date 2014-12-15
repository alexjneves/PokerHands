using PokerHands.Enums;

namespace PokerHands.Model
{
    public class Winner
    {
        public Player Player { get; set; }

        public HandType Hand { get; set; }

        public string Details { get; set; }
    }
}
