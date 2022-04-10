using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Deck
    {
        readonly protected List<Card> cards;

        public Deck()
        {
            cards = new List<Card>(52);
        }

        public void Init()
        {
            cards.Clear();

            foreach (var suit in (Poker.Suit[])Enum.GetValues(typeof(Poker.Suit)))
            {
                cards.AddRange(Enumerable.Range(2, 13).Select(value => new Card { Suit = suit, Value = value }));
            }
        }
    }
}
