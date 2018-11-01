using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Xml;
using System.Xml.Schema;

namespace Items
{
    public enum ItemRarity { Basic, Common, Uncommon, Epic, Legendary, None };
    public enum ItemClass { Armor, Weapon, Potion, None };
    public enum ItemType { Magic, Ranged, Melle, None };

    public abstract class Item
    {
        //BASIC VARIABLES ====================================================================================

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
        }

        //BASIC FUNCTIONS ====================================================================================

        protected bool ChangeName(string name)
        {
            try
            {
                this.Name = name;
                return true;
            }
            catch (Exception exc)
            {
                Debug.Log("Class 'Item' in 'ChangeItemName' function:" + exc.ToString());
                return false;
            }
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

