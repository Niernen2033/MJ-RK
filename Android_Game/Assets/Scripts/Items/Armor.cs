using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public Armor(ItemType itemType, ItemIcon icon,
            string name, int goldValue, double weight, bool isEquiped, bool isBroken, int durability, 
            Statistics vitalityBonus, Statistics magicArmourBonus, Statistics rangedArmorBonus, Statistics melleArmorBonus,
            Statistics dexterityBonus, Statistics intelligenceBonus, Statistics strengthBonus) : 
            base(ItemClass.Armor, itemType, icon, name, goldValue, weight, isEquiped, isBroken, durability)
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
