using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Xml;
using System.Xml.Schema;

public enum ItemClass { Armor, Weapon, Potion, Empty }
public enum ItemType { Magic, Ranged, Melle, Empty }

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
    protected Item(ItemClass itemClass, ItemType itemType, 
        string name, double durability, int goldValue, double weight, bool isUsed, bool isBroken)
    {
        this.ItemClass = itemClass;
        this.ItemType = itemType;

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
            if (DebugInfo.InGameNamespaceDebugInfo == true)
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
            if (DebugInfo.InGameNamespaceDebugInfo == true)
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
            if (DebugInfo.InGameNamespaceDebugInfo == true)
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
                if (DebugInfo.InGameNamespaceDebugInfo == true)
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
            if (DebugInfo.InGameNamespaceDebugInfo == true)
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
                if (DebugInfo.InGameNamespaceDebugInfo == true)
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
            if (DebugInfo.InGameNamespaceDebugInfo == true)
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

