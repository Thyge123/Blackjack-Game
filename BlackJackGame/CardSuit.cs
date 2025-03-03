using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class CardSuit
    {
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        public Suit SuitType { get; set; }

        public CardSuit(Suit suit)
        {
            SuitType = suit;
        }

        public CardSuit()
        {
            
        }

        public override string ToString()
        {
            return $"{SuitType}";
        }

    }
}
