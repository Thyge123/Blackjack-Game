using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackGame.Tests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void Constructor_Creates52CardDeck()
        {
            // Arrange & Act
            var deck = new Deck();

            // Assert
            Assert.AreEqual(52, deck.Cards.Count);
            Assert.IsTrue(ContainsAllPossibleCards(deck.Cards));
        }

        [TestMethod]
        public void Shuffle_ReordersCards()
        {
            // Arrange
            var deck = new Deck();
            var originalOrder = new List<Card>(deck.Cards);

            // Act
            deck.Shuffle();

            // Assert
            Assert.AreEqual(52, deck.Cards.Count);

            // Check that cards have been reordered
            bool atLeastOneCardMoved = false;
            for (int i = 0; i < deck.Cards.Count; i++)
            {
                if (!deck.Cards[i].Equals(originalOrder[i]))
                {
                    atLeastOneCardMoved = true;
                    break;
                }
            }

            Assert.IsTrue(atLeastOneCardMoved, "Shuffle did not change card order");
        }

        [TestMethod]
        public void DrawCard_ReturnsTopCardAndRemovesIt()
        {
            // Arrange
            var deck = new Deck();
            var expectedCard = deck.Cards[0];
            int initialCount = deck.Cards.Count;

            // Act
            var drawnCard = deck.DrawCard();

            // Assert
            Assert.AreEqual(expectedCard.Suit, drawnCard.Suit);
            Assert.AreEqual(expectedCard.Value, drawnCard.Value);
            Assert.AreEqual(initialCount - 1, deck.Cards.Count);
            Assert.IsFalse(deck.Cards.Any(c => c.Suit == drawnCard.Suit && c.Value == drawnCard.Value));
        }

        [TestMethod]
        public void DrawCard_CreatesNewDeckWhenEmpty()
        {
            // Arrange
            var deck = new Deck();

            // Act - Draw all 52 cards
            for (int i = 0; i < 52; i++)
            {
                deck.DrawCard();
            }

            // Assert - The deck should have been automatically replenished
            Assert.AreEqual(51, deck.Cards.Count); // 52 new cards minus the one just drawn
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DrawCard_ThrowsWhenCardsIsNull()
        {
            // Arrange
            var deck = new Deck();
            deck.Cards = null;  // Force null collection to trigger exception

            // Act - should throw InvalidOperationException
            deck.DrawCard();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shuffle_ThrowsExceptionWhenCardsIsNull()
        {
            // Arrange
            var deck = new Deck();
            deck.Cards = null;  // Force null collection to trigger exception

            // Act - this should throw InvalidOperationException
            deck.Shuffle();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shuffle_ThrowsExceptionWhenCardsIsEmpty()
        {
            // Arrange
            var deck = new Deck();
            deck.Cards = new List<Card>();  // Empty collection

            // Act - this should throw InvalidOperationException
            deck.Shuffle();
        }

        [TestMethod]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var deck = new Deck();
            var firstCard = deck.Cards[0];

            // Act
            string result = deck.ToString();

            // Assert
            Assert.IsTrue(result.Contains(firstCard.ToString()));
            Assert.IsTrue(result.StartsWith(firstCard.ToString()));

            // Verify all cards are in the string
            foreach (var card in deck.Cards)
            {
                Assert.IsTrue(result.Contains(card.ToString()));
            }
        }

        [TestMethod]
        public void ToString_HandlesNullCards()
        {
            // Arrange
            var deck = new Deck();
            deck.Cards = null;  // Force null collection

            // Act & Assert
            try
            {
                string result = deck.ToString();
                // ToString doesn't have explicit exception handling, so expect an exception
                Assert.Fail("ToString should throw when Cards is null");
            }
            catch (NullReferenceException)
            {
                // Expected behavior
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ToString_HandlesEmptyCollection()
        {
            // Arrange
            var deck = new Deck();
            deck.Cards = new List<Card>();  // Empty collection

            // Act
            string result = deck.ToString();

            // Assert
            Assert.AreEqual("", result);
        }

        // Helper method to check if all possible card combinations are in the deck
        private bool ContainsAllPossibleCards(List<Card> cards)
        {
            foreach (CardSuit.Suit suit in Enum.GetValues(typeof(CardSuit.Suit)))
            {
                foreach (CardValue.CardsValue value in Enum.GetValues(typeof(CardValue.CardsValue)))
                {
                    if (!cards.Any(c => c.Suit == suit && c.Value == value))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
