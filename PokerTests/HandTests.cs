using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Tests
{
    [TestClass()]
    public class HandTests
    {
        private readonly TestDeck testDeck;

        public HandTests()
        {
            testDeck = new TestDeck();
        }

        [TestInitialize]
        public void InitializeTests()
        {
            testDeck.Init();
        }

        [TestMethod()]
        public void HighCardWins()
        {
            var winningHand = testDeck.GetHighCardHand(14);
            var losingHand = testDeck.GetHighCardHand(7);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void PairWinsHighCard()
        {
            var winningHand = testDeck.GetPairHand(2);
            var losingHand = testDeck.GetHighCardHand(7);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void PairWinsLowerPair()
        {
            var winningHand = testDeck.GetPairHand(3);
            var losingHand = testDeck.GetPairHand(2);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void PairWinsEqualPairWithHighCard()
        {
            var winningHand = testDeck.GetPairHand(pairValue: 3, highCard: 14);
            var losingHand = testDeck.GetPairHand(pairValue: 3, highCard: 12);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void TwoPairsWinsHighCard()
        {
            var winningHand = testDeck.GetTwoPairsHand(pair1Value: 3, pair2Value: 4);
            var losingHand = testDeck.GetHighCardHand(7);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void TwoPairsWinsPair()
        {
            var winningHand = testDeck.GetTwoPairsHand(pair1Value: 3, pair2Value: 4);
            var losingHand = testDeck.GetPairHand(7);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void TwoPairsWinsTwoPairsWithLowerHighPair()
        {
            var winningHand = testDeck.GetTwoPairsHand(pair1Value: 3, pair2Value: 4);
            var losingHand = testDeck.GetTwoPairsHand(pair1Value: 2, pair2Value: 3);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void TwoPairsWinsTwoPairsWithLowerLowPair()
        {
            var winningHand = testDeck.GetTwoPairsHand(pair1Value: 3, pair2Value: 4);
            var losingHand = testDeck.GetTwoPairsHand(pair1Value: 2, pair2Value: 4);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void TwoPairsWinsTwoPairsWithLowerHighCard()
        {
            var winningHand = testDeck.GetTwoPairsHand(pair1Value: 5, pair2Value: 4, highCard: 3);
            var losingHand = testDeck.GetTwoPairsHand(pair1Value: 5, pair2Value: 4, highCard: 2);

            Assert.IsTrue(winningHand > losingHand);
        }

        [TestMethod()]
        public void EqualTwoPairsEndsInDraw()
        {
            var hand1 = testDeck.GetTwoPairsHand(pair1Value: 5, pair2Value: 4, highCard: 3);
            var hand2 = testDeck.GetTwoPairsHand(pair1Value: 5, pair2Value: 4, highCard: 3);

            Assert.IsTrue(hand1 == hand2);
        }
    }
}