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
            try
            {
                if (card == null)
                {
                    return; // Do not add null cards
                }
                Cards.Add(card);
                CalculateHandValue();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error adding card to hand: " + e.Message);
            }
        }


        private void CalculateHandValue()
        {
            try
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
                // Add all aces as 11 initially
                HandValue += aceCount * 11;
                // Convert aces from 11 to 1 as needed to avoid bust
                for (int i = 0; i < aceCount; i++)
                {
                    if (HandValue > 21)
                    {
                        HandValue -= 10; // Convert one ace from 11 to 1
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error calculating hand value: " + e.Message);
            }
        }

        public void ClearHand()
        {
            try
            {
                Cards.Clear();
                HandValue = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error clearing hand: " + e.Message);
            }
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
            try
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
            catch (Exception e)
            {
                Console.WriteLine("Error checking if hand is bust: " + e.Message);
                return false;
            }
        }

        public bool BlackJack()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("Error checking if hand is blackjack: " + e.Message);
                return false;
            }
        }
    }
}