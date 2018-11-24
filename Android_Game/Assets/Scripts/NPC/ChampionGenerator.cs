using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Items;

namespace NPC
{
    class ChampionGenerator
    {
        public Champion GenerateChampion(int level, ChampionClass championClass, ChampionType championType)
        {
            Champion result = null;
            Equipment equipment = new Equipment();
            List<Item> items = new List<Item>();

            switch (championClass)
            {
                case ChampionClass.Mage:
                    {
                        result = new Champion(championClass, championType, string.Empty, this.GenerateExperience(level), level,
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //streng
                            false, equipment, items);

                        break;
                    }
                case ChampionClass.Range:
                    {
                        result = new Champion(championClass, championType, string.Empty, this.GenerateExperience(level), level,
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //streng
                            false, equipment, items);

                        break;
                    }
                case ChampionClass.Warrior:
                    {
                        result = new Champion(championClass, championType, string.Empty, this.GenerateExperience(level), level,
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaDecrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(level, championClass, StatisticsGeneratorBoostType.MegaIncrease), //streng
                            false, equipment, items);

                        break;
                    }
            }

            return result;
        }

        public Player GenerateNewPlayer(ChampionClass championClass, string name)
        {
            Player result = null;
            Equipment equipment = new Equipment();
            List<Item> items = new List<Item>();

            switch (championClass)
            {
                case ChampionClass.Mage:
                    {
                        result = new Player(championClass, ChampionType.Normal, name, 0, 1,
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //streng
                            false, equipment, items);

                        break;
                    }
                case ChampionClass.Range:
                    {
                        result = new Player(championClass, ChampionType.Normal, name, 0, 1,
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //streng
                            false, equipment, items);

                        break;
                    }
                case ChampionClass.Warrior:
                    {
                        result = new Player(championClass, ChampionType.Normal, name, 0, 1,
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.Normal),
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //magic 
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //ranged
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //melle
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //dexterity
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaDecrease), //inteli
                            StatisticsGenerator.GenerateChampionStatistics(1, championClass, StatisticsGeneratorBoostType.MegaIncrease), //streng
                            false, equipment, items);

                        break;
                    }
            }

            return result;
        }

        private int GenerateExperience(int level)
        {
            int result = 0;

            result = (int)Math.Pow(1.1, level);

            return result;
        }
    }
}
