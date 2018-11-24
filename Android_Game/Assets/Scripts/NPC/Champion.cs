using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using Items;


namespace NPC
{
    public enum ChampionClass { None = -1, Mage, Warrior, Range };
    public enum ChampionType { None = -1, Enemy, Normal };

    public class Champion
    {
        public string Name { get; set; }

        public ChampionType ChampionType { get; set; }
        //Champion class
        public ChampionClass ChampionClass { get; set; }

        public int Experience { get; set; }

        public int Level { get; set; }

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
        [XmlElement(Type = typeof(Item), ElementName = "ItemBagpackItem")]
        [XmlElement(Type = typeof(ConsumeableItem), ElementName = "ConsumeableBagpackItem")]
        [XmlElement(Type = typeof(Armor), ElementName = "ArmorBagpackItem")]
        [XmlElement(Type = typeof(Weapon), ElementName = "WeaponBagpackItem")]
        public List<Item> Bagpack { get; set; }

        public Equipment Equipment { get; set; }



        //CONSTRUCTORS************************************************************

        public Champion(ChampionClass championClass, ChampionType championType, string name,
            int experience, int level, Statistics vitality, Statistics magicArmor, Statistics rangedArmor, Statistics melleArmor,
            Statistics dexterity, Statistics intelligence, Statistics strength, bool illness, Equipment equipment, List<Item> bagpack)
        {
            this.Name = name;
            this.ChampionClass = championClass;
            this.ChampionType = championType;
            this.Experience = experience;
            this.Level = level;
            this.Vitality = vitality;
            this.MagicArmor = magicArmor;
            this.RangedArmor = rangedArmor;
            this.MelleArmor = melleArmor;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
            this.Strength = strength;
            this.IsIllness = illness;

            this.Equipment = equipment;
            this.Bagpack = new List<Item>(bagpack);
        }

        public Champion()
        {
            this.Name = string.Empty;
            this.ChampionClass = ChampionClass.None;
            this.ChampionType = ChampionType.None;
            this.Experience = 0;
            this.Level = 0;
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

        public Champion(Champion champion)
        {
            this.ChampionClass = champion.ChampionClass;
            this.ChampionType = champion.ChampionType;
            this.Experience = champion.Experience;
            this.Level = champion.Level;
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
        public virtual void PostInstantiate()
        {
            foreach(Item item in this.Bagpack)
            {
                item.PostInstantiate();
            }
            this.Equipment.PostInstantiate();

            this.Vitality.PostInstantiate();
            this.MagicArmor.PostInstantiate();
            this.RangedArmor.PostInstantiate();
            this.MelleArmor.PostInstantiate();
            this.Dexterity.PostInstantiate();
            this.Intelligence.PostInstantiate();
            this.Strength.PostInstantiate();
        }

    }
}

