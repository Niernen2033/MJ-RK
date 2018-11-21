using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public static class ItemIndexManagement
    {
        public static int GetItemRarityIndex(ItemRarity itemRarity)
        {
            int result = (int)ItemRarity.None;

            if (itemRarity == ItemRarity.None)
            {
                return result;
            }

            switch (itemRarity)
            {
                case ItemRarity.Basic:
                    {
                        result = (int)ItemIndex.ItemClass.Basic;
                        break;
                    }
                case ItemRarity.Common:
                    {
                        result = (int)ItemIndex.ItemClass.Common;
                        break;
                    }
                case ItemRarity.Epic:
                    {
                        result = (int)ItemIndex.ItemClass.Epic;
                        break;
                    }
                case ItemRarity.Legendary:
                    {
                        result = (int)ItemIndex.ItemClass.Legendary;
                        break;
                    }
                case ItemRarity.Uncommon:
                    {
                        result = (int)ItemIndex.ItemClass.Uncommon;
                        break;
                    }
            }

            return result;
        }


        public static ItemRarity GetRarityFromIndex(int index)
        {
            ItemRarity result = ItemRarity.None;

            ItemIndex.ItemClass itemClass = (ItemIndex.ItemClass)index;

            switch(itemClass)
            {
                case ItemIndex.ItemClass.Basic:
                    {
                        result = ItemRarity.Basic;
                        break;
                    }
                case ItemIndex.ItemClass.Common:
                    {
                        result = ItemRarity.Common;
                        break;
                    }
                case ItemIndex.ItemClass.Epic:
                    {
                        result = ItemRarity.Epic;
                        break;
                    }
                case ItemIndex.ItemClass.Legendary:
                    {
                        result = ItemRarity.Legendary;
                        break;
                    }
                case ItemIndex.ItemClass.Uncommon:
                    {
                        result = ItemRarity.Uncommon;
                        break;
                    }
            }

            return result;
        }


        public static Type GetItemTypeBy(ItemClass itemClass = ItemClass.None, ItemType itemType = ItemType.None, EqType eqType = EqType.None, ItemSubType itemSubType = ItemSubType.None)
        {
            Type resultType = null;

            if (itemClass == ItemClass.None)
            {
                return resultType;
            }

            switch (itemClass)
            {
                case ItemClass.Magic:
                    {
                        if (itemType == ItemType.None)
                        {
                            return resultType;
                        }

                        switch (itemType)
                        {
                            case ItemType.Armor:
                                {
                                    if (eqType == EqType.None)
                                    {
                                        return resultType;
                                    }

                                    switch (eqType)
                                    {
                                        case EqType.Body:
                                            {
                                                resultType = typeof(ItemIndex.MagicArmor.Body);
                                                break;
                                            }
                                        case EqType.Boots:
                                            {
                                                resultType = typeof(ItemIndex.MagicArmor.Boots);
                                                break;
                                            }
                                        case EqType.Gloves:
                                            {
                                                resultType = typeof(ItemIndex.MagicArmor.Gloves);
                                                break;
                                            }
                                        case EqType.Helmet:
                                            {
                                                resultType = typeof(ItemIndex.MagicArmor.Helmet);
                                                break;
                                            }
                                        case EqType.Shield:
                                            {
                                                resultType = typeof(ItemIndex.MagicArmor.Shield);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case ItemType.Weapon:
                                {
                                    resultType = typeof(ItemIndex.MagicArmor.Weapon);
                                    break;
                                }
                            case ItemType.Trinket:
                                {
                                    resultType = typeof(ItemIndex.Trinket);
                                    break;
                                }
                        }

                        break;
                    }
                case ItemClass.Melle:
                    {
                        if (itemType == ItemType.None)
                        {
                            return resultType;
                        }

                        switch (itemType)
                        {
                            case ItemType.Armor:
                                {
                                    if (eqType == EqType.None)
                                    {
                                        return resultType;
                                    }

                                    switch (eqType)
                                    {
                                        case EqType.Body:
                                            {
                                                resultType = typeof(ItemIndex.MelleArmor.Body);
                                                break;
                                            }
                                        case EqType.Boots:
                                            {
                                                resultType = typeof(ItemIndex.MelleArmor.Boots);
                                                break;
                                            }
                                        case EqType.Gloves:
                                            {
                                                resultType = typeof(ItemIndex.MelleArmor.Gloves);
                                                break;
                                            }
                                        case EqType.Helmet:
                                            {
                                                resultType = typeof(ItemIndex.MelleArmor.Helmet);
                                                break;
                                            }
                                        case EqType.Shield:
                                            {
                                                resultType = typeof(ItemIndex.MelleArmor.Shield);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case ItemType.Weapon:
                                {
                                    resultType = typeof(ItemIndex.MelleArmor.Weapon);
                                    break;
                                }
                            case ItemType.Trinket:
                                {
                                    resultType = typeof(ItemIndex.Trinket);
                                    break;
                                }
                        }

                        break;
                    }
                case ItemClass.Ranged:
                    {
                        if (itemType == ItemType.None)
                        {
                            return resultType;
                        }

                        switch (itemType)
                        {
                            case ItemType.Armor:
                                {
                                    if (eqType == EqType.None || eqType == EqType.Shield)
                                    {
                                        return resultType;
                                    }

                                    switch (eqType)
                                    {
                                        case EqType.Body:
                                            {
                                                resultType = typeof(ItemIndex.RangedArmor.Body);
                                                break;
                                            }
                                        case EqType.Boots:
                                            {
                                                resultType = typeof(ItemIndex.RangedArmor.Boots);
                                                break;
                                            }
                                        case EqType.Gloves:
                                            {
                                                resultType = typeof(ItemIndex.RangedArmor.Gloves);
                                                break;
                                            }
                                        case EqType.Helmet:
                                            {
                                                resultType = typeof(ItemIndex.RangedArmor.Helmet);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case ItemType.Weapon:
                                {
                                    resultType = typeof(ItemIndex.RangedArmor.Weapon);
                                    break;
                                }
                            case ItemType.Trinket:
                                {
                                    resultType = typeof(ItemIndex.Trinket);
                                    break;
                                }
                        }

                        break;
                    }
                case ItemClass.Normal:
                    {
                        if (itemType == ItemType.None)
                        {
                            return resultType;
                        }

                        switch (itemType)
                        {
                            case ItemType.Food:
                                {
                                    resultType = typeof(ItemIndex.Food);
                                    break;
                                }
                            case ItemType.Junk:
                                {
                                    if (itemSubType == ItemSubType.None)
                                    {
                                        return resultType;
                                    }

                                    switch (itemSubType)
                                    {
                                        case ItemSubType.Junk_BodyParts:
                                            {
                                                resultType = typeof(ItemIndex.Junk.BodyParts);
                                                break;
                                            }
                                        case ItemSubType.Junk_Gems:
                                            {
                                                resultType = typeof(ItemIndex.Junk.Gems);
                                                break;
                                            }
                                        case ItemSubType.Junk_Generic:
                                            {
                                                resultType = typeof(ItemIndex.Junk.Generic);
                                                break;
                                            }
                                        case ItemSubType.Junk_Gold:
                                            {
                                                resultType = typeof(ItemIndex.Junk.Gold);
                                                break;
                                            }
                                        case ItemSubType.Junk_Minerals:
                                            {
                                                resultType = typeof(ItemIndex.Junk.Minerals);
                                                break;
                                            }
                                    }

                                    break;
                                }
                            case ItemType.Potion:
                                {
                                    if (itemSubType == ItemSubType.None)
                                    {
                                        return resultType;
                                    }

                                    switch (itemSubType)
                                    {
                                        case ItemSubType.Potion_Dexterity:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Dexterity);
                                                break;
                                            }
                                        case ItemSubType.Potion_Health:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Health);
                                                break;
                                            }
                                        case ItemSubType.Potion_Intelligence:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Intelligence);
                                                break;
                                            }
                                        case ItemSubType.Potion_Mana:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Mana);
                                                break;
                                            }
                                        case ItemSubType.Potion_Strength:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Strength);
                                                break;
                                            }
                                        case ItemSubType.Potion_Armor:
                                            {
                                                resultType = typeof(ItemIndex.Potions.Armor);
                                                break;
                                            }
                                    }

                                    break;
                                }
                            case ItemType.Gold:
                                {
                                    resultType = typeof(ItemIndex.Gold);
                                    break;
                                }
                        }

                        break;
                    }
            }

            return resultType;
        }
    }
}
