using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class CardValue
    {
        public enum CardsValue
        { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

        public CardsValue Value { get; set; }

        public CardValue(CardsValue value)
        {
            Value = value;
        }

        public CardValue()
        {
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}