using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int HandValue { get; set; }

        public Hand()
        {
            Cards = [];
            HandValue = 0;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
0
            CalculateHandValue();
        }

        private void CalculateHandValue()
        {
            HandValue = 0;
            int aceCount = 0;

            // First, count aces and sum non-ace cards
            foreach (Card card in Cards)
            {
                if (card.Value == CardValue.CardsValue.Ace)
                {
                    aceCount++;
                }
                else
                {
                    HandValue += card.GetCardValue();
                }
            }

            // Add all aces as 1 initially
            HandValue += aceCount;

            // Try to add 10 for each ace if it doesn't cause a bust
            for (int i = 0; i < aceCount; i++)
            {
                if (HandValue + 10 <= 21)
                {
                    HandValue += 10;
                }
                else
                {
                    break;
                }
            }
        }


        public void ClearHand()
        {
            Cards.Clear();
            HandValue = 0;
        }
        public override string ToString()
        {
            string handString = "";
            foreach (Card card in Cards)
            {
                handString += card.ToString() + ", ";
            }
            return handString;
        }

        public bool IsBust()
        {
            if (HandValue > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BlackJack()
        {
            if (HandValue == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
