using FluentAssertions;
using NUnit.Framework;
using PokerHands.Enums;

namespace PokerHands.Test
{
    [TestFixture]
    public class PokerHandsTests
    {
        private const string White = "2D 3H 5S 7C 9H";
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

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.TwoPair);
            winner.Details.Should().Be("TwoPair: Twos and Fives");
        }

        [Test]
        public void TestThreeOfAKind()
        {
            const string Black = "2H 2D 2S 9C AD";

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

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.Straight);
            winner.Details.Should().Be("Straight: 2 3 4 5 6");
        }

        [Test]
        public void TestFlush()
        {
            const string Black = "2H 3H 5H 6H 9H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.Flush);
            winner.Details.Should().Be("Flush: Hearts");
        }

        [Test]
        public void TestStraightFlush()
        {
            const string Black = "2H 3H 4H 5H 6H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.StraightFlush);
            winner.Details.Should().Be("StraightFlush");
        }

        [Test]
        public void TestFullHouse()
        {
            const string Black = "2H 2D 5S 5C 5H";

            var hands = _builder.Build(Black, White);

            var winner = _pokerHands.CalculateWinner(hands);

            winner.Player.Should().Be(Player.Black);
            winner.Hand.Should().Be(HandType.FullHouse);
            winner.Details.Should().Be("FullHouse");
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
