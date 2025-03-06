using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Player
    {
        public Hand Hand { get; protected set; } // Player's hand
        public bool DoesStand { get; set; }

        public Player(Hand hand)
        {
            Hand = hand; // Initialize the hand
            DoesStand = false;
        }

        public Player()
        {
            Hand = new Hand(); // Initialize the hand
        }

        // Deal the initial hand
        public Hand DealInitialHand(Deck deck)
        {
            ArgumentNullException.ThrowIfNull(deck, nameof(deck)); // Check if deck is null

            DoesStand = false; // Reset stand status  
            Hand.ClearHand(); // Clear the hand

            // Deal two cards to start
            Hand.AddCard(deck.DrawCard());
            Hand.AddCard(deck.DrawCard());

            return Hand;
        }

        // Player's turn
        public void Hit(Deck deck)
        {
            ArgumentNullException.ThrowIfNull(deck, nameof(deck)); // Check if deck is null

            Hand.AddCard(deck.DrawCard()); // Draw a card
            GetHandValue(); // Get the hand value
        }


        // Player stands
        public bool Stand()
        {
            Console.WriteLine($"\nPlayer stands with {GetHandValue()}");
            DoesStand = true;
            return DoesStand;
        }

        // Check if player has gone bust
        public bool IsBust()
        {
            return Hand.IsBust();
        }

        // Check if player has blackjack
        public bool HasBlackjack()
        {
            return Hand.BlackJack();
        }

        // Get the current hand value
        public int GetHandValue()
        {
            return Hand.HandValue;
        }
    }
}