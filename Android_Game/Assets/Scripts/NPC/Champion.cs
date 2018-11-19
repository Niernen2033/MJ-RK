using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using Items;

public class Champion
{
    //Basic champions vitality
    public Statistics Vitality { get; set; }

    //Basic champions magic defence
    public Statistics MagicArmor { get; set; }

    //Basic champions ranged defence
    public Statistics RangedArmor { get; set; }

    //Basic champions melle defence
    public Statistics MelleArmor { get; set; }

    //Basic champions dexterity
    public Statistics Dexterity { get; set; }

    //Basic champions intelligence
    public Statistics Intelligence { get; set; }

    //Basic champions strength
    public Statistics Strength { get; set; }

    //Describe if chhampion has illness or not
    public bool IsIllness { get; set; }

    //List of player backpacki tems
    [XmlElement(Type = typeof(Armor), ElementName = "ArmorBagpackItem")]
    [XmlElement(Type = typeof(Weapon), ElementName = "WeaponBagpackItem")]
    public List<Item> Bagpack { get; set; }
    public Equipment Equipment { get; set; }


    //CONSTRUCTORS************************************************************

    protected Champion(int vitality, double magicArmor, double rangedArmor, double melleArmor, 
        int dexterity, int intelligence, int strength, bool illness, Equipment equipment, List<Item> bagpack)
    {
        this.Vitality = new Statistics(vitality);
        this.MagicArmor = new Statistics(magicArmor);
        this.RangedArmor = new Statistics(rangedArmor);
        this.MelleArmor = new Statistics(melleArmor);
        this.Dexterity = new Statistics(dexterity);
        this.Intelligence = new Statistics(intelligence);
        this.Strength = new Statistics(strength);
        this.IsIllness = illness;

        this.Equipment = equipment;
        this.Bagpack = new List<Item>(bagpack);
    }

    protected Champion()
    {
        this.Vitality = new Statistics();
        this.MagicArmor = new Statistics();
        this.RangedArmor = new Statistics();
        this.MelleArmor = new Statistics();
        this.Dexterity = new Statistics();
        this.Intelligence = new Statistics();
        this.Strength = new Statistics();
        this.IsIllness = false;

        this.Equipment = new Equipment();
        this.Bagpack = new List<Item>();
    }

    protected Champion(Champion champion)
    {
        this.Vitality = champion.Vitality;
        this.MagicArmor = champion.MagicArmor;
        this.RangedArmor = champion.RangedArmor;
        this.MelleArmor = champion.MelleArmor;
        this.Dexterity = champion.Dexterity;
        this.Intelligence = champion.Intelligence;
        this.Strength = champion.Strength;
        this.IsIllness = champion.IsIllness;

        this.Equipment = new Equipment(champion.Equipment);
        this.Bagpack = new List<Item>(champion.Bagpack);
    }

    //FUNCTIONS**************************************************


}

