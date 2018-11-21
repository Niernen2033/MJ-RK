using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveLoad;
using UnityEngine;

namespace Items
{
    public enum ItemGeneratorBoostType { Increase, Decrease, Normal, Zero, MegaDecrease, MegaIncrease };

    public class ItemGenerator
    {
        public Armor GenerateArmor(int itemLevel, ItemClass itemClass, ItemType itemType, EqType eqType)
        {
            Armor result = null;

            if(itemClass == ItemClass.Ranged && eqType == EqType.Shield)
            {
                return result;
            }

            ItemIcon itemIcon = this.GenerateItemIcon(itemClass, itemType, eqType);
            string itemName = this.GenerateItemName(itemClass, eqType);
            int goldValue = this.GenerateItemGoldValue(itemLevel);
            double itemWeight;
            ItemFeatures itemFeatures = new ItemFeatures();
            itemFeatures.EnableFeatures(ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsEquipAble,
                ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsRepairAble, ItemFeaturesType.IsSellAble,
                ItemFeaturesType.IsUpgradeAble);

            if (itemType == ItemType.Armor)
            {
                itemWeight = Math.Round(CryptoRandom.NextDouble(0, 5), 2, MidpointRounding.AwayFromZero);

                Armor armor = null;

                switch (itemClass)
                {
                    case ItemClass.Magic:
                        {
                            armor = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                            break;
                        }
                    case ItemClass.Melle:
                        {
                            armor = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease));
                            break;
                        }
                    case ItemClass.Ranged:
                        {
                            armor = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                            break;
                        }
                }

                result = armor;
            }
            else if (itemType == ItemType.Trinket)
            {
                itemWeight = Math.Round(CryptoRandom.NextDouble(0, 2), 2, MidpointRounding.AwayFromZero);

                Armor trinket = null;

                switch (itemClass)
                {
                    case ItemClass.Magic:
                        {
                            trinket = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                            break;
                        }
                    case ItemClass.Melle:
                        {
                            trinket = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                            break;
                        }
                    case ItemClass.Ranged:
                        {
                            trinket = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                               itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Decrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaIncrease),
                               this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                            break;
                        }
                }

