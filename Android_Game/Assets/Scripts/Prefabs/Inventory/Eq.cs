using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

namespace Prefabs.Inventory
{

    public class Eq : MonoBehaviour
    {

        public BagpackSlot HelmetSlot;
        public BagpackSlot BodySlot;
        public BagpackSlot WeaponSlot;
        public BagpackSlot ShieldSlot;
        public BagpackSlot TrinketSlot;
        public BagpackSlot GlovesSlot;
        public BagpackSlot BootsSlot;
        public Image itemInfoPanel;

        private ItemFeaturesType[] eqFeatures;
        private Champion eqOwner;

        private void Awake()
        {
            this.ClearData();
            this.eqFeatures = new ItemFeaturesType[2] { ItemFeaturesType.IsEquipAble, ItemFeaturesType.IsInfoAble };
        }

        private void ClearData()
        {
            this.eqOwner = null;
        }

        private void AddModifiersToChampion(EquipmentItem item, EqType eqType)
        {
            if(this.eqOwner != null)
            {
                if (item is Armor)
                {
                    Armor armor = (Armor)item;

                    this.eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    this.eqOwner.Dexterity.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.DexterityBonus.Acctual, eqType));

                    this.eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    this.eqOwner.Intelligence.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.IntelligenceBonus.Acctual, eqType));

                    this.eqOwner.MagicArmor.RemoveAllModifiers(eqType);
                    this.eqOwner.MagicArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.MagicArmorBonus.Acctual, eqType));

                    this.eqOwner.MelleArmor.RemoveAllModifiers(eqType);
                    this.eqOwner.MelleArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.MelleArmorBonus.Acctual, eqType));

                    this.eqOwner.RangedArmor.RemoveAllModifiers(eqType);
                    this.eqOwner.RangedArmor.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.RangedArmorBonus.Acctual, eqType));

                    this.eqOwner.Strength.RemoveAllModifiers(eqType);
                    this.eqOwner.Strength.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.StrengthBonus.Acctual, eqType));

                    this.eqOwner.Vitality.RemoveAllModifiers(eqType);
                    this.eqOwner.Vitality.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus, 
                        StatisticsModifierType.AddFlat, armor.VitalityBonus.Acctual, eqType));
                }
                else if (item is Weapon)
                {
                    Weapon weapon = (Weapon)item;

                    this.eqOwner.Dexterity.RemoveAllModifiers(eqType);
                    this.eqOwner.Dexterity.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.DexterityBonus.Acctual, eqType));

                    this.eqOwner.Intelligence.RemoveAllModifiers(eqType);
                    this.eqOwner.Intelligence.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.IntelligenceBonus.Acctual, eqType));

                    this.eqOwner.Strength.RemoveAllModifiers(eqType);
                    this.eqOwner.Strength.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.StrengthBonus.Acctual, eqType));

                    this.eqOwner.Vitality.RemoveAllModifiers(eqType);
                    this.eqOwner.Vitality.AddModifier(new StatisticsModifier(StatisticsModifierClass.ItemBonus,
                        StatisticsModifierType.AddFlat, weapon.VitalityBonus.Acctual, eqType));
                }
            }
        }

        public void SetChampion(Champion champion)
        {
            this.eqOwner = champion;
        }

        public void AddToEQ(EquipmentItem item, EqType eqType, out EquipmentItem returnItem)
        {
            if (item != null)
            {
                EquipmentItem result = null;
                switch (eqType)
                {
                    case EqType.Body:
                        {
                            if (this.BodySlot.IsEmpty)
                            {
                                this.BodySlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.BodySlot.item;
                                result.Unequip();
                                this.BodySlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Boots:
                        {
                            if (this.BootsSlot.IsEmpty)
                            {
                                this.BootsSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.BootsSlot.item;
                                result.Unequip();
                                this.BootsSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Gloves:
                        {
                            if (this.GlovesSlot.IsEmpty)
                            {
                                this.GlovesSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.GlovesSlot.item;
                                result.Unequip();
                                this.GlovesSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Helmet:
                        {
                            if (this.HelmetSlot.IsEmpty)
                            {
                                this.HelmetSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.HelmetSlot.item;
                                result.Unequip();
                                this.HelmetSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Shield:
                        {
                            if (this.ShieldSlot.IsEmpty)
                            {
                                this.ShieldSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.ShieldSlot.item;
                                result.Unequip();
                                this.ShieldSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Trinket:
                        {
                            if (this.TrinketSlot.IsEmpty)
                            {
                                this.TrinketSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.TrinketSlot.item;
                                result.Unequip();
                                this.TrinketSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                    case EqType.Weapon:
                        {
                            if (this.WeaponSlot.IsEmpty)
                            {
                                this.WeaponSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            else
                            {
                                //swap
                                result = (EquipmentItem)this.WeaponSlot.item;
                                result.Unequip();
                                this.WeaponSlot.AddItem(item, this.eqFeatures);
                                item.Equip();
                            }
                            this.AddModifiersToChampion(item, eqType);
                            break;
                        }
                }
                returnItem = result;
            }
            else
            {
                returnItem = null;
            }
        }
    }
}
