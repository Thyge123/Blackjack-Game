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

        // Add a card to the hand
        public void AddCard(Card card)
        {
            ArgumentNullException.ThrowIfNull(card, nameof(card)); // Check for null card         
            Cards.Add(card); // Add the card to the hand
            CalculateHandValue();
        }

        // Clear the hand
        public void ClearHand()
        {
            Cards.Clear();
            HandValue = 0;
        }


        // Calculate the value of the hand
        private void CalculateHandValue()
        {
            HandValue = 0;
            int aceCount = 0;

            // First, count aces and sum non-ace cards
            foreach (Card card in Cards)
            {
                if (card.Value == CardValue.CardsValue.Ace) // Check for ace
                {
                    aceCount++; // Count aces
                }
                else
                {
                    HandValue += card.GetCardValue(); // Sum non-ace cards
                }
            }

            // Add all aces as 11 initially
            HandValue += aceCount * 11;

            // Convert aces from 11 to 1 as needed to avoid bust
            for (int i = 0; i < aceCount && HandValue > 21; i++)
            {
                HandValue -= 10; // Convert one ace from 11 to 1
            }
        }

        public bool IsBust()
        {
            return HandValue > 21;
        }

        public bool BlackJack()
        {
            return HandValue == 21;
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
    }
}