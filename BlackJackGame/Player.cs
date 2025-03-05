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
            DoesStand = false;
            try
            {
                if (deck == null)
                {
                    throw new NullReferenceException("Deck is null");
                }
                // Clear the hand before dealing
                Hand.ClearHand();
                // Deal two cards to start
                Hand.AddCard(deck.DrawCard());
                Hand.AddCard(deck.DrawCard());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error dealing initial hand: " + e.Message);
                throw; 
            }

            return Hand;
        }


        public void Hit(Deck deck)
        {
            try
            {
                if(deck == null)
                {
                    throw new NullReferenceException("Deck is null");
                }
                Hand.AddCard(deck.DrawCard());
                GetHandValue();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error drawing card: " + e.Message);
                throw; 
            }
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
            try
            {
                return Hand.IsBust();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error checking if player is bust: " + e.Message);
                return false;
            }
        }

        // Check if player has blackjack
        public bool HasBlackjack()
        {
            try
            {
                return Hand.BlackJack();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error checking if player has blackjack: " + e.Message);
                return false;
            }
        }

        // Get the current hand value
        public int GetHandValue()
        {
            try
            {
                return Hand.HandValue;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting hand value: " + e.Message);
                return 0;
            }
        }
    }
}