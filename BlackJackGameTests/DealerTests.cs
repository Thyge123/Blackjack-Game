using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using static BlackJackGame.CardSuit;
using static BlackJackGame.CardValue;

namespace BlackJackGame.Tests
{
    [TestClass]
    public class DealerTests
    {
        private Dealer _dealer;
        private Deck _deck;


        [TestInitialize]
        public void Setup()
        {
            _dealer = new Dealer();
            _deck = new Deck();
        }

        [TestMethod]
        public void GetHand_ReturnsNotNull()
        {
            // Act
            var hand = _dealer.GetHand();

            // Assert
            Assert.IsNotNull(hand);
        }

        [TestMethod]
        public void PlayTurn_DealerStandsAt17OrHigher()
        {
            // Arrange
            // Create a hand with a value of 17 (without soft 17)
            _dealer.DealInitialHand(_deck);

            // Force a specific hand value
            while (_dealer.GetHand().HandValue < 17 || (_dealer.GetHand().HandValue == 17 && _dealer.GetHand().Cards.Exists(c => c.Value == CardsValue.Ace)))
            {
                _dealer.Hit(_deck);
            }

            int initialHandValue = _dealer.GetHand().HandValue;

            // Act
            _dealer.PlayTurn(_deck);

            // Assert
            Assert.IsTrue(_dealer.GetHand().HandValue >= 17);
            // If initial value was already 17 or higher (but not soft 17), it should remain unchanged
            if (initialHandValue >= 17 && !(_dealer.GetHand().Cards.Exists(c => c.Value == CardsValue.Ace) && initialHandValue == 17))
            {
                Assert.AreEqual(initialHandValue, _dealer.GetHand().HandValue);
            }
        }

        [TestMethod]
        public void HasSoftSeventeen_WithAceAnd6_ReturnsTrue()
        {
            // Arrange
            // Create a fresh dealer
            _dealer = new Dealer();
            // Deal initial hand and then clear it
            _dealer.DealInitialHand(_deck);
            _dealer.GetHand().ClearHand();
            // Add our test cards
            _dealer.GetHand().AddCard(new Card(Suit.Hearts, CardsValue.Ace));
            _dealer.GetHand().AddCard(new Card(Suit.Spades, CardsValue.Six));

            // Act
            bool result = PrivateMethodInvoker.InvokePrivateMethod<bool>(_dealer, "HasSoftSeventeen");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasSoftSeventeen_WithoutAce_ReturnsFalse()
        {
            // Arrange
            // Create a fresh dealer
            _dealer = new Dealer();
            // Deal initial hand and then clear it
            _dealer.DealInitialHand(_deck);
            _dealer.GetHand().ClearHand();
            // Add our test cards
            _dealer.GetHand().AddCard(new Card(Suit.Hearts, CardsValue.Ten));
            _dealer.GetHand().AddCard(new Card(Suit.Spades, CardsValue.Seven));

            // Act
            bool result = PrivateMethodInvoker.InvokePrivateMethod<bool>(_dealer, "HasSoftSeventeen");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSoftSeventeen_NotSeventeen_ReturnsFalse()
        {
            // Arrange
            // Create a fresh dealer
            _dealer = new Dealer();
            // Deal initial hand and then clear it
            _dealer.DealInitialHand(_deck);
            _dealer.GetHand().ClearHand();
            // Add our test cards
            _dealer.GetHand().AddCard(new Card(Suit.Hearts, CardsValue.Ace));
            _dealer.GetHand().AddCard(new Card(Suit.Spades, CardsValue.Ten)); // Value is 21, not 17

            // Act
            bool result = PrivateMethodInvoker.InvokePrivateMethod<bool>(_dealer, "HasSoftSeventeen");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetFaceUpCard_WithCards_ReturnsFirstCard()
        {
            // Arrange
            _dealer.DealInitialHand(_deck);
            var expectedCard = _dealer.GetHand().Cards[0];

            // Act
            var result = _dealer.GetFaceUpCard();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCard, result);
        }

        [TestMethod]
        public void GetFaceUpCard_EmptyHand_ReturnsNull()
        {
            // Arrange - default dealer has empty hand

            // Act
            var result = _dealer.GetFaceUpCard();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetHandRoundOne_ReturnsFirstCardValue()
        {
            // Arrange
            _dealer = new Dealer();
            // Deal initial hand and then clear it
            _dealer.DealInitialHand(_deck);
            _dealer.GetHand().ClearHand();
            // Add our test card
            _dealer.GetHand().AddCard(new Card(Suit.Hearts, CardsValue.King)); // Value 10

            // Act
            int result = _dealer.GetHandRoundOne();

            // Assert
            Assert.AreEqual(10, result);
        }

        // Helper class for calling private methods in tests
        private static class PrivateMethodInvoker
        {
            public static T InvokePrivateMethod<T>(object instance, string methodName, params object[] parameters)
            {
                var methodInfo = instance.GetType().GetMethod(methodName,
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (methodInfo == null)
                    throw new ArgumentException($"Method {methodName} not found");

                return (T)methodInfo.Invoke(instance, parameters);
            }
        }
    }
}
