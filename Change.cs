using System;
using System.Collections.Generic;
using System.Linq;

namespace Change
{
    public static class Change
    {
        public static int[] Calculate(int target, int[] coins)
        {
            CheckForInvalidArguments(target, coins);

            if (target == 0)
                return new int[0];
            else
                return CalculateMinimumChangeCoinsList(target, coins);
        }

        private static int[] CalculateMinimumChangeCoinsList(int target, int[] coins)
        {
            var possibleChangeOptions = GeneratePossibleChangeOptions(target, coins);          

            if (ExactChangeCanBeGiven(possibleChangeOptions, target))
                return possibleChangeOptions.GetChangeOption(target).ChangeCoins.ToArray();
            else
                throw new ArgumentException($"Cannot give exact change for {target}");
        }

        private static ChangeOptionList GeneratePossibleChangeOptions(int target, int[] coins)
        {
            var possibleChangeOptions = SetUpInitialPossibleChangeOptions();
            foreach (var coin in GetSortedCoins(coins))
            {
                ConsiderPossibleChangeOptionsForCoin(target, possibleChangeOptions, coin);
            }
            return possibleChangeOptions;
        }

        private static ChangeOptionList SetUpInitialPossibleChangeOptions()
        {
            var changeObjectList = new ChangeOptionList();
            changeObjectList.AddChangeOption(0);
            return changeObjectList;
        }

        private static bool ExactChangeCanBeGiven(ChangeOptionList possibleChangeOptions, int target)
        {
            return possibleChangeOptions.ChangeOptionExistsForTargetValue(target);
        }

        private static void ConsiderPossibleChangeOptionsForCoin(int target, ChangeOptionList possibleChangeOptions, int coin)
        {
            for (int subTarget = coin; subTarget <= target; ++subTarget)
            {
                var changeOption = GetPossibleChangeOptionWhenCoinSubtractedFromSubTarget(possibleChangeOptions, coin, subTarget);

                if (ExistingChangeOptionShouldBeUpdated(possibleChangeOptions, subTarget, changeOption))
                {
                    UpdateChangeOption(possibleChangeOptions, subTarget, coin, changeOption);
                }
            }
        }

        private static void UpdateChangeOption(ChangeOptionList possibleChangeOptions, int targetValue, int coin, ChangeOption existingChangeOption)
        {
            var newCoinList = CreateNewCoinList(coin, existingChangeOption);
            if (!possibleChangeOptions.ChangeOptionExistsForTargetValue(targetValue))
                possibleChangeOptions.AddChangeOption(targetValue, newCoinList);
            else
                possibleChangeOptions.GetChangeOption(targetValue).SetChangeCoins(newCoinList);
        }

        private static IEnumerable<int> CreateNewCoinList(int coin, ChangeOption existingChangeOption)
        {
            List<int> newCoinList = new List<int>();
            newCoinList.AddRange(existingChangeOption.ChangeCoins);
            newCoinList.Add(coin);
            return newCoinList;
        }

        private static ChangeOption GetPossibleChangeOptionWhenCoinSubtractedFromSubTarget(ChangeOptionList possibleChangeOptions, int coin, int subtarget)
        {
            int targetAfterCoinStubtracted = subtarget - coin;
            return possibleChangeOptions.GetChangeOption(targetAfterCoinStubtracted);
        }

        private static bool ExistingChangeOptionShouldBeUpdated(ChangeOptionList existingChangeOptions, int subtarget, ChangeOption newChangeOption)
        {
            return newChangeOption != null &&
                (!existingChangeOptions.ChangeOptionExistsForTargetValue(subtarget) || NewChangeOptionIsShorter(existingChangeOptions.GetChangeOption(subtarget), newChangeOption));
        }

        private static bool NewChangeOptionIsShorter(ChangeOption existingChangeOption, ChangeOption changeOption)
        {
            return existingChangeOption.ChangeCoins.Count > changeOption.ChangeCoins.Count + 1;
        }

        private static IEnumerable<int> GetSortedCoins(int[] coins)
        {
            var coinsList = coins.ToList();
            coinsList.Sort();
            return coinsList;
        }

        private static void CheckForInvalidArguments(int target, int[] coins)
        {
            CheckForNegativeTarget(target);
            CheckForTargetTooSmall(target, coins);
        }

        private static void CheckForTargetTooSmall(int target, int[] coins)
        {
            if (target > 0 && target < coins.Min())
                throw new ArgumentException($"Target ({target}) cannot be less than minimum change denomination ({coins.Min()})");
        }

        private static void CheckForNegativeTarget(int target)
        {
            if (target < 0)
                throw new ArgumentException($"Cannot have negative target {target}");
        }

    }
}