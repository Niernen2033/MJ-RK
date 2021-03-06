﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Items;
using NPC;
using UnityEngine;

namespace CityScene
{
    public class CityData : IPostInstantiate
    {

        [XmlElement(Type = typeof(Item), ElementName = "ItemBlackSmithShopBagpackItem")]
        [XmlElement(Type = typeof(ConsumeableItem), ElementName = "ConsumeableBlackSmithShopBagpackItem")]
        [XmlElement(Type = typeof(Armor), ElementName = "ArmorBlackSmithShopBagpackItem")]
        [XmlElement(Type = typeof(Weapon), ElementName = "WeaponBlackSmithShopBagpackItem")]
        public List<Item> BlackSmithShopBagpack { get; set; }

        [XmlElement(Type = typeof(Item), ElementName = "ItemTawernShopBagpackItem")]
        [XmlElement(Type = typeof(ConsumeableItem), ElementName = "ConsumeableTawernShopBagpackItem")]
        [XmlElement(Type = typeof(Armor), ElementName = "ArmorTawernShopBagpackItem")]
        [XmlElement(Type = typeof(Weapon), ElementName = "WeaponTawernShopBagpackItem")]
        public List<Item> TawernShopBagpack { get; set; }

        [XmlElement(Type = typeof(Item), ElementName = "ItemChurchShopBagpackItem")]
        [XmlElement(Type = typeof(ConsumeableItem), ElementName = "ConsumeableChurchShopBagpackItem")]
        [XmlElement(Type = typeof(Armor), ElementName = "ArmorChurchShopBagpackItem")]
        [XmlElement(Type = typeof(Weapon), ElementName = "WeaponChurchShopBagpackItem")]
        public List<Item> ChurchShopBagpack { get; set; }

        public CityObjectType CityObjectType { get; set; }

        [XmlElement(Type = typeof(CityDataChampion), ElementName = "TawernCityDataChampionItem")]
        public List<CityDataChampion> TawernChampions { get; set; }

        public CityData()
        {
            this.CityObjectType = CityObjectType.CityAll;
            this.BlackSmithShopBagpack = new List<Item>();
            this.TawernShopBagpack = new List<Item>();
            this.ChurchShopBagpack = new List<Item>();
            this.TawernChampions = new List<CityDataChampion>();
        }

        public void Reload(int playerLevel)
        {
            this.BlackSmithShopBagpack.Clear();
            this.GenerateBlackSmithShopBagpack(playerLevel);

            this.ChurchShopBagpack.Clear();
            this.GenerateChurchShopBagpack(playerLevel);

            this.TawernShopBagpack.Clear();
            this.GenerateTawernShopBagpack(playerLevel);

            this.TawernChampions.Clear();
            this.GenerateTawernChampions(playerLevel);
        }

        private void GenerateBlackSmithShopBagpack(int playerLevel)
        {
            
            ItemGenerator itemGenerator = new ItemGenerator();

            this.BlackSmithShopBagpack.Add(itemGenerator.GenerateGoldByLevel(playerLevel));

            for (int i = 0; i < 15; i++)
            {
                int randArmorClass = CryptoRandom.Next(1, 3);//1-3
                int randArmorEq = CryptoRandom.Next(2, 6);

                if ((ItemClass)randArmorClass == ItemClass.Ranged)
                {
                    if ((EqType)randArmorEq == EqType.Shield)
                    {
                        while (true)
                        {
                            randArmorEq = CryptoRandom.Next(2, 6); //2-6
                            if ((EqType)randArmorEq != EqType.Shield)
                            {
                                break;
                            }
                        }
                    }
                }

                //Debug.Log(i + " : " + (EqType)randArmorEq + " : " + (ItemClass)randArmorClass);
                Item armor = itemGenerator.GenerateArmor(playerLevel, (ItemClass)randArmorClass, ItemType.Armor, (EqType)randArmorEq);
                this.BlackSmithShopBagpack.Add(armor);
            }
            
            for(int i=0; i<10; i++)
            {
                int randArmorClass = CryptoRandom.Next(1, 3);
                Item armor = itemGenerator.GenerateWeapon(playerLevel, (ItemClass)randArmorClass);
                this.BlackSmithShopBagpack.Add(armor);
            }

            for(int i=0; i<10; i++)
            {
                int randArmorClass = CryptoRandom.Next(1, 3);
                Item armor = itemGenerator.GenerateArmor(playerLevel, (ItemClass)randArmorClass, ItemType.Trinket, EqType.Trinket);
                this.BlackSmithShopBagpack.Add(armor);
            }
            
        }

