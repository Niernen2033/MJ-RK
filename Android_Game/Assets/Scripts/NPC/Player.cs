using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

//[System.Serializable]
public class Player : Champion
{
    //List of player backpacki tems
    [XmlElement(Type = typeof(Armor), ElementName = "ArmorBackpackItem")]
    [XmlElement(Type = typeof(Weapon), ElementName = "WeaponBackpackItem")]
    public List<Item> Backpack { get; private set; }

    [XmlElement(Type = typeof(Armor), ElementName = "ArmorEquipmentItem")]
    [XmlElement(Type = typeof(Weapon), ElementName = "WeaponEquipmentItem")]
    public List<Item> Equipment { get; private set; }

    public Player(int vitality, double magicArmor, double rangedArmor, double melleArmor,
        int dexterity, int intelligence, int strength, bool illness)
        : base(vitality, magicArmor, rangedArmor, melleArmor, dexterity, intelligence, strength, illness)
    {
        this.Backpack = new List<Item>();
        this.Equipment = new List<Item>();
    }

    public Player() : base()
    {
        this.Backpack = new List<Item>();
        this.Equipment = new List<Item>();
    }


}
