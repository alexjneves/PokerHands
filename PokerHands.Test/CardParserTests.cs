using System;
using FluentAssertions;
using NUnit.Framework;
using PokerHands.Enums;
using PokerHands.Exceptions;
using PokerHands.Parsers;

namespace PokerHands.Test
{
    [TestFixture]
    class CardParserTests
    {
        private CardParser _cardParser;

        [SetUp]
        public void SetUp()
        {
            _cardParser = new CardParser();
        }

        [Test]
        public void WhenPassedACharacterD_ShouldReturnDiamond()
        {
            var result = _cardParser.Parse("2D");

            result.Suit.Should().Be(Suit.Diamonds);
        }

        [Test]
        public void WhenPassedACharacterS_ShouldReturnSpade()
        {
            var result = _cardParser.Parse("2S");

            result.Suit.Should().Be(Suit.Spades);
        }

        [Test]
        public void WhenPassedACharacterH_ShouldReturnHeart()
        {
            var result = _cardParser.Parse("2H");

            result.Suit.Should().Be(Suit.Hearts);   
        }

        [Test]
        public void WhenPassedACharacterC_ShouldReturnClub()
        {
            var result = _cardParser.Parse("2C");

            result.Suit.Should().Be(Suit.Clubs);
        }

        [Test]
        public void WhenPassedValueTwo_ShouldReturnTwo()
        {
            var result = _cardParser.Parse("2C");

            result.Value.Should().Be(Value.Two);
        }

        [Test]
        public void WhenPassedValueTen_ShouldReturnTen()
        {
            var result = _cardParser.Parse("10C");

            result.Value.Should().Be(Value.Ten);
        }

        [Test]
        public void WhenPassedAce_ShouldReturnAce()
        {
            var result = _cardParser.Parse("AC");

            result.Value.Should().Be(Value.Ace);
        }

        [Test]
        public void WhenPassedInvalidInput_ShouldThrowInvalidInputException()
        {
            Action parseCard = () => _cardParser.Parse("BadInput");

            parseCard.ShouldThrow<InvalidInputException>();
        }

        [Test]
        public void WhenPassedInvalidValue_ShouldThrowInvalidValueException()
        {
            Action parseCard = () => _cardParser.Parse("1C");

            parseCard.ShouldThrow<InvalidValueException>();
        }

        [Test]
        public void WhenPassedInvalidSuit_ShouldThrowInvalidSuitException()
        {
            Action parseCard = () => _cardParser.Parse("2E");

            parseCard.ShouldThrow<InvalidSuitException>();
        }

    }
}
