using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Items
{
    public class Armor : EquipmentItem
    {
        //Bonus to basic champions vitality (will be add to basic champion statistic)
        public Statistics VitalityBonus { get; set; }

        //Bonus to basic champions maagic defence (will be add to basic champion statistic)
        public Statistics MagicArmorBonus { get; set; }

        //Bonus to basic champions ranged defence (will be add to basic champion statistic)
        public Statistics RangedArmorBonus { get; set; }

        //Bonus to basic champions melle defence (will be add to basic champion statistic)
        public Statistics MelleArmorBonus { get; set; }

        //Bonus to basic champions dexterity  (will be add to basic champion statistic)
        public Statistics DexterityBonus { get; set; }

        //Bonus to basic champions intelligence  (will be add to basic champion statistic)
        public Statistics IntelligenceBonus { get; set; }

        //Bonus to basic champions strength (will be add to basic champion statistic)
        public Statistics StrengthBonus { get; set; }

        public Armor(ItemType itemType, ItemIcon icon, EqType eqType,
            string basicName, string additionalName, int goldValue, double weight, bool isEquiped, bool isBroken, int durability, int level, int upgradeLevel,
            Statistics vitalityBonus, Statistics magicArmourBonus, Statistics rangedArmorBonus, Statistics melleArmorBonus,
            Statistics dexterityBonus, Statistics intelligenceBonus, Statistics strengthBonus) : 
            base(ItemClass.Armor, itemType, icon, eqType, basicName, additionalName, goldValue, weight, isEquiped, isBroken, durability, level, upgradeLevel)
        {
            this.VitalityBonus = vitalityBonus;
            this.MagicArmorBonus = magicArmourBonus;
            this.RangedArmorBonus = rangedArmorBonus;
            this.MelleArmorBonus = melleArmorBonus;
            this.DexterityBonus = dexterityBonus;
            this.IntelligenceBonus = intelligenceBonus;
            this.StrengthBonus = strengthBonus;
        }

        public Armor() : base()
        {
            this.Class = ItemClass.Armor;
            this.VitalityBonus = new Statistics();
            this.MagicArmorBonus = new Statistics();
            this.RangedArmorBonus = new Statistics();
            this.MelleArmorBonus = new Statistics();
            this.DexterityBonus = new Statistics();
            this.IntelligenceBonus = new Statistics();
            this.StrengthBonus = new Statistics();
        }

        public void LevelUp()
        {
            int upgradeValue = 0;
            if (this.UpgradeLevel < 5)
            {
                this.UpgradeLevel++;

                upgradeValue = (int)(this.VitalityBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.VitalityBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.VitalityBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.MagicArmorBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.MagicArmorBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.MagicArmorBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.RangedArmorBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.RangedArmorBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.RangedArmorBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.MelleArmorBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.MelleArmorBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.MelleArmorBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.DexterityBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.DexterityBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.DexterityBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.IntelligenceBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.IntelligenceBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.IntelligenceBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                upgradeValue = (int)(this.StrengthBonus.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.StrengthBonus.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.StrengthBonus.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

                this.ChangeName("(+" + this.UpgradeLevel.ToString() + ")");
            }
        }

        public override string ToString()
        {
            return this.Class + " : " + this.Name + ";" 
                + "Durability: " + this.Durability + ";"
                + "VitalityBonus: " + this.VitalityBonus.Acctual + ";" 
                + "MagicArmorBonus: " + this.MagicArmorBonus.Acctual + ";" 
                + "RangedArmorBonus: " + this.RangedArmorBonus.Acctual + ";" 
                + "MelleArmorBonus: " + this.MelleArmorBonus.Acctual + ";"
                + "DexterityBonus: " + this.DexterityBonus.Acctual + ";" 
                + "IntelligenceBonus: " + this.IntelligenceBonus.Acctual + ";" 
                + "StrengthBonus: " + this.StrengthBonus.Acctual;
        }
    }
}
