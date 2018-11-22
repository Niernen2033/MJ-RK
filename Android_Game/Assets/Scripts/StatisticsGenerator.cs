using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Items;
using NPC;

public enum StatisticsGeneratorBoostType { Increase, Decrease, Normal, Zero, MegaDecrease, MegaIncrease };

public static class StatisticsGenerator
{
    public static Statistics GenerateItemStatistics(int level, ItemRarity itemRarity, StatisticsGeneratorBoostType boost)
    {
        int basicValue = 0;

        switch (boost)
        {
            case StatisticsGeneratorBoostType.Decrease:
                {
                    basicValue = CryptoRandom.Next((int)((6 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((11 + (int)itemRarity) * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Increase:
                {
                    basicValue = CryptoRandom.Next((int)((14 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((19 + (int)itemRarity) * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Normal:
                {
                    basicValue = CryptoRandom.Next((int)((10 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((15 + (int)itemRarity) * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Zero:
                {
                    basicValue = 0;
                    break;
                }
            case StatisticsGeneratorBoostType.MegaDecrease:
                {
                    basicValue = CryptoRandom.Next((int)((2 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((7 + (int)itemRarity) * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.MegaIncrease:
                {
                    basicValue = CryptoRandom.Next((int)((18 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((23 + (int)itemRarity) * Math.Pow(1.1, level)));
                    break;
                }
        }


        Statistics result = new Statistics(basicValue);

        return result;
    }

    public static Statistics GenerateChampionStatistics(int level, ChampionClass championClass, StatisticsGeneratorBoostType boost)
    {
        Statistics result = null;

        int basicValue = 0;

        switch (boost)
        {
            case StatisticsGeneratorBoostType.Decrease:
                {
                    basicValue = CryptoRandom.Next((int)(6 * Math.Pow(1.1, level)), (int)(11 * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Increase:
                {
                    basicValue = CryptoRandom.Next((int)(14* Math.Pow(1.1, level)), (int)(19 * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Normal:
                {
                    basicValue = CryptoRandom.Next((int)(10 * Math.Pow(1.1, level)), (int)(15 * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.Zero:
                {
                    basicValue = 0;
                    break;
                }
            case StatisticsGeneratorBoostType.MegaDecrease:
                {
                    basicValue = CryptoRandom.Next((int)(2 * Math.Pow(1.1, level)), (int)(7 * Math.Pow(1.1, level)));
                    break;
                }
            case StatisticsGeneratorBoostType.MegaIncrease:
                {
                    basicValue = CryptoRandom.Next((int)(18 * Math.Pow(1.1, level)), (int)(23 * Math.Pow(1.1, level)));
                    break;
                }
        }

        result = new Statistics(basicValue);

        return result;
    }
}
