using System;
using System.Collections.Generic;
using System.Text;

namespace Change
{
    class ChangeOptionList
    {
        readonly Dictionary<int, ChangeOption> changeOptionDictionary;

        public ChangeOptionList()
        {
            changeOptionDictionary = new Dictionary<int, ChangeOption>();
        }

        public void AddChangeOption(int targetValue)
        {
            AddChangeOption(targetValue, new List<int>());
        }

        public void AddChangeOption(int targetValue, IEnumerable<int> changeCoins)
        {
            var newChangeOption = new ChangeOption(targetValue);
            foreach (var changeCoin in changeCoins)
            {
                newChangeOption.AddChangeCoin(changeCoin);
            }
            changeOptionDictionary.Add(targetValue, newChangeOption);
        }
        public ChangeOption GetChangeOption(int targetValue)
        {
            if (ChangeOptionExistsForTargetValue(targetValue))
                return changeOptionDictionary[targetValue];
            else
                return null;
        }

        public bool ChangeOptionExistsForTargetValue(int targetValue)
        {
            return changeOptionDictionary.ContainsKey(targetValue);
        }
    }
}
