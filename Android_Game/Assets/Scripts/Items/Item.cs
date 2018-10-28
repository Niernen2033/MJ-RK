using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Xml;
using System.Xml.Schema;

public enum ItemClass { Armor, Weapon, Potion, Empty };
public enum ItemType { Magic, Ranged, Melle, Empty };

namespace Items
{
    public abstract class Item
    {
        //BASIC VARIABLES ====================================================================================

        public event EventHandler<EventArgs> EquipedItem;
        public event EventHandler<EventArgs> UnequipedItem;

        //Item class
        public ItemClass ItemClass { get; set; }

        //Item type
        public ItemType ItemType { get; set; }

        //Item name
        public string Name { get; set; }

        //Item icon
        public string ItemIconName { get; set; }

        //Establish that champion used this item or not
        public bool IsUsed { get; set; }

        //Establish that item is broken or not
        public bool IsBroken { get; set; }

        //Durability of item (MAX VALUE: 100 || MIN VALUE: 0)
        public double Durability { get; set; }

        //Item basic value
        public int GoldValue { get; set; }

        //Item basic weight
        public double Weight { get; set; }


        //BASIC CONSTRUCTORS ====================================================================================
        protected Item(ItemClass itemClass, ItemType itemType, string itemIconName,
            string name, double durability, int goldValue, double weight, bool isUsed, bool isBroken)
        {
            this.ItemClass = itemClass;
            this.ItemType = itemType;
            this.ItemIconName = itemIconName;

            this.Name = name;
            this.Durability = durability;
            this.GoldValue = goldValue;
            this.IsUsed = isUsed;
            this.IsBroken = isBroken;
            this.Weight = weight;
        }

        protected Item()
        {
            this.ItemClass = ItemClass.Empty;
            this.ItemType = ItemType.Empty;

            this.Name = string.Empty;
            this.Durability = 0;
            this.GoldValue = 0;
            this.IsUsed = false;
            this.IsBroken = false;
            this.Weight = 0;
        }

        //Copy constructor
        protected Item(ref Item championItem)
        {
            this.ItemClass = championItem.ItemClass;
            this.ItemType = championItem.ItemType;

            this.Name = championItem.Name;
            this.Durability = championItem.Durability;
            this.GoldValue = championItem.GoldValue;
            this.IsUsed = championItem.IsUsed;
            this.IsBroken = championItem.IsBroken;
            this.Weight = championItem.Weight;
        }

        //BASIC FUNCTIONS ====================================================================================

        /// <summary>
        /// Function change basic item name to 'name'
        /// </summary>
        /// <param name="name"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool ChangeItemName(string name)
        {
            try
            {
                this.Name = name;
                return true;
            }
            catch (Exception exc)
            {
                Debug.Log("Class 'ChampionItem' in 'ChangeItemName' function:" + exc.ToString());
                return false;
            }
        }

        /// <summary>
        /// Function unequip 'item' from player
        /// </summary>
        /// <returns>TRUE if succeed (item is off now) or FALSE if failed (item is already on)</returns>
        protected bool UnequipItem(ref Item item)
        {
            if (this.IsUsed == true)
            {
                this.IsUsed = false;
                this.OnUnequipedItem(new EventArgs());
                return true;
            }
            else
            {
                Debug.Log("Class 'ChampionItem' in 'UnequipItem' function: Item is already off");
                return false;
            }
        }

        /// <summary>
        /// Function equip 'item' from player
        /// </summary>
        /// <returns>TRUE if succeed (item is on now) or FALSE if failed (item is already off)</returns>
        protected bool EquipItem(ref Item item)
        {
            if (this.IsUsed == false)
            {
                this.IsUsed = true;
                this.OnEquipedItem(new EventArgs());
                return true;
            }
            else
            {
                Debug.Log("Class 'ChampionItem' in 'EquipItem' function: Item is already on");
                return false;
            }
        }

