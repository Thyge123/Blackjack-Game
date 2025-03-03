using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlackJackGame.CardSuit;
using static BlackJackGame.CardValue;

namespace BlackJackGame
{
    public class Card
    {
        public Suit Suit { get; set; }
        public CardsValue Value { get; set; }
        public Card(Suit suit, CardsValue value)
        {
            Suit = suit;
            Value = value;
        }

        public Card()
        {
            
        }

        public int GetCardValue()
        {
            switch (Value)
            {
                case CardsValue.Ace:
                    return 11;
                case CardsValue.Two:
                    return 2;
                case CardsValue.Three:
                    return 3;
                case CardsValue.Four:
                    return 4;
                case CardsValue.Five:
                    return 5;
                case CardsValue.Six:
                    return 6;
                case CardsValue.Seven:
                    return 7;
                case CardsValue.Eight:
                    return 8;
                case CardsValue.Nine:
                    return 9;
                case CardsValue.Ten:
                case CardsValue.Jack:
                case CardsValue.Queen:
                case CardsValue.King:
                    return 10;
                default:
                    return 0;
            }
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

    }
}
