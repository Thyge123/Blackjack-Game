using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJackGame;
using System.Collections.Generic;
using static BlackJackGame.CardSuit;
using static BlackJackGame.CardValue;

namespace BlackJackGame.Tests
{
    [TestClass]
    public class PlayerTests
    {
        private Player player;
        private Deck deck;

        [TestInitialize]
        public void Setup()
        {
            player = new Player();
            deck = new Deck();
            deck.Shuffle();
        }

        [TestMethod]
        public void Constructor_WithNoParameters_CreatesNewHand()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.IsNotNull(player.Hand);
            Assert.AreEqual(0, player.Hand.Cards.Count);
            Assert.IsFalse(player.DoesStand);
        }

        [TestMethod]
        public void Constructor_WithHandParameter_SetsHandProperty()
        {
            // Arrange
            var hand = new Hand();

            // Act
            var player = new Player(hand);

            // Assert
            Assert.AreEqual(hand, player.Hand);
            Assert.IsFalse(player.DoesStand);
        }

        [TestMethod]
        public void DealInitialHand_ShouldDealTwoCards()
        {
            // Arrange
            // Using setup

            // Act
            var hand = player.DealInitialHand(deck);

            // Assert
            Assert.AreEqual(2, hand.Cards.Count);
            Assert.IsFalse(player.DoesStand);
        }

        [TestMethod]
        public void DealInitialHand_ShouldClearHandBeforeDealing()
        {
            // Arrange
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Ace));

            // Act
            player.DealInitialHand(deck);

            // Assert
            Assert.AreEqual(2, player.Hand.Cards.Count);
        }

        [TestMethod]
        public void Hit_ShouldAddOneCardToHand()
        {
            // Arrange
            player.DealInitialHand(deck);
            int initialCount = player.Hand.Cards.Count;

            // Act
            player.Hit(deck);

            // Assert
            Assert.AreEqual(initialCount + 1, player.Hand.Cards.Count);
        }

        [TestMethod]
        public void Stand_ShouldSetDoesStandToTrue()
        {
            // Arrange
            player.DoesStand = false;

            // Act
            bool result = player.Stand();

            // Assert
            Assert.IsTrue(player.DoesStand);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBust_WhenHandValueOver21_ReturnsTrue()
        {
            // Arrange
            // Create a hand that's guaranteed to be bust
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Ten));
            player.Hand.AddCard(new Card(Suit.Diamonds, CardsValue.Ten));
            player.Hand.AddCard(new Card(Suit.Clubs, CardsValue.Five));

            // Act
            bool result = player.IsBust();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBust_WhenHandValueNotOver21_ReturnsFalse()
        {
            // Arrange
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Ten));
            player.Hand.AddCard(new Card(Suit.Diamonds, CardsValue.Eight));

            // Act
            bool result = player.IsBust();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasBlackjack_WithAceAndFaceCard_ReturnsTrue()
        {
            // Arrange
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Ace));
            player.Hand.AddCard(new Card(Suit.Diamonds, CardsValue.King));

            // Act
            bool result = player.HasBlackjack();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasBlackjack_WithoutBlackjack_ReturnsFalse()
        {
            // Arrange
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Eight));
            player.Hand.AddCard(new Card(Suit.Diamonds, CardsValue.Nine));

            // Act
            bool result = player.HasBlackjack();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHandValue_ReturnsCorrectValue()
        {
            // Arrange
            player.Hand.AddCard(new Card(Suit.Hearts, CardsValue.Eight));
            player.Hand.AddCard(new Card(Suit.Diamonds, CardsValue.Nine));
            // Total should be 17

            // Act
            int handValue = player.GetHandValue();

            // Assert
            Assert.AreEqual(17, handValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DealInitialHand_WithNullDeck_ThrowsException()
        {
            // Arrange
            Deck nullDeck = null;

            // Act
            player.DealInitialHand(nullDeck);

            // Assert handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Hit_WithNullDeck_ThrowsException()
        {
            // Arrange
            Deck nullDeck = null;

            // Act
            player.Hit(nullDeck);

            // Assert handled by ExpectedException
        }
    }
}