        /// <summary>
        /// Function decrease durability of item by 'durabilityDecreaseCount' (0.2 by default)
        /// </summary>
        /// <param name="durabilityDecreaseCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool DecreaseDurability(double durabilityDecreaseCount = 0.2)
        {
            if ((this.Durability > 0) && (durabilityDecreaseCount >= 0))
            {
                try
                {
                    this.Durability -= durabilityDecreaseCount;
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'ChampionItem' in 'DecreaseDurability' function:" + exc.ToString());
                    return false;
                }

                if (this.Durability <= 0)
                {
                    this.IsBroken = true;
                    this.Durability = 0;
                }

                return true;
            }
            else
            {
                Debug.Log("Class 'ChampionItem' in 'DecreaseDurability' function: Durability is equal to 0");
                return false;
            }
        }

        /// <summary>
        /// Function increase durability of item(repair item) by 'durabilityIncreaseCount'.
        /// </summary>
        /// <param name="durabilityIncreaseCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool RepairItem(double durabilityIncreaseCount)
        {
            if ((this.Durability < 100) && (durabilityIncreaseCount >= 0))
            {
                try
                {
                    this.Durability += durabilityIncreaseCount;
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'ChampionItem' in 'RepairItem' function:" + exc.ToString());
                    return false;
                }

                if (this.Durability > 100)
                    this.Durability = 100;

                if (this.IsBroken)
                    this.IsBroken = false;

                return true;
            }
            else
            {
                Debug.Log("Class: 'ChampionItem' in 'RepairItem' function: Wrong durabilityIncreaseCount number or durability is equal to 100");
                return false;
            }
        }

        // EVENTS******************************************************************
        protected virtual void OnEquipedItem(EventArgs e)
        {
            this.EquipedItem?.Invoke(this, e);
        }

        protected virtual void OnUnequipedItem(EventArgs e)
        {
            this.UnequipedItem?.Invoke(this, e);
        }
    }

