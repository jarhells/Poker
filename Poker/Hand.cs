using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Hand : IComparable<Hand>
    {
        private List<Card> cards = new List<Card>(5);

        public Hand(List<Card> cards)
        {
            if (cards.Count != 5)
            {
                throw new ArgumentException("Hand must have exact 5 cards.");
            }

            this.cards = cards;
            cards.Sort();
        }

        public int CompareTo(Hand other)
        {
            int result = 0;

            if (CompareTwoPairs(other, out result) != 0) return result;
            if (ComparePairs(other, out result) != 0) return result;


            return CompareHighs(other);
        }

        private int CompareTwoPairs(Hand other, out int result)
        {
            result = 0;

            var thisPair1 = FindPair(this);
            var thisPair2 = FindPair(this, thisPair1);

            var otherPair1 = FindPair(other);
            var otherPair2 = FindPair(other, otherPair1);

            if (!isTwoPairs(thisPair1, thisPair2) && !isTwoPairs(otherPair1, otherPair2))
            {
                result = 0;
            }
            else if (isTwoPairs(thisPair1, thisPair2) && !isTwoPairs(otherPair1, otherPair2))
            {
                result = -1;
            }
            else if (!isTwoPairs(thisPair1, thisPair2) && isTwoPairs(otherPair1, otherPair2))
            {
                result = 1;
            }
            else
            {
                List<int> thisPairs = new List<int> { thisPair1.Value, thisPair2.Value };
                List<int> otherPairs = new List<int> { otherPair1.Value, otherPair2.Value };

                thisPairs.Sort();
                otherPairs.Sort();

                result = ComparePairValues(thisPairs[1], otherPairs[1]);

                if (result == 0)
                {
                    result = ComparePairValues(thisPairs[0], otherPairs[0]);
                }
            }

            return result;
        }

        private bool isTwoPairs(int? pair1, int? pair2)
        {
            return pair1.HasValue && pair2.HasValue;
        }

        private static int ComparePairs(Hand h1, Hand h2, out int result)
        {
            result = 0;
            var thisPair = FindPair(h1);
            var otherPair = FindPair(h2);

            if (!thisPair.HasValue && !otherPair.HasValue)
            {
                return result = 0;
            }

            if (thisPair.HasValue && !otherPair.HasValue)
            {
                return result = -1;
            }

            if (!thisPair.HasValue && otherPair.HasValue)
            {
                return result = 1;
            }

            return result = ComparePairValues(thisPair.Value, otherPair.Value);
        }

        private static int ComparePairValues(int pair1, int pair2)
        {
            return pair1.CompareTo(pair2) * -1;
        }

        private int ComparePairs(Hand other, out int result)
        {
            return ComparePairs(this, other, out result);
        }

        private static int? FindPair(Hand hand, int? ignoredPair = null)
        {
            for (int i = 1; i < hand.cards.Count; i++)
            {
                if (hand.cards[i].Value == hand.cards[i - 1].Value && hand.cards[i].Value != ignoredPair)
                {
                    return hand.cards[i].Value;
                }
            }

            return null;
        }

        private int CompareHighs(Hand other)
        {
            var thisPair1 = FindPair(this);
            var thisPair2 = FindPair(this, thisPair1);

            var otherPair1 = FindPair(other);
            var otherPair2 = FindPair(other, otherPair1);

            var thisHigh = this.cards.Where(c => 
                (thisPair1.HasValue ? c.Value != thisPair1.Value : true) && (thisPair2.HasValue ? c.Value != thisPair2.Value : true)).First().Value;
            var otherHigh = other.cards.Where(c =>
                (otherPair1.HasValue ? c.Value != otherPair1.Value : true) && (otherPair2.HasValue ? c.Value != otherPair2.Value : true)).First().Value;

            return thisHigh.CompareTo(otherHigh) * -1;
        }

        public static bool operator <(Hand h1, Hand h2)
        {
            return h1.CompareTo(h2) > 0;
        }

        public static bool operator >(Hand h1, Hand h2)
        {
            return h1.CompareTo(h2) < 0;
        }

        public static bool operator ==(Hand h1, Hand h2)
        {
            return h1.CompareTo(h2) == 0;
        }

        public static bool operator !=(Hand h1, Hand h2)
        {
            return h1.CompareTo(h2) != 0;
        }

        public override bool Equals(object obj)
        {
            return this == obj as Hand;
        }

        public override int GetHashCode()
        {
            return this.cards.GetHashCode() * 17;
        }

        public static bool operator <=(Hand left, Hand right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Hand left, Hand right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
