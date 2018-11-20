using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveLoad;
using UnityEngine;

namespace Items
{
    public class ItemGenerator
    {
        public Armor GenerateArmor(int itemLevel, ItemClass itemClass, ItemType itemType, EqType eqType)
        {
            Armor result = null;
            if (itemType == ItemType.Armor)
            {
                ItemIcon itemIcon = this.GenerateItemIcon(itemClass, itemType, eqType);
                string itemName = this.GenerateItemName(itemClass, eqType);
                int goldValue = this.GenerateItemGoldValue(itemLevel);
                double itemWeight = Math.Round(CryptoRandom.NextDouble(0, 5), 2, MidpointRounding.AwayFromZero);
                ItemFeatures itemFeatures = new ItemFeatures();
                itemFeatures.EnableFeatures(ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsEquipAble, 
                    ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsRepairAble, ItemFeaturesType.IsSellAble, 
                    ItemFeaturesType.IsUpgradeAble);

                Armor armor = new Armor(itemClass, itemType, ItemSubType.None, itemIcon, eqType, itemFeatures,
                   itemName, string.Empty, goldValue, itemWeight, false, false, 100, itemLevel, 0,
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity),
                   this.GenerateItemStatistics(itemLevel, itemIcon.Rarity));

                result = armor;
            }
            else if (itemType == ItemType.Trinket)
            {

            }

            return result;
        }

        private Statistics GenerateItemStatistics(int level, int itemRarity)
        {
            int basicValue = CryptoRandom.Next((int)((10 + itemRarity) * Math.Pow(1.1, level)), (int)((15+itemRarity) * Math.Pow(1.1, level)));
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
                30, //none
                25, //basic
                20, //common
                13, //uncommon
                8, //epic
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
