using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Items
{
    public class Weapon : EquipmentItem
    {
        //Bonus to basic champions damage (will be add to basic champion statistic)
        public Statistics BasicDamage { get; set; }

        //Bonus to basic champions vitality (will be add to basic champion statistic)
        public Statistics VitalityBonus { get; set; }

        //Bonus to basic champions dexterity  (will be add to basic champion statistic)
        public Statistics DexterityBonus { get; set; }

        //Bonus to basic champions intelligence  (will be add to basic champion statistic)
        public Statistics IntelligenceBonus { get; set; }

        //Bonus to basic champions strength (will be add to basic champion statistic)
        public Statistics StrengthBonus { get; set; }

        public Weapon(ItemClass itemClass, ItemType itemType, ItemSubType itemSubType, ItemIcon icon, EqType eqType, ItemFeatures itemFeatures,
            string basicName, string additionalName, int goldValue, double weight, bool isEquiped, bool isBroken, int durability, int level, int upgradeLevel,
            Statistics basicDamage, Statistics vitalityBonus, Statistics dexterityBonus, Statistics intelligenceBonus,
            Statistics strengthBonus) : base(itemClass, itemType, itemSubType, icon, eqType, itemFeatures, basicName, additionalName, goldValue, weight, isEquiped, isBroken, durability, level, upgradeLevel)
        {
            this.BasicDamage = basicDamage;
            this.VitalityBonus = vitalityBonus;
            this.DexterityBonus = dexterityBonus;
            this.IntelligenceBonus = intelligenceBonus;
            this.StrengthBonus = strengthBonus;
        }

        public Weapon() : base()
        {
            this.BasicDamage = new Statistics();
            this.VitalityBonus = new Statistics();
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

                upgradeValue = (int)(this.BasicDamage.Basic * Math.Pow(1.2, this.UpgradeLevel));
                this.BasicDamage.RemoveAllModifiers(StatisticsModifierClass.LevelUP);
                this.BasicDamage.AddModifier(new StatisticsModifier(StatisticsModifierClass.LevelUP, StatisticsModifierType.AddFlat, upgradeValue));

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

        public override void PostInstantiate()
        {
            base.PostInstantiate();
            this.BasicDamage.PostInstantiate();
            this.VitalityBonus.PostInstantiate();
            this.DexterityBonus.PostInstantiate();
            this.IntelligenceBonus.PostInstantiate();
            this.StrengthBonus.PostInstantiate();
        }

        public override string ToString()
        {
            return this.Class + " : " + this.Name + ";"
                + "GoldValue: " + this.GoldValue + ";"
                + "BasicDamage: " + this.BasicDamage.Acctual + ";"
                + "Durability: " + this.Durability + ";"
                + "VitalityBonus: " + this.VitalityBonus.Acctual + ";"
                + "DexterityBonus: " + this.DexterityBonus.Acctual + ";"
                + "IntelligenceBonus: " + this.IntelligenceBonus.Acctual + ";"
                + "StrengthBonus: " + this.StrengthBonus.Acctual;
        }
    }
}
