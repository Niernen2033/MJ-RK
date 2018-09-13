using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
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

    public Armor(ItemType itemType, string name, double durability, int goldValue, double weight, bool isUsed, bool isBroken,
        Statistics vitalityBonus, Statistics magicArmourBonus, Statistics rangedArmorBonus, Statistics melleArmorBonus, Statistics dexterityBonus,
        Statistics intelligenceBonus, Statistics strengthBonus)
        : base(ItemClass.Armor, itemType, name, durability, goldValue, weight, isUsed, isBroken)
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
        this.ItemClass = ItemClass.Armor;
        this.VitalityBonus = new Statistics();
        this.MagicArmorBonus = new Statistics();
        this.RangedArmorBonus = new Statistics();
        this.MelleArmorBonus = new Statistics();
        this.DexterityBonus = new Statistics();
        this.IntelligenceBonus = new Statistics();
        this.StrengthBonus = new Statistics();
    }

    public Armor(ref Armor armor)
    {
        this.ItemClass = armor.ItemClass;
        this.ItemType = armor.ItemType;
        this.Name = armor.Name;
        this.Durability = armor.Durability;
        this.GoldValue = armor.GoldValue;
        this.Weight = armor.Weight;
        this.IsUsed = armor.IsUsed;
        this.IsBroken = armor.IsBroken;

        this.VitalityBonus = armor.VitalityBonus;
        this.MagicArmorBonus = armor.MagicArmorBonus;
        this.RangedArmorBonus = armor.RangedArmorBonus;
        this.MelleArmorBonus = armor.MelleArmorBonus;
        this.DexterityBonus = armor.DexterityBonus;
        this.IntelligenceBonus = armor.IntelligenceBonus;
        this.StrengthBonus = armor.StrengthBonus;
    }
}