    sealed class ItemIcon
    {
        public static class MelleArmor
        {
            public enum Body
            {
                Body1_1 = 44,
                Body1_2 = 43,
                Body1_3 = 44,
                Body2_1 = 56,
                Body2_2 = 55,
                Body2_3 = 54,
                Body3_1 = 62,
                Body3_2 = 61,
                Body3_3 = 60,
            }
            public enum Helmet
            {
                Helmet1 = 53,
                Helmet2 = 59,
                Helmet3 = 41,
                Helmet4 = 47,
            }
            public enum Gloves
            {
                Gloves1 = 46,
                Gloves2 = 52,
                Gloves3 = 58,
                Gloves4 = 40,
            }
            public enum Boots
            {
                Boots1 = 39,
                Boots2 = 45,
                Boots3 = 51,
                Boots4 = 57,
            }
            public enum Weapon
            {
                Sword1 = 103,
                Sword2 = 571,
                Sword3 = 589,
                Sword4 = 608,
                Axe1 = 105,
                Axe2 = 122,
                Axe3 = 563,
                Axe4 = 571,
            }
            public enum SecondHand
            {
                Shield1 = 113,
                Shield2 = 114,
                Shield3 = 119,
                Shield4 = 120,
                Shield5 = 645,
                Shield6 = 637,
            }
        }
        public static class RangedArmor
        {
            public enum Body
            {
                Body1 = 666,
                Body2 = 670,
                Body3 = 443,
                Body4_1 = 23,
                Body4_2 = 24,
            }
            public enum Helmet
            {
                Helmet1 = 33,
                Helmet2 = 34,
                Helmet3 = 35,
                Helmet4 = 37,
                Helmet5 = 41,
                Helmet6 = 442,
            }
            public enum Gloves
            {
                Gloves1 = 46,
                Gloves2 = 441,
                Gloves3 = 669,
            }
            public enum Boots
            {
                Boots1 = 25,
                Boots2 = 19,
                Boots3 = 20,
                Boots4 = 39,
            }
            public enum Weapon
            {
                Bow1 = 614,
                Bow2 = 615,
                Bow3 = 616,
                Bow4 = 617,
                Bow5 = 826,
                Crossbow1 = 619,
                Crossbow2 = 624,
                Crossbow3 = 816,
                Crossbow4 = 817,
            }
        }
        public static class MagicArmor
        {
            public enum Body
            {
                Body1 = 66,
                Body2 = 67,
                Body3 = 68,
                Body4 = 69,
                Body5 = 70,
                Body6 = 71,
                Body7 = 72,
                Body8 = 73,
            }
            public enum Helmet
            {
                Helmet1 = 42,
                Helmet2 = 41,
                Helmet3 = 33,
                Helmet4 = 27,
                Helmet5 = 22,
            }
            public enum Gloves
            {
                Gloves1 = 46,
                Gloves2 = 441,
                Gloves3 = 669,
            }
            public enum Boots
            {
                Boots1 = 25,
                Boots2 = 19,
                Boots3 = 20,
                Boots4 = 39,
            }
            public enum Weapon
            {
                Staff1 = 675,
                Staff2 = 678,
                Staff3 = 680,
                Staff4 = 693,
                Staff5 = 699,
                Staff6 = 701,
                Staff7 = 695,
                Staff8 = 683,
            }
            public enum SecondHand
            {
                Shield1 = 120,
                Shield2 = 702,
                Shield3 = 703,
                Shield4 = 705,
                Book1 = 371,
                Book2 = 413,
                Book3 = 412,
                Book4 = 506,
                Book5 = 505,
            }
        }
        public enum ItemClass
        {
            Basic = 809,
            Common = 810,
            Uncommon = 7,
            Epic = 808,
            Legendary = 12,
        }
        public enum Trinket
        {
            Amulet1 = 455,
            Amulet2 = 456,
            Amulet3 = 457,
            Amulet4 = 449,
            Amulet5 = 450,
            Head1 = 448,
            Head2 = 460,
            Head3 = 414,
        }
        public static class Potions
        {
            public enum Health
            {
                Small = 229,
                Medium = 230,
                Large = 228,
            }
            public static int Empty = 214;
            public enum Mana
            {
                Small = 232,
                Medium = 233,
                Large = 231,
            }
            public enum Armor
            {
                Small = 235,
                Medium = 236,
                Large = 234,
            }
            public enum Strength
            {
                Small = 220,
                Medium = 221,
                Large = 219,
            }
            public enum Dexterity
            {
                Small = 216,
                Medium = 217,
                Large = 215,
            }
            public enum Intelligence
            {
                Small = 211,
                Medium = 212,
                Large = 210,
            }
        }
        public static class Junk
        {
            public enum Gems
            {
                Gem1 = 361,
                Gem2 = 360,
                Gem3 = 403,
            }
            public enum Gold
            {
                Gold1 = 300,
                Gold2 = 301,
                Gold3 = 302,
                Gold4 = 304,
                Gold5 = 305,
            }
            public enum Minerals
            {
                Mineral1 = 379,
                Mineral2 = 380,
                Mineral3 = 381,
                Mineral4 = 382,
                Mineral5 = 383,
                Mineral6 = 384,
                Mineral7 = 385,
                Mineral8 = 386,
                Mineral9 = 387,
                Mineral10 = 388,
            }   
            public enum BodyParts
            {
                BodyPart1 = 422,
                BodyPart2 = 451,
                BodyPart3 = 775,
                BodyPart4 = 776,
                BodyPart5 = 777,
                BodyPart6 = 778,
                BodyPart7 = 779,
                BodyPart8 = 780,
                BodyPart9 = 781,
            }
            public enum Generic
            {
                Generic1 = 272,
                Generic2 = 274,
                Generic3 = 280,
                Generic4 = 283,
                Generic5 = 270,
                Generic6 = 255,
                Generic7 = 278,
                Generic8 = 285,
                Generic9 = 303,
                Generic10 = 312,
                Generic11 = 333,
                Generic12 = 341,
                Generic13 = 355,
            }
        }
        public enum Special
        {
            No_Intem = 439,
            Bagpack1 = 83,
            Bagpack2 = 84,

        }
        public enum Gold
        {
            Small = 366,
            Medium = 365,
            Large = 363,
        }
        public enum Food
        {
            Food1 = 144,
            Food2 = 145,
            Food3 = 146,
            Food4 = 147,
            Food5 = 148,
            Food6 = 149,
            Food7 = 150,
            Food8 = 151,
            Food9 = 152,
            Food10 = 153,
            Food11 = 154,
            Food12 = 155,
            Food13 = 156,
            Food14 = 157,
            Food15 = 158,
        }
    }
}

