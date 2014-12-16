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

        [Test]
        public void TestPair()
        {
            const string Black = "2H 2D 5S 9C AD";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.Pair);
            winner.Details.Should().Be("Pair: Twos");
        }

        [Test]
        public void TestTwoPair()
        {
            const string Black = "2H 2D 5S 5C AD";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.TwoPair);
            winner.Details.Should().Be("TwoPair: Fives and Twos");
        }

        [Test]
        public void TestThreeOfAKind()
        {
            const string Black = "2H 2D 2S 9C AD";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.ThreeOfAKind);
            winner.Details.Should().Be("ThreeOfAKind: Twos");
        }

        [Test]
        public void TestFourOfAKind()
        {
            const string Black = "2H 2D 2S 2C AD";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.FourOfAKind);
            winner.Details.Should().Be("FourOfAKind: Twos");
        }

        [Test]
        public void TestStraight()
        {
            const string Black = "2H 3D 4S 5C 6D";
            const string White = "2D 3H 5S 7C 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.Straight);
            winner.Details.Should().Be("Straight: 2 3 4 5 6");
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
