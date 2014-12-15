using FluentAssertions;
using NUnit.Framework;
using PokerHands.Enums;

namespace PokerHands.Test
{
    [TestFixture]
    public class PokerHandsTests
    {
        private PokerHands _pokerHands;
        private HandBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _pokerHands = new PokerHands();
            _builder = new HandBuilder();
        }

        [Test]
        public void TestHighCard()
        {
            const string Black = "2H 3D 5S 9C AD";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.HighCard);
            winner.Details.Should().Be("Ace of Diamonds");
        }

    }

    public class HandBuilder
    {
        public string Build(string black, string white)
        {
            return string.Format("Black: {0} White: {1}", black, white);
        }
    }
}
