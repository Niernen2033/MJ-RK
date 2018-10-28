using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Weapon : Item
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

        public Weapon(ItemType itemType, string itemIconName, string name, double durability, int goldValue, double weight, bool isUsed, bool isBroken,
            Statistics basicDamage, Statistics vitalityBonus, Statistics dexterityBonus, Statistics intelligenceBonus,
            Statistics strengthBonus) : base(ItemClass.Weapon, itemType, itemIconName, name, durability, goldValue, weight, isUsed, isBroken)
        {
            this.BasicDamage = basicDamage;
            this.VitalityBonus = vitalityBonus;
            this.DexterityBonus = dexterityBonus;
            this.IntelligenceBonus = intelligenceBonus;
            this.StrengthBonus = strengthBonus;
        }

        public Weapon() : base()
        {
            this.ItemClass = ItemClass.Weapon;
            this.BasicDamage = new Statistics();
            this.VitalityBonus = new Statistics();
            this.DexterityBonus = new Statistics();
            this.IntelligenceBonus = new Statistics();
            this.StrengthBonus = new Statistics();
        }

        public Weapon(ref Weapon weapon)
        {
            this.ItemClass = weapon.ItemClass;
            this.ItemType = weapon.ItemType;
            this.Name = weapon.Name;
            this.Durability = weapon.Durability;
            this.GoldValue = weapon.GoldValue;
            this.Weight = weapon.Weight;
            this.IsUsed = weapon.IsUsed;
            this.IsBroken = weapon.IsBroken;

            this.BasicDamage = weapon.BasicDamage;
            this.VitalityBonus = weapon.VitalityBonus;
            this.DexterityBonus = weapon.DexterityBonus;
            this.IntelligenceBonus = weapon.IntelligenceBonus;
            this.StrengthBonus = weapon.StrengthBonus;
        }
    }
}
