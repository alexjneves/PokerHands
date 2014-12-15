using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PokerHands.Enums;
using PokerHands.Exceptions;
using PokerHands.Model;
using PokerHands.Parsers;

namespace PokerHands.Test
{
    [TestFixture]
    public class HandParserTests
    {
        private const int NumberOfCardsInAHand = 5;
        private HandParser _handParser;

        [SetUp]
        public void SetUp()
        {
            _handParser = new HandParser();
        }

        [Test]
        public void WhenSplittingHands_AndPassedValidHands_ShouldReturnCorrectArray()
        {
            const string InputString = "Black: 2H 3D 5S 9C KD White: 2C 3H 4S 8C AH";

            var handStrings = _handParser.SplitHands(InputString);

            handStrings.Length.Should().Be(2);

            handStrings[0].Should().Be("2H 3D 5S 9C KD");
            handStrings[1].Should().Be("2C 3H 4S 8C AH");
        }

        [Test]
        public void WhenSplittingHands_AndPassedInvalidInput_ShouldThrowInvalidHandException()
        {
            const string InputString = "White: 2H 3D 5S 9C KD Black: 2C 3H 4S 8C AH";

            Action split = () => _handParser.SplitHands(InputString);

            split.ShouldThrow<InvalidHandException>();
        }

        [Test]
        public void WhenPassedValidHand_ShouldSucceed()
        {
            const string ValidHand = "2H 3D 5S 9C KD";

            var hand = _handParser.ParseHand(ValidHand);

            hand.Cards.Count.Should().Be(NumberOfCardsInAHand);

            var cards = new List<Card>
            {
                new Card {Value = Value.Two, Suit = Suit.Hearts},
                new Card {Value = Value.Three, Suit = Suit.Diamonds},
                new Card {Value = Value.Five, Suit = Suit.Spades},
                new Card {Value = Value.Nine, Suit = Suit.Clubs},
                new Card {Value = Value.King, Suit = Suit.Diamonds}
            };

            var parsedCards = hand.Cards;

            parsedCards.ShouldBeEquivalentTo(cards);
        }

        [Test]
        public void WhenPassedInvalidHand_ShouldThrowInvalidHandException()
        {
            const string InvalidHand = "2H 3D 5S 9C";

            Action parse = () => _handParser.ParseHand(InvalidHand);

            parse.ShouldThrow<InvalidHandException>();
        }
    }
}