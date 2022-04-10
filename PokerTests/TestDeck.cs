using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Tests
{
    public class TestDeck : Deck
    {
        public Hand GetHighCardHand(int highCard)
        {
            if (highCard < 7 || highCard > 14)
            {
                throw new ArgumentOutOfRangeException(nameof(highCard), highCard, "High card value must be between 7 and 14");
            }

            var cards = new List<Card>(5);
            cards.AddRange(GetRandomCards(cards, highCard));

            return new Hand(cards);
        }

        public Hand GetPairHand(int pairValue, int? highCard = null)
        {
            var cards = new List<Card>(5)
            {
                GetCardWithValue(pairValue),
                GetCardWithValue(pairValue)
            };

            cards.AddRange(GetRandomCards(cards, highCard));

            return new Hand(cards);
        }

        public Hand GetTwoPairsHand(int pair1Value, int pair2Value, int? highCard = null)
        {
            if (pair1Value == pair2Value)
            {
                throw new ArgumentException("Can not have two pairs that both pair have same value");
            }

            var cards = new List<Card>(5)
            {
                GetCardWithValue(pair1Value),
                GetCardWithValue(pair1Value),
                GetCardWithValue(pair2Value),
                GetCardWithValue(pair2Value)
            };

            cards.AddRange(GetRandomCards(cards, highCard));

            return new Hand(cards);
        }

        public Hand GetThreeOfAKindHand(int value, int? highCard = null)
        {
            var cards = new List<Card>(5)
            {
                GetCardWithValue(value),
                GetCardWithValue(value),
                GetCardWithValue(value)
            };

            cards.AddRange(GetRandomCards(cards, highCard));

            return new Hand(cards);
        }

        private IEnumerable<Card> GetRandomCards(List<Card> existingCards, int? highCard = null)
        {
            var rnd = new Random();

            if (highCard.HasValue)
            {
                existingCards.Add(GetCardWithValue(highCard.Value));
            }

            var randomCards = Enumerable.Range(2, highCard.HasValue ? highCard.Value - 1 : 13)
                .Where(v => !existingCards.Select(c => c.Value).Contains(v))
                .OrderBy(v => rnd.Next())
                .Take(5 - existingCards.Count)
                .Select(v => GetCardWithValue(v));

            return randomCards;
        }

        private Card GetCardWithValue(int value)
        {
            var card = cards.First(c => c.Value == value);

            cards.Remove(card);

            return card;
        }
    }
}
