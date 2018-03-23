using System;
using System.Collections.Generic;
using System.Text;

namespace Change
{
    class PossibleCoinDenominations
    {
        readonly List<int> coinDenominations;
        public PossibleCoinDenominations(int[] coinValues)
        {
            coinDenominations = OrderCoinDenominationsList(coinValues);
        }

        public IEnumerable<int> CoinDenominations
        {
            get
            {
                return coinDenominations;
            }
        }

        List<int> OrderCoinDenominationsList(int[] coinValues)
        {
            var orderedDenominations = new List<int>();
            foreach (int coinValue in coinValues)
            {
                if (CanCoinValueBeAddedToDenominationsList(orderedDenominations, coinValue))
                    orderedDenominations.Add(coinValue);
            }
            coinDenominations.Sort();
            return coinDenominations;
        }

        private static bool CanCoinValueBeAddedToDenominationsList(List<int> orderedDenominations, int coinValue)
        {
            return coinValue > 0 && !orderedDenominations.Contains(coinValue);
        }
    }
}
