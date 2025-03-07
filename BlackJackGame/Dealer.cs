using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlackJackGame.CardValue;

namespace BlackJackGame
{
    public class Dealer : Player
    {
        public Dealer() : base()
        {
        }

        // Expose the hand to check its status
        public Hand GetHand()
        {
            return base.Hand;
        }

        // Deal the initial hand
        public void PlayTurn(Deck deck)
        {
            // Dealer must hit on 16 or less, and stand on 17 or more. Must hit on soft 17 (Ace + 6)
            while (GetHand().HandValue < 17 || (GetHand().HandValue == 17 && HasSoftSeventeen()))
            {
                Thread.Sleep(1000);
                Hit(deck);
                Console.WriteLine($"\nDealer hits: {GetHand().HandValue}");
            }

            if (DoesStand)
            {
                Console.WriteLine($"\nDealer stands with {GetHand().HandValue}");
            }
        }

        // Helper method to determine if dealer has a soft 17
        private bool HasSoftSeventeen()
        {
            if (GetHand().HandValue != 17)
                return false;

            return GetHand().Cards.Any(c => c.Value == CardsValue.Ace);
        }

        // Show only the first card (face up card)
        public Card GetFaceUpCard()
        {
            if (GetHand().Cards.Count > 0)
            {
                return GetHand().Cards[0];
            }
            return null;
        }

        // Show all cards in the hand
        public void ShowHand()
        {
            Console.WriteLine("\nDealer's hand:");
            foreach (Card card in GetHand().Cards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine($"\nTotal: {GetHand().HandValue}");
        }

        // Get the value of the first card in the hand
        public int GetHandRoundOne()
        {
            var hand = GetHand();
            var firstCardValue = hand.Cards[0].GetCardValue();
            return firstCardValue;
        }
    }
}