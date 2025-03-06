using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        // Create a deck of cards
        public Deck()
        {
            Cards = new List<Card>(); // Initialize the deck

            foreach (CardSuit.Suit suit in Enum.GetValues(typeof(CardSuit.Suit))) // Enumerate all card suits
            {
                foreach (CardValue.CardsValue value in Enum.GetValues(typeof(CardValue.CardsValue))) // Enumerate all card suits and values
                {
                    Cards.Add(new Card(suit, value)); // Add each card to the deck
                }
            }

            // Shuffle the deck after creation
            Shuffle();
        }


        /// <summary>
        /// Shuffles all cards in the deck.
        /// This method randomly reorders the cards in the deck to ensure fair gameplay.
        /// </summary>
        public void Shuffle()
        {
            try
            {
                // Create a new random number generator for shuffling
                Random random = new Random();

                // Iterate through each card in the deck
                for (int i = 0; i < Cards.Count; i++)
                {
                    // Select a random index from all cards in the deck
                    int randomIndex = random.Next(Cards.Count);

                    // Swap the current card with the randomly selected card
                    Card temp = Cards[i];          // Store current card temporarily
                    Cards[i] = Cards[randomIndex]; // Replace current card with random card
                    Cards[randomIndex] = temp;     // Place current card at random position
                }
            }
            catch (Exception e)
            {
                // Handle any errors that might occur during shuffling
                // This provides error resilience in case of collection modification issues
                Console.WriteLine("Error shuffling deck: " + e.Message);
            }
        }


        public Card DrawCard()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            if (Cards.Count == 0)
            {
                Console.WriteLine("\nDeck is empty. Creating new deck.");
                CreateNewDeck();
                card = Cards[0]; // Draw the first card from the new deck
                Cards.RemoveAt(0);
            }
            return card;
        }

        // Create a new deck of cards
        private void CreateNewDeck()
        {
            Cards = new List<Card>(); // Clear the current deck

            foreach (CardSuit.Suit suit in Enum.GetValues(typeof(CardSuit.Suit)))
            {
                foreach (CardValue.CardsValue value in Enum.GetValues(typeof(CardValue.CardsValue)))
                {
                    Cards.Add(new Card(suit, value)); // Add each card to the new deck
                }
            }
            Shuffle();
        }

        public override string ToString()
        {
            string deckString = "";
            foreach (Card card in Cards)
            {
                deckString += card.ToString() + "\n";
            }
            return deckString;
        }
    }
}