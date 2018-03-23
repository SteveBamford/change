using System;
using System.Collections.Generic;
using System.Text;

namespace Change
{
    class ChangeCoinSet
    {
        readonly List<int> changeCoinValues;

        public ChangeCoinSet()
        {
            changeCoinValues = new List<int>();
        }

        public int CoinSetSize
        {
            get
            {
                return changeCoinValues.Count;
            }
        }

        public int[] CoinSetValuesAsArray
        {
            get
            {
                return changeCoinValues.ToArray();
            }
        }

        public void AddCoinValue(int coinValue)
        {
            changeCoinValues.Add(coinValue);
        }

    }
}
