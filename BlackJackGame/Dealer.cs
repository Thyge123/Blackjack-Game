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

        // Dealer plays according to house rules
        public void PlayTurn(Deck deck)
        {
            // Dealer must hit on 16 or less, and stand on 17 or more
            // Special rule: Dealer must hit on soft 17 (Ace + 6)
            while (GetHand().HandValue < 17 || (GetHand().HandValue == 17 && HasSoftSeventeen()))
            {
                Hit(deck);
                Console.WriteLine($"Dealer hits: {GetHand().HandValue}");
            }

            if (IsBust())
            {
                Console.WriteLine("Dealer busts!");
            }
            else
            {
                Console.WriteLine($"Dealer stands with {GetHand().HandValue}");
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
    }
}
