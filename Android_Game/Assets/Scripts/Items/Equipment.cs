using NPC;
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

        public virtual void PostInstantiate()
        {
            if (this.Helmet != null)
            {
                this.Helmet.PostInstantiate();
            }
            if (this.Body != null)
            {
                this.Body.PostInstantiate();
            }
            if (this.Boots != null)
            {
                this.Boots.PostInstantiate();
            }
            if (this.Weapon != null)
            {
                this.Weapon.PostInstantiate();
            }
            if (this.Gloves != null)
            {
                this.Gloves.PostInstantiate();
            }
            if (this.Shield != null)
            {
                this.Shield.PostInstantiate();
            }
            if (this.Trinket != null)
            {
                this.Trinket.PostInstantiate();
            }
        }

        public void EquipItem(EqType eqItemType, Item item, Champion eqOwner)
        {
            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        this.Body = (Armor)item;
                        this.Body.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Boots:
                    {
                        this.Boots = (Armor)item;
                        this.Boots.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Gloves:
                    {
                        this.Gloves = (Armor)item;
                        this.Gloves.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Helmet:
                    {
                        this.Helmet = (Armor)item;
                        this.Helmet.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Shield:
                    {
                        this.Shield = (Armor)item;
                        this.Shield.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Trinket:
                    {
                        this.Trinket = (Armor)item;
                        this.Trinket.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
                case EqType.Weapon:
                    {
                        this.Weapon = (Weapon)item;
                        this.Weapon.Equip();
                        this.AddModifiersToChampion(item, eqItemType, eqOwner);
                        break;
                    }
            }
        }

        public void UnequipItem(EqType eqItemType, Champion eqOwner)
        {
            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        this.Body.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Body = null;
                        break;
                    }
                case EqType.Boots:
                    {
                        this.Boots.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Boots = null;
                        break;
                    }
                case EqType.Gloves:
                    {
                        this.Gloves.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Gloves = null;
                        break;
                    }
                case EqType.Helmet:
                    {
                        this.Helmet.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Helmet = null;
                        break;
                    }
                case EqType.Shield:
                    {
                        this.Shield.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Shield = null;
                        break;
                    }
                case EqType.Trinket:
                    {
                        this.Trinket.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Trinket = null;
                        break;
                    }
                case EqType.Weapon:
                    {
                        this.Weapon.Unequip();
                        this.RemoveModifiersFromChampion(eqItemType, eqOwner);
                        this.Weapon = null;
                        break;
                    }
            }
        }

        private void AddModifiersToChampion(Item item, EqType eqType, Champion eqOwner)
        {
            if (eqOwner != null)
            {
                if (item is Armor)
                {
                    Armor armor = (Armor)item;

                    eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    eqOwner.Dexterity.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.DexterityBonus.Acctual, eqType));

                    eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    eqOwner.Intelligence.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.IntelligenceBonus.Acctual, eqType));

                    eqOwner.MagicArmor.RemoveAllModifiers(eqType);
                    eqOwner.MagicArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.MagicArmorBonus.Acctual, eqType));

                    eqOwner.MelleArmor.RemoveAllModifiers(eqType);
                    eqOwner.MelleArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.MelleArmorBonus.Acctual, eqType));

                    eqOwner.RangedArmor.RemoveAllModifiers(eqType);
                    eqOwner.RangedArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.RangedArmorBonus.Acctual, eqType));

                    eqOwner.Strength.RemoveAllModifiers(eqType);
                    eqOwner.Strength.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.StrengthBonus.Acctual, eqType));

                    eqOwner.Vitality.RemoveAllModifiers(eqType);
                    eqOwner.Vitality.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, armor.VitalityBonus.Acctual, eqType));
                }
                else if (item is Weapon)
                {
                    Weapon weapon = (Weapon)item;

                    eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    eqOwner.Dexterity.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.DexterityBonus.Acctual, eqType));

                    eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    eqOwner.Intelligence.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.IntelligenceBonus.Acctual, eqType));

                    eqOwner.Strength.RemoveAllModifiers(eqType);
                    eqOwner.Strength.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.StrengthBonus.Acctual, eqType));

                    eqOwner.Vitality.RemoveAllModifiers(eqType);
                    eqOwner.Vitality.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.VitalityBonus.Acctual, eqType));
                }
            }
        }

        private void RemoveModifiersFromChampion(EqType eqType, Champion eqOwner)
        {
            if (eqOwner != null)
            {
                if (eqType != EqType.Weapon && eqType != EqType.None)
                {
                    eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    eqOwner.MagicArmor.RemoveAllModifiers(eqType);
                    eqOwner.MelleArmor.RemoveAllModifiers(eqType);
                    eqOwner.RangedArmor.RemoveAllModifiers(eqType);
                    eqOwner.Strength.RemoveAllModifiers(eqType);
                    eqOwner.Vitality.RemoveAllModifiers(eqType);
                }
                else if (eqType != EqType.None)
                {
                    eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    eqOwner.Strength.RemoveAllModifiers(eqType);
                    eqOwner.Vitality.RemoveAllModifiers(eqType);
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
                        Debug.Log("YEP");
                        return null;
                    }
            }
        }
    }
}
