using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Player
    {
        public Hand Hand { get; protected set; }

        public Player(Hand hand)
        {
            Hand = hand;
        }

        public Player()
        {
            Hand = new Hand();
        }

        public Hand DealInitialHand(Deck deck)
        {
            // Clear the hand before dealing
            Hand.ClearHand();

            // Deal two cards to start
            Hand.AddCard(deck.DrawCard());
            Hand.AddCard(deck.DrawCard());

      
            // Return the complete hand
            return Hand;
        }

        public void Hit(Deck deck)
        {
            Hand.AddCard(deck.DrawCard());
            GetHandValue();
        }

        public void Stand()
        {
            // Do nothing
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
