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
            var possibleChangeOptionsArray = SetUpInitialPossibleChangeOptionsArray(target);

            foreach (var coin in GetSortedCoins(coins))
            {
                ConsiderPossibleChangeOptionsForCoin(target, possibleChangeOptionsArray, coin);
            }

            if (ExactChangeCanBeGiven(possibleChangeOptionsArray, target))
                return possibleChangeOptionsArray[target];
            else
                throw new ArgumentException($"Cannot give exact change for {target}");
        }

        private static bool ExactChangeCanBeGiven(int[][] possibleChangeOptions, int target)
        {
            return possibleChangeOptions[target] != null;
        }

        private static int[][] SetUpInitialPossibleChangeOptionsArray(int target)
        {
            var possibleChangeOptionsArray = new int[target + 1][];
            possibleChangeOptionsArray[0] = new int[0];
            return possibleChangeOptionsArray;
        }

        private static void ConsiderPossibleChangeOptionsForCoin(int target, int[][] possibleChangeOptions, int coin)
        {
            for (int subTarget = coin; subTarget <= target; ++subTarget)
            {
                var changeOption = GetPossibleChangeOptionWhenCoinSubtractedFromSubTarget(possibleChangeOptions, coin, subTarget);

                if (ExistingChangeOptionShouldBeUpdated(possibleChangeOptions, subTarget, changeOption))
                {
                    possibleChangeOptions[subTarget] = AddCoinToChangeOption(coin, changeOption);
                }
            }
        }

        private static int[] GetPossibleChangeOptionWhenCoinSubtractedFromSubTarget(int[][] possibleChangeOptions, int coin, int subtarget)
        {
            int targetAfterCoinStubtracted = subtarget - coin;
            return possibleChangeOptions[targetAfterCoinStubtracted];
        }

        private static int[] AddCoinToChangeOption(int coin, int[] changeOption)
        {
            return new[] { coin }.Concat(changeOption).ToArray();
        }

        private static bool ExistingChangeOptionShouldBeUpdated(int[][] existingChangeOptions, int subtarget, int[] newChangeOption)
        {
            return newChangeOption != null &&
                (existingChangeOptions[subtarget] == null || NewChangeOptionIsShorter(existingChangeOptions[subtarget], newChangeOption));
        }

        private static bool NewChangeOptionIsShorter(int[] existingChangeOption, int[] changeOption)
        {
            return existingChangeOption.Length > changeOption.Length + 1;
        }

        private static int[] GetSortedCoins(int[] coins)
        {
            var coinsList = coins.ToList();
            coinsList.Sort();
            coinsList.Reverse();
            return coinsList.ToArray();
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