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

        public Player(ChampionClass championClass, ChampionType championType,
            int experience, int level, Statistics vitality, Statistics magicArmor, Statistics rangedArmor, Statistics melleArmor,
            Statistics dexterity, Statistics intelligence, Statistics strength, bool illness, Equipment equipment, List<Item> bagpack)
            : base(championClass, championType, experience, level, vitality, magicArmor, rangedArmor, melleArmor, dexterity, intelligence, strength, illness, equipment, bagpack)
        {
            this.MaxCapacity = 100;
        }

        public Player() : base()
        {
            this.MaxCapacity = (int)this.Strength.Basic;
        }


    }
}
