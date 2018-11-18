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

    public class Item
    {
        //BASIC VARIABLES ====================================================================================
        [XmlIgnore]
        public string Hash { get; private set; }

        [XmlIgnore]
        public string Name { get; private set; }

        //Item class
        public ItemClass Class { get; set; }

        //Item type
        public ItemType Type { get; set; }

        //Item name
        public string BasicName { get; set; }

        //Item additional name
        public string AdditionalName { get; set; }

        //Item level
        public int Level { get; set; }

        //Item icon
        public ItemIcon Icon { get; set; }

        //Item basic value
        public int GoldValue { get; set; }

        //Item basic weight
        public double Weight { get; set; }

        //Features
        public ItemFeatures Features { get; set; }


        //BASIC CONSTRUCTORS ====================================================================================
        public Item(ItemClass itemClass, ItemType itemType, ItemIcon icon,
            string basicName, string additionalName, int goldValue, double weight, int level)
        {
            this.Class = itemClass;
            this.Type = itemType;
            this.Icon = icon;
            this.Features = new ItemFeatures();

            this.BasicName = basicName;
            this.AdditionalName = additionalName;
            this.GoldValue = goldValue;
            this.Weight = weight;
            this.Level = level;
            this.Name = this.BasicName + this.AdditionalName;

            this.CalculateHash();
        }

        public Item()
        {
            this.Class = ItemClass.None;
            this.Type = ItemType.None;
            this.Icon = new ItemIcon();
            this.Features = new ItemFeatures();

            this.BasicName = string.Empty;
            this.AdditionalName = string.Empty;
            this.GoldValue = 0;
            this.Weight = 0;
            this.Level = 0;
            this.Name = this.BasicName + this.AdditionalName;

            this.CalculateHash();
        }

        public Item(Item item)
        {
            this.Class = item.Class;
            this.Type = item.Type;
            this.Icon = new ItemIcon(item.Icon);
            this.Features = new ItemFeatures(item.Features);

            this.BasicName = string.Copy(item.BasicName);
            this.AdditionalName = string.Copy(item.AdditionalName);
            this.GoldValue = item.GoldValue;
            this.Weight = item.Weight;
            this.Level = item.Level;
            this.Name = this.BasicName + this.AdditionalName;

            this.CalculateHash();
        }

        //BASIC FUNCTIONS ====================================================================================
        public virtual void PostInstantiate()
        {
            this.Name = this.BasicName + this.AdditionalName;
            this.CalculateHash();
        }

        protected bool ChangeName(string name)
        {
            try
            {
                this.AdditionalName = name;
                this.Name = this.BasicName + this.AdditionalName;
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
                + this.Icon.Rarity.ToString() + DateTime.Now.ToString();

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

