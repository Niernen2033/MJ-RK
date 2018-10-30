using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using Items;

namespace NPC
{
    public class Player : Champion
    {
        //List of player backpacki tems
        [XmlElement(Type = typeof(Armor), ElementName = "ArmorBagpackItem")]
        [XmlElement(Type = typeof(Weapon), ElementName = "WeaponBagpackItem")]
        public List<Item> Backpack { get; set; }

        //public Equipment Equipment { get; set; }
        public int MaxCapacity { get; set; }

        public Player(int vitality, double magicArmor, double rangedArmor, double melleArmor,
            int dexterity, int intelligence, int strength, bool illness)
            : base(vitality, magicArmor, rangedArmor, melleArmor, dexterity, intelligence, strength, illness)
        {
            this.Backpack = new List<Item>();
            //this.Equipment = new Equipment();
            this.MaxCapacity = 100;
        }

        public Player() : base()
        {
            this.Backpack = new List<Item>();
            //this.Equipment = new Equipment();
            this.MaxCapacity = 100;
        }


    }
}
