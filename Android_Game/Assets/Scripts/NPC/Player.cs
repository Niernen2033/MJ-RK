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

        [XmlElement(Type = typeof(Champion), ElementName = "PlayerTeam")]
        public Champion[] Team { get; set; }

        public Player(ChampionClass championClass, ChampionType championType, string name,
            int experience, int level, Statistics vitality, Statistics magicArmor, Statistics rangedArmor, Statistics melleArmor,
            Statistics dexterity, Statistics intelligence, Statistics strength, bool illness, Equipment equipment, List<Item> bagpack)
            : base(championClass, championType, name, experience, level, vitality, magicArmor, rangedArmor, melleArmor, dexterity, intelligence, strength, illness, equipment, bagpack)
        {
            this.MaxCapacity = 100;
            this.Team = new Champion[3];
            for(int i=0; i<this.Team.Length; i++)
            {
                this.Team[i] = new Champion();
            }
        }

        public Player() : base()
        {
            this.MaxCapacity = (int)this.Strength.Basic;
            this.Team = new Champion[3];
            for (int i = 0; i < this.Team.Length; i++)
            {
                this.Team[i] = new Champion();
            }
        }


    }
}