                result = trinket;
            }

            return result;
        }

        public Weapon GenerateWeapon(int itemLevel, ItemClass itemClass)
        {
            Weapon result = null;

            ItemIcon itemIcon = this.GenerateItemIcon(itemClass, ItemType.Weapon);
            string itemName = this.GenerateItemName(itemClass, EqType.Weapon);
            int goldValue = this.GenerateItemGoldValue(itemLevel);
            double itemWeight = Math.Round(CryptoRandom.NextDouble(0, 5), 2, MidpointRounding.AwayFromZero);
            ItemFeatures itemFeatures = new ItemFeatures();
            itemFeatures.EnableFeatures(ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsEquipAble,
                ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsRepairAble, ItemFeaturesType.IsSellAble,
                ItemFeaturesType.IsUpgradeAble);

            switch (itemClass)
            {
                case ItemClass.Magic:
                    {
                        result = new Weapon(itemClass, ItemType.Weapon, ItemSubType.None, itemIcon, EqType.Weapon,
                            itemFeatures, itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                        break;
                    }
                case ItemClass.Melle:
                    {
                        result = new Weapon(itemClass, ItemType.Weapon, ItemSubType.None, itemIcon, EqType.Weapon,
                            itemFeatures, itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase));

                        break;
                    }
                case ItemClass.Ranged:
                    {
                        result = new Weapon(itemClass, ItemType.Weapon, ItemSubType.None, itemIcon, EqType.Weapon,
                            itemFeatures, itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Normal),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.Increase),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease),
                            this.GenerateItemStatistics(itemLevel, ItemIndexManagement.GetRarityFromIndex(itemIcon.Rarity), ItemGeneratorBoostType.MegaDecrease));
                        break;
                    }
            }

            return result;
        }

        public ConsumeableItem GenerateConsumeableItem(ItemType itemType, ItemSubType itemSubType)
        {
            ConsumeableItem result = null;

            int goldValue;
            int boost;
            ItemIcon itemIcon = this.GenerateItemIcon(ItemClass.Normal, itemType, EqType.None, itemSubType);
            itemIcon.Rarity = (int)ItemRarity.None;
            ItemFeatures itemFeatures = new ItemFeatures();
            itemFeatures.EnableFeatures(ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsSellAble, ItemFeaturesType.IsEatAble);

            switch(itemType)
            {
                case ItemType.Potion:
                    {
                        switch (itemSubType)
                        {
                            case ItemSubType.Potion_Armor:
                                {
                                    goldValue = CryptoRandom.Next(200,500);
                                    boost = CryptoRandom.Next(8, 15);
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Armor Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            case ItemSubType.Potion_Dexterity:
                                {
                                    goldValue = CryptoRandom.Next(200, 500);
                                    boost = CryptoRandom.Next(8, 15);
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Dexterity Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            case ItemSubType.Potion_Health:
                                {
                                    goldValue = 50;
                                    boost = 60;
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Health Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            case ItemSubType.Potion_Intelligence:
                                {
                                    goldValue = CryptoRandom.Next(200, 500);
                                    boost = CryptoRandom.Next(8, 15);
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Intelligence Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            case ItemSubType.Potion_Mana:
                                {
                                    goldValue = 50;
                                    boost = 60;
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Mana Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            case ItemSubType.Potion_Strength:
                                {
                                    goldValue = CryptoRandom.Next(200, 500);
                                    boost = CryptoRandom.Next(8, 15);
                                    result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                                        itemFeatures, "Armor Potion", string.Empty, goldValue, 0.5);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case ItemType.Food:
                    {
                        boost = CryptoRandom.Next(20, 50);
                        goldValue = CryptoRandom.Next(20, 60);
                        result = new ConsumeableItem(boost, ItemClass.Normal, itemType, itemSubType, itemIcon,
                            itemFeatures, "Food" +
                            "", string.Empty, goldValue, CryptoRandom.NextDouble(0.3, 1));
                        break;
                    }
            }

            return result;
        }

        public Item GenerateGoldByValue(int goldValue)
        {
            ItemIcon itemIcon = new ItemIcon();
            itemIcon.Index = (int)ItemIndex.Gold.Large;
            itemIcon.Rarity = (int)ItemRarity.None;
            ItemFeatures itemFeatures = new ItemFeatures();
            Item goldPrefab = new Item(ItemClass.Normal, ItemType.Gold, ItemSubType.None, itemIcon, itemFeatures,
                "Gold", string.Empty, goldValue, 0, 0);
            return goldPrefab;
        }

        public Item GenerateGoldByLevel(int level)
        {
            Item result = this.GenerateGoldByValue(CryptoRandom.Next((int)Math.Pow(1.1,level)*50, (int)Math.Pow(1.1, level) * 80));
            return result;
        }

        public Item GenerateJunk(ItemSubType itemSubType)
        {
            Item result = null;

            int goldValue;
            ItemIcon itemIcon = this.GenerateItemIcon(ItemClass.Normal, ItemType.Junk, EqType.None, itemSubType);
            itemIcon.Rarity = (int)ItemRarity.None;

            ItemFeatures itemFeatures = new ItemFeatures();
            itemFeatures.EnableFeatures(ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsSellAble);

            switch (itemSubType)
            {
                case ItemSubType.Junk_BodyParts:
                    {
                        goldValue = CryptoRandom.Next(20, 50);
                        result = new Item(ItemClass.Normal, ItemType.Junk, itemSubType, itemIcon, itemFeatures,
                            "Body part", string.Empty, goldValue, 0.5, 0);
                        break;
                    }
                case ItemSubType.Junk_Gems:
                    {
                        goldValue = CryptoRandom.Next(100, 500);
                        result = new Item(ItemClass.Normal, ItemType.Junk, itemSubType, itemIcon, itemFeatures,
                            "Gem", string.Empty, goldValue, 0.5, 0);
                        break;
                    }
                case ItemSubType.Junk_Generic:
                    {
                        goldValue = CryptoRandom.Next(20, 60);
                        result = new Item(ItemClass.Normal, ItemType.Junk, itemSubType, itemIcon, itemFeatures,
                            "Strange thing", string.Empty, goldValue, 0.5, 0);
                        break;
                    }
                case ItemSubType.Junk_Gold:
                    {
                        goldValue = CryptoRandom.Next(100, 300);
                        result = new Item(ItemClass.Normal, ItemType.Junk, itemSubType, itemIcon, itemFeatures,
                            "Golden item", string.Empty, goldValue, 0.5, 0);
                        break;
                    }
                case ItemSubType.Junk_Minerals:
                    {
                        goldValue = CryptoRandom.Next(50, 200);
                        result = new Item(ItemClass.Normal, ItemType.Junk, itemSubType, itemIcon, itemFeatures,
                            "Mineral", string.Empty, goldValue, 0.5, 0);
                        break;
                    }
            }

            return result;
        }

        private Statistics GenerateItemStatistics(int level, ItemRarity itemRarity, ItemGeneratorBoostType boost)
        {
            int basicValue = 0;

            switch(boost)
            {
                case ItemGeneratorBoostType.Decrease:
                    {
                        basicValue = CryptoRandom.Next((int)((6 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((11 + (int)itemRarity) * Math.Pow(1.1, level)));
                        break;
                    }
                case ItemGeneratorBoostType.Increase:
                    {
                        basicValue = CryptoRandom.Next((int)((14 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((19 + (int)itemRarity) * Math.Pow(1.1, level)));
                        break;
                    }
                case ItemGeneratorBoostType.Normal:
                    {
                        basicValue = CryptoRandom.Next((int)((10 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((15 + (int)itemRarity) * Math.Pow(1.1, level)));
                        break;
                    }
                case ItemGeneratorBoostType.Zero:
                    {
                        basicValue = 0;
                        break;
                    }
                case ItemGeneratorBoostType.MegaDecrease:
                    {
                        basicValue = CryptoRandom.Next((int)((2 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((7 + (int)itemRarity) * Math.Pow(1.1, level)));
                        break;
                    }
                case ItemGeneratorBoostType.MegaIncrease:
                    {
                        basicValue = CryptoRandom.Next((int)((18 + (int)itemRarity) * Math.Pow(1.1, level)), (int)((23 + (int)itemRarity) * Math.Pow(1.1, level)));
                        break;
                    }
            }

            
            Statistics result = new Statistics(basicValue);

            return result;
        }

        private int GenerateItemGoldValue(int level)
        {
            int result = 0;

            result = CryptoRandom.Next((int)(20 * Math.Pow(1.1, level)), (int)(25 * Math.Pow(1.1, level)));

            return result;
        }

        private ItemIcon GenerateItemIcon(ItemClass itemClass, ItemType itemType, EqType eqType = EqType.None, ItemSubType itemSubType = ItemSubType.None)
        {
            ItemIcon result = null;
            int iconIndex_index = CryptoRandom.Next(0, Enum.GetValues(ItemIndexManagement.GetItemTypeBy(itemClass, itemType, eqType, itemSubType)).Length - 1);
            int iconIndex = (int)Enum.GetValues(ItemIndexManagement.GetItemTypeBy(itemClass, itemType, eqType, itemSubType)).GetValue(iconIndex_index);

            result = new ItemIcon(iconIndex, this.GenerateRarityIndex());

            return result;
        }

        private string GenerateItemName(ItemClass itemClass, EqType eqType)
        {
            string result = string.Empty;
            ItemNameData itemNameData = new ItemNameData();
            if(!XmlManager.Load<ItemNameData>(SaveInfo.Paths.Resources.ItemNameData.ItemNames, out itemNameData))
            {
                return result;
            }

            if(eqType == EqType.None)
            {
                return result;
            }

            switch(eqType)
            {
                case EqType.Body:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.ArmorBodyItemNames.Count - 1);
                        result = itemNameData.ArmorBodyItemNames[itemIndex];
                        break;
                    }
                case EqType.Boots:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.ArmorBootsItemNames.Count - 1);
                        result = itemNameData.ArmorBootsItemNames[itemIndex];
                        break;
                    }
                case EqType.Gloves:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.ArmorGlovesItemNames.Count - 1);
                        result = itemNameData.ArmorGlovesItemNames[itemIndex];
                        break;
                    }
                case EqType.Helmet:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.ArmorHelmetItemNames.Count - 1);
                        result = itemNameData.ArmorHelmetItemNames[itemIndex];
                        break;
                    }
                case EqType.Shield:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.ArmorShieldItemNames.Count - 1);
                        result = itemNameData.ArmorShieldItemNames[itemIndex];
                        break;
                    }
                case EqType.Trinket:
                    {
                        int itemIndex = CryptoRandom.Next(0, itemNameData.TrinketItemNames.Count - 1);
                        result = itemNameData.TrinketItemNames[itemIndex];
                        break;
                    }
                case EqType.Weapon:
                    {
                        if(itemClass == ItemClass.None)
                        {
                            return result;
                        }

                        switch(itemClass)
                        {
                            case ItemClass.Magic:
                                {
                                    int itemIndex = CryptoRandom.Next(0, itemNameData.WeaponMagicItemNames.Count - 1);
                                    result = itemNameData.WeaponMagicItemNames[itemIndex];
                                    break;
                                }
                            case ItemClass.Ranged:
                                {
                                    int itemIndex = CryptoRandom.Next(0, itemNameData.WeaponRangedItemNames.Count - 1);
                                    result = itemNameData.WeaponRangedItemNames[itemIndex];
                                    break;
                                }
                            case ItemClass.Melle:
                                {
                                    int itemIndex = CryptoRandom.Next(0, itemNameData.WeaponMelleItemNames.Count - 1);
                                    result = itemNameData.WeaponMelleItemNames[itemIndex];
                                    break;
                                }
                        }

                        break;
                    }
            }

            return result;
        }

        private int GenerateRarityIndex()
        {
            //must sum to 100
            int[] itemRarityChance = new int[6]
            {
                50, //none
                20, //basic
                12, //common
                8, //uncommon
                6, //epic
                4, //legendary
            };

            int result = -1;
            int rarityNumber = CryptoRandom.Next(0, 100);
            int rarityCounter = 0;
            for (int i = 0; i < itemRarityChance.Length; i++)
            {
                rarityCounter += itemRarityChance[i];
                if (rarityNumber <= rarityCounter)
                {
                    switch(i)
                    {
                        case 0:
                            {
                                result = (int)ItemRarity.None;
                                break;
                            }
                        case 1:
                            {
                                result = (int)ItemIndex.ItemClass.Basic;
                                break;
                            }
                        case 2:
                            {
                                result = (int)ItemIndex.ItemClass.Common;
                                break;
                            }
                        case 3:
                            {
                                result = (int)ItemIndex.ItemClass.Uncommon;
                                break;                         
                            }
                        case 4:
                            {
                                result = (int)ItemIndex.ItemClass.Epic;
                                break;
                            }
                        case 5:
                            {
                                result = (int)ItemIndex.ItemClass.Legendary;
                                break;
                            }
                    }
                    break;
                }
            }

            return result;
        }
    }
}