        private void GenerateTawernShopBagpack(int playerLevel)
        {
            ItemGenerator itemGenerator = new ItemGenerator();

            this.TawernShopBagpack.Add(itemGenerator.GenerateGoldByLevel(playerLevel));

            for (int i = 0; i < 10; i++)
            {
                int randJunk = CryptoRandom.Next(6, 10);
                Item armor = itemGenerator.GenerateJunk((ItemSubType)randJunk);
                this.TawernShopBagpack.Add(armor);
            }

            for (int i = 0; i < 6; i++)
            {
                int randJunkType = CryptoRandom.Next(0, 1);
                if(randJunkType == 0)
                {
                    randJunkType = (int)ItemType.Food;
                }
                else
                {
                    randJunkType = (int)ItemType.Potion;
                }
                int randJunk = CryptoRandom.Next(0, 5);
                //Debug.Log(i + " : " + (ItemType)randJunkType + " : " + (ItemSubType)randJunk);
                ConsumeableItem consumeItem = itemGenerator.GenerateConsumeableItem((ItemType)randJunkType, (ItemSubType)randJunk);
                this.TawernShopBagpack.Add(consumeItem);
            }
        }

        private void GenerateChurchShopBagpack(int playerLevel)
        {
            ItemGenerator itemGenerator = new ItemGenerator();

            this.ChurchShopBagpack.Add(itemGenerator.GenerateGoldByLevel(playerLevel));

            for (int i = 0; i < 15; i++)
            {
                int randPotion = CryptoRandom.Next(0, 5);
                //Debug.Log((ItemSubType)randPotion);
                ConsumeableItem consumeItem = itemGenerator.GenerateConsumeableItem(ItemType.Potion, (ItemSubType)randPotion);
                this.ChurchShopBagpack.Add(consumeItem);
            }
        }

        private void GenerateTawernChampions(int playerLevel)
        {
            ChampionGenerator championGenerator = new ChampionGenerator();

            Champion mageChampion = championGenerator.GenerateChampion(playerLevel, ChampionClass.Mage, ChampionType.Normal);
            Champion rangedChampion = championGenerator.GenerateChampion(playerLevel, ChampionClass.Range, ChampionType.Normal);
            Champion melleChampion = championGenerator.GenerateChampion(playerLevel, ChampionClass.Warrior, ChampionType.Normal);

            this.TawernChampions.Add(new CityDataChampion(mageChampion, true));
            this.TawernChampions.Add(new CityDataChampion(rangedChampion, true));
            this.TawernChampions.Add(new CityDataChampion(melleChampion, true));
        }

        public virtual void PostInstantiate()
        {
            foreach(Item item in this.BlackSmithShopBagpack)
            {
                item.PostInstantiate();
            }
            foreach (Item item in this.TawernShopBagpack)
            {
                item.PostInstantiate();
            }
            foreach (Item item in this.ChurchShopBagpack)
            {
                item.PostInstantiate();
            }
            foreach(CityDataChampion cityDataChampion in this.TawernChampions)
            {
                cityDataChampion.PostInstantiate();
            }
        }
    }

    public class CityDataChampion
    {
        public Champion Champion { get; set; }
        public bool Available { get; set; }

        public CityDataChampion()
        {
            this.Champion = new Champion();
            this.Available = false;
        }

        public CityDataChampion(Champion champion, bool available)
        {
            this.Champion = champion;
            this.Available = available;
        }

        public virtual void PostInstantiate()
        {
            this.Champion.PostInstantiate();
        }
    }
}
