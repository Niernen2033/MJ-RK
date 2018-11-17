using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public Weapon(ItemType itemType, ItemIcon icon,
            string name, int goldValue, double weight, bool isEquiped, bool isBroken, int durability, int level, int upgradeLevel,
            Statistics basicDamage, Statistics vitalityBonus, Statistics dexterityBonus, Statistics intelligenceBonus,
            Statistics strengthBonus) : base(ItemClass.Weapon, itemType, icon, name, goldValue, weight, isEquiped, isBroken, durability, level, upgradeLevel)
        {
            this.BasicDamage = basicDamage;
            this.VitalityBonus = vitalityBonus;
            this.DexterityBonus = dexterityBonus;
            this.IntelligenceBonus = intelligenceBonus;
            this.StrengthBonus = strengthBonus;
        }

        public Weapon() : base()
        {
            this.Class = ItemClass.Weapon;
            this.BasicDamage = new Statistics();
            this.VitalityBonus = new Statistics();
            this.DexterityBonus = new Statistics();
            this.IntelligenceBonus = new Statistics();
            this.StrengthBonus = new Statistics();
        }
    }
}
