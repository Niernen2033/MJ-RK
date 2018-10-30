﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Xml.Serialization;

namespace Items
{
    public abstract class EquipmentItem : Item
    {
        public event EventHandler<ItemEventArgs> EquipedItem;
        public event EventHandler<ItemEventArgs> UnequipedItem;

        //Establish that champion used this item or not
        public bool IsEquiped { get; set; }

        //Establish that item is broken or not
        public bool IsBroken { get; set; }

        //Durability of item (MAX VALUE: 100 || MIN VALUE: 0)
        public double Durability { get; set; }

        protected EquipmentItem() : base()
        {
            this.IsEquiped = false;
            this.IsBroken = false;
            this.Durability = 100;
        }

        protected EquipmentItem(ItemClass itemClass, ItemType itemType, ItemIcon icon,
            string name, int goldValue, double weight, bool isEquiped, bool isBroken, int durability) :
            base(itemClass, itemType, icon, name, goldValue, weight)
        {
            this.IsEquiped = isEquiped;
            this.IsBroken = isBroken;
            this.Durability = durability;
        }

        //METHODS***********************************************
        protected bool UnequipItem(Item item)
        {
            if (this.IsEquiped == true)
            {
                this.IsEquiped = false;
                this.OnUnequipedItem(new ItemEventArgs(item));
                return true;
            }
            else
            {
                Debug.Log("Class 'EquipableItem' in 'UnequipItem' function: Item is already off");
                return false;
            }
        }

        protected bool EquipItem(Item item)
        {
            if (this.IsEquiped == false)
            {
                this.IsEquiped = true;
                this.OnEquipedItem(new ItemEventArgs(item));
                return true;
            }
            else
            {
                Debug.Log("Class 'EquipableItem' in 'EquipItem' function: Item is already on");
                return false;
            }
        }

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
                    Debug.Log("Class 'EquipableItem' in 'DecreaseDurability' function:" + exc.ToString());
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
                Debug.Log("Class 'EquipableItem' in 'DecreaseDurability' function: Durability is equal to 0");
                return false;
            }
        }

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
                    Debug.Log("Class 'EquipableItem' in 'RepairItem' function:" + exc.ToString());
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
                Debug.Log("Class: 'EquipableItem' in 'RepairItem' function: Wrong durabilityIncreaseCount number or durability is equal to 100");
                return false;
            }
        }

        // EVENTS******************************************************************
        protected virtual void OnEquipedItem(ItemEventArgs e)
        {
            this.EquipedItem?.Invoke(this, e);
        }

        protected virtual void OnUnequipedItem(ItemEventArgs e)
        {
            this.UnequipedItem?.Invoke(this, e);
        }
    }
}