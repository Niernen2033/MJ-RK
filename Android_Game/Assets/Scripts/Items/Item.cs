using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Text;

namespace Items
{
    public enum ItemRarity
    {
        Basic = 809,
        Common = 810,
        Uncommon = 7,
        Epic = 808,
        Legendary = 12,
        None = 439,
    };
    public enum ItemClass { Armor, Weapon, Potion, Food, None };
    public enum ItemType { Magic, Ranged, Melle, None };

    public abstract class Item
    {
        //BASIC VARIABLES ====================================================================================
        [XmlIgnore]
        public string Hash { get; private set; }

        //Item class
        public ItemClass Class { get; set; }

        //Item type
        public ItemType Type { get; set; }

        //Item name
        public string Name { get; set; }

        //Item icon
        public ItemIcon Icon { get; set; }

        //Item basic value
        public int GoldValue { get; set; }

        //Item basic weight
        public double Weight { get; set; }

        //Features
        public ItemFeatures Features { get; set; }

        //BASIC CONSTRUCTORS ====================================================================================
        protected Item(ItemClass itemClass, ItemType itemType, ItemIcon icon,
            string name, int goldValue, double weight)
        {
            this.Class = itemClass;
            this.Type = itemType;
            this.Icon = icon;
            this.Features = new ItemFeatures();

            this.Name = name;
            this.GoldValue = goldValue;
            this.Weight = weight;

            this.CalculateHash();
        }

        protected Item()
        {
            this.Class = ItemClass.None;
            this.Type = ItemType.None;
            this.Icon = new ItemIcon();
            this.Features = new ItemFeatures();

            this.Name = string.Empty;
            this.GoldValue = 0;
            this.Weight = 0;

            this.CalculateHash();
        }

        //BASIC FUNCTIONS ====================================================================================

        protected bool ChangeName(string name)
        {
            try
            {
                this.Name = name;
                this.CalculateHash();
                return true;
            }
            catch (Exception exc)
            {
                Debug.Log("Class 'Item' in 'ChangeItemName' function:" + exc.ToString());
                return false;
            }
        }

        private void CalculateHash()
        {
            string dataInfo = this.Class.ToString() + this.Type.ToString() + this.Name + this.Icon.Index.ToString()
                + this.Icon.Rarity.ToString();

            for (int i = 0; i < this.Features.GetFeatures.Length; i++)
            {
                dataInfo += this.Features.GetFeatures[i].ToString();
            }
            this.Hash = GameGlobals.CalculateIndyvidualHash(dataInfo);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Item item;
            try
            {
                item = (Item)obj;
            }
            catch
            {
                return false;
            }

            if(this.Hash != item.Hash)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override int GetHashCode()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(this.Hash);
            int result = 0;
            for(int i=0; i<bytes.Length; i++)
            {
                result ^= (int)bytes[i];
            }
            return result;
        }
    }

    public class ItemEventArgs : EventArgs
    {
        public Item Item { get; private set; }
        public Type ItemType { get; private set; }

        public ItemEventArgs(Item item)
        {
            this.Item = item;
            if (item is EquipmentItem)
            {
                this.ItemType = typeof(EquipmentItem);
            }
            else
            {
                this.ItemType = typeof(Item);
            }
        }
    }
}

