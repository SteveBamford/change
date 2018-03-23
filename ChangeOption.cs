using System;
using System.Collections.Generic;
using System.Text;

namespace Change
{
    class ChangeOption
    {
        public ChangeOption(int targetValue)
        {
            TargetValue = targetValue;
            ChangeCoins = new List<int>();
        }

        public int TargetValue { get; private set; }
        public List<int> ChangeCoins { get; private set; }

        public void AddChangeCoin(int changeCoin)
        {
            ChangeCoins.Add(changeCoin);
        }

        public void SetChangeCoins(IEnumerable<int> changeCoins)
        {
            ChangeCoins.Clear();
            ChangeCoins.AddRange(changeCoins);
        }
    }
}
