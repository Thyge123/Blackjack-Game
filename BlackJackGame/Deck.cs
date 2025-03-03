using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Deck
    {
        public int DeckSize { get; set; }
        public List<Card> Cards { get; set; }

        public Deck()
        {
            DeckSize = 52;
            Cards = new List<Card>();

            foreach (CardSuit.Suit suit in Enum.GetValues(typeof(CardSuit.Suit)))
            {
                foreach (CardValue.CardsValue value in Enum.GetValues(typeof(CardValue.CardsValue)))
                {
                    Cards.Add(new Card(suit, value));
                }
            }

            // Shuffle the deck after creation
            Shuffle();

            Console.WriteLine("Deck created and shuffled.");  
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int i = 0; i < Cards.Count; i++)
            {
                int randomIndex = random.Next(Cards.Count);
                Card temp = Cards[i];
                Cards[i] = Cards[randomIndex];
                Cards[randomIndex] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            if(Cards.Count == 0)
            {
                Console.WriteLine("\nDeck is empty. Creating new deck.");
                Deck newDeck = new Deck();
                Cards = newDeck.Cards;
                Shuffle();
            }
            return card;
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
