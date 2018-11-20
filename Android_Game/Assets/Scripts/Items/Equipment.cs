using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Items
{
    public class Equipment
    {
        public Armor Helmet { get; set; }
        public Armor Body { get; set; }
        public Armor Boots { get; set; }
        public Weapon Weapon { get; set; }
        public Armor Gloves { get; set; }
        public Armor Shield { get; set; }
        public Armor Trinket { get; set; }

        public Equipment()
        {
            this.Helmet = new Armor();
            this.Body = new Armor();
            this.Boots = new Armor();
            this.Weapon = new Weapon();
            this.Gloves = new Armor();
            this.Shield = new Armor();
            this.Trinket = new Armor();
        }

        public Equipment(Armor helmet, Armor body, Armor boots, Armor gloves, Armor shield, Armor trinket, Weapon weapon)
        {
            this.Helmet = helmet;
            this.Body = body;
            this.Boots = boots;
            this.Weapon = weapon;
            this.Gloves = gloves;
            this.Shield = shield;
            this.Trinket = trinket;
        }

        public Equipment(Equipment equipment)
        {
            this.Helmet = equipment.Helmet;
            this.Body = equipment.Body;
            this.Boots = equipment.Boots;
            this.Weapon = equipment.Weapon;
            this.Gloves = equipment.Gloves;
            this.Shield = equipment.Shield;
            this.Trinket = equipment.Trinket;
        }

        public void EquipItem(EqType eqItemType, Item item)
        {
            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        this.Body = (Armor)item;
                        this.Body.Equip();
                        break;
                    }
                case EqType.Boots:
                    {
                        this.Boots = (Armor)item;
                        this.Boots.Equip();
                        break;
                    }
                case EqType.Gloves:
                    {
                        this.Gloves = (Armor)item;
                        this.Gloves.Equip();
                        break;
                    }
                case EqType.Helmet:
                    {
                        this.Helmet = (Armor)item;
                        this.Helmet.Equip();
                        break;
                    }
                case EqType.Shield:
                    {
                        this.Shield = (Armor)item;
                        this.Shield.Equip();
                        break;
                    }
                case EqType.Trinket:
                    {
                        this.Trinket = (Armor)item;
                        this.Trinket.Equip();
                        break;
                    }
                case EqType.Weapon:
                    {
                        this.Weapon = (Weapon)item;
                        this.Weapon.Equip();
                        break;
                    }
            }
        }

        public void UnequipItem(EqType eqItemType)
        {
            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        this.Body.Unequip();
                        this.Body = null;
                        break;
                    }
                case EqType.Boots:
                    {
                        this.Boots.Unequip();
                        this.Boots = null;
                        break;
                    }
                case EqType.Gloves:
                    {
                        this.Gloves.Unequip();
                        this.Gloves = null;
                        break;
                    }
                case EqType.Helmet:
                    {
                        this.Helmet.Unequip();
                        this.Helmet = null;
                        break;
                    }
                case EqType.Shield:
                    {
                        this.Shield.Unequip();
                        this.Shield = null;
                        break;
                    }
                case EqType.Trinket:
                    {
                        this.Trinket.Unequip();
                        this.Trinket = null;
                        break;
                    }
                case EqType.Weapon:
                    {
                        this.Weapon.Unequip();
                        this.Weapon = null;
                        break;
                    }
            }
        }

        public EquipmentItem GetItemByType(EqType eqItemType)
        {
            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        return this.Body;
                    }
                case EqType.Boots:
                    {
                        return this.Boots;
                    }
                case EqType.Gloves:
                    {
                        return this.Gloves;
                    }
                case EqType.Helmet:
                    {
                        return this.Helmet;
                    }
                case EqType.Shield:
                    {
                        return this.Shield;
                    }
                case EqType.Trinket:
                    {
                        return this.Trinket;
                    }
                case EqType.Weapon:
                    {
                        return this.Weapon;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
