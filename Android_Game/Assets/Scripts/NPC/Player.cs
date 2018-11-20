using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using Items;

namespace NPC
{
    public class Player : Champion
    {
        public int MaxCapacity { get; set; }

        public Player(ChampionClass championClass, int vitality, double magicArmor, double rangedArmor, double melleArmor,
            int dexterity, int intelligence, int strength, bool illness, Equipment equipment, List<Item> bagpack)
            : base(championClass, vitality, magicArmor, rangedArmor, melleArmor, dexterity, intelligence, strength, illness, equipment, bagpack)
        {
            this.MaxCapacity = 100;
        }

        public Player() : base()
        {
            this.MaxCapacity = 100;
        }


    }
}
