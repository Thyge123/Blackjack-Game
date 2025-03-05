using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static BlackJackGame.CardSuit;

namespace BlackJackGame.Tests
{
    [TestClass]
    public class HandTests
    {
        [TestMethod]
        public void Constructor_InitializesEmptyHandWithZeroValue()
        {
            // Arrange & Act
            Hand hand = new Hand();

            // Assert
            Assert.IsNotNull(hand.Cards);
            Assert.AreEqual(0, hand.Cards.Count);
            Assert.AreEqual(0, hand.HandValue);
        }

        [TestMethod]
        public void AddCard_AddsCardToHandAndCalculatesValue()
        {
            // Arrange
            Hand hand = new Hand();
            Card card = new Card(Suit.Hearts, CardValue.CardsValue.Ten);

            // Act
            hand.AddCard(card);

            // Assert
            Assert.AreEqual(1, hand.Cards.Count);
            Assert.AreEqual(10, hand.HandValue);
            Assert.AreSame(card, hand.Cards[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCard_ThrowsArgumentNullExceptionForNullCard()
        {
            // Arrange
            Hand hand = new Hand();

            // Act - This should throw ArgumentNullException
            hand.AddCard(null);
        }

        [TestMethod]
        public void CalculateHandValue_CorrectlyCalculatesNonAceCards()
        {
            // Arrange
            Hand hand = new Hand();

            // Act
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Queen));

            // Assert
            Assert.AreEqual(20, hand.HandValue);
        }

        [TestMethod]
        public void CalculateHandValue_HandlesAcesCorrectly_OneAceAsEleven()
        {
            // Arrange
            Hand hand = new Hand();

            // Act
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.Ace));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Ten));

            // Assert - Ace should be counted as 11 for a total of 21
            Assert.AreEqual(21, hand.HandValue);
        }

        [TestMethod]
        public void CalculateHandValue_HandlesAcesCorrectly_OneAceAsOne()
        {
            // Arrange
            Hand hand = new Hand();

            // Act
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.Ace));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Ten));
            hand.AddCard(new Card(Suit.Clubs, CardValue.CardsValue.Queen));

            // Assert - Ace should be counted as 1 for a total of 21
            Assert.AreEqual(21, hand.HandValue);
        }

        [TestMethod]
        public void CalculateHandValue_HandlesMultipleAcesCorrectly()
        {
            // Arrange
            Hand hand = new Hand();

            // Act
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.Ace));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Ace));
            hand.AddCard(new Card(Suit.Clubs, CardValue.CardsValue.Nine));

            // Assert - One ace should be 11, one ace should be 1, for a total of 21
            Assert.AreEqual(21, hand.HandValue);
        }

        [TestMethod]
        public void ClearHand_RemovesAllCardsAndResetsValue()
        {
            // Arrange
            Hand hand = new Hand();
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Queen));

            // Act
            hand.ClearHand();

            // Assert
            Assert.AreEqual(0, hand.Cards.Count);
            Assert.AreEqual(0, hand.HandValue);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            Hand hand = new Hand();
            Card card1 = new Card(Suit.Hearts, CardValue.CardsValue.King);
            Card card2 = new Card(Suit.Diamonds, CardValue.CardsValue.Queen);

            // Act
            hand.AddCard(card1);
            hand.AddCard(card2);
            string result = hand.ToString();

            // Assert
            Assert.IsTrue(result.Contains(card1.ToString()));
            Assert.IsTrue(result.Contains(card2.ToString()));
            Assert.IsTrue(result.Contains(", "));
        }

        [TestMethod]
        public void IsBust_ReturnsTrueWhenValueExceeds21()
        {
            // Arrange
            Hand hand = new Hand();
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Queen));
            hand.AddCard(new Card(Suit.Clubs, CardValue.CardsValue.Two));

            // Act & Assert
            Assert.IsTrue(hand.IsBust());
            Assert.AreEqual(22, hand.HandValue);
        }

        [TestMethod]
        public void IsBust_ReturnsFalseWhenValueIsLessOrEqualTo21()
        {
            // Arrange
            Hand hand = new Hand();
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Queen));

            // Act & Assert
            Assert.IsFalse(hand.IsBust());
            Assert.AreEqual(20, hand.HandValue);
        }

        [TestMethod]
        public void BlackJack_ReturnsTrueWhenValueIs21()
        {
            // Arrange
            Hand hand = new Hand();
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Ace));

            // Act & Assert
            Assert.IsTrue(hand.BlackJack());
            Assert.AreEqual(21, hand.HandValue);
        }

        [TestMethod]
        public void BlackJack_ReturnsFalseWhenValueIsNot21()
        {
            // Arrange
            Hand hand = new Hand();
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.King));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.CardsValue.Queen));

            // Act & Assert
            Assert.IsFalse(hand.BlackJack());
            Assert.AreEqual(20, hand.HandValue);
        }

        [TestMethod]
        public void ToString_HandlesEmptyHand()
        {
            // Arrange
            Hand hand = new Hand();

            // Act
            string result = hand.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CalculateHandValue_HandlesInvalidCardGracefully()
        {
            // Arrange
            Hand hand = new Hand();
            hand.Cards.Add(new Card()); // Uninitialized card

            // Act - Trigger recalculation through AddCard
            hand.AddCard(new Card(Suit.Hearts, CardValue.CardsValue.Two));

            // Assert - We're just testing it doesn't throw exceptions
            Assert.AreEqual(2, hand.Cards.Count);
        }
    }
}
