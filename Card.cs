using System;

namespace Poker
{
    public enum Suit
    {
        Spades = 1,
        Hearts = 2,
        Diamonds = 3,
        Clubs = 4
    }

    public class Card : IComparable<Card>
    {
        public Suit Suit { get; set; }

        private int value;

        public int Value 
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value < 2 || value > 14)
                {
                    throw new ArgumentOutOfRangeException("Invalid value. Card value must be between 2 and 14");
                }

                this.value = value;
            }
        }

        public int CompareTo(Card other)
        {
            return this.Value > other.Value ? -1 : this.Value < other.Value ? 1 : 0;
        }
    }
}
