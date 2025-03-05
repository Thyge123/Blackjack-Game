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
        public bool DoesStand { get; set; }

        public Player(Hand hand)
        {
            Hand = hand;
            DoesStand = false;
        }

        public Player()
        {
            Hand = new Hand();
        }

        public Hand DealInitialHand(Deck deck)
        {
            ArgumentNullException.ThrowIfNull(deck, nameof(deck));

            DoesStand = false;
            Hand.ClearHand();

            // Deal two cards to start
            Hand.AddCard(deck.DrawCard());
            Hand.AddCard(deck.DrawCard());

            return Hand;
        }

        public void Hit(Deck deck)
        {
            ArgumentNullException.ThrowIfNull(deck, nameof(deck));

            Hand.AddCard(deck.DrawCard());
            GetHandValue();
        }


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