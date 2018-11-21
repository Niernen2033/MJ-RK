﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using NPC;
using Items;
using Prefabs.Inventory;

using SaveLoad;

namespace CityScene
{
    public delegate void OpenBuildingCallback(City.ObjectType cityObjectType);

    public class City : MonoBehaviour
    {
        public enum ObjectType { BlackSmith, Tawern, Church, PlayerHouse, Dungeons, CityAll };
        public GameObject cityAll;
        public GameObject blackSmith;
        public GameObject tawern;
        public GameObject playerHouse;
        public GameObject church;
        public GameObject inventory;

        // Use this for initialization
        public void Awake()
        {
            ProfileSave.Instance.Load();
            //GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);
        }

        public void Start()
        {
            this.OpenBuilding(ObjectType.CityAll);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenInventory()
        {           
            EqInventory shopInventory = inventory.gameObject.GetComponent<EqInventory>();
            if (!shopInventory.IsOpen)
            {
                if(shopInventory.PlayerBagpack == null)
                {
                    shopInventory.OpenAndLoadInventory(GameSave.Instance.Player.Equipment, GameSave.Instance.Player.Bagpack, null);
                }
                if (shopInventory.PlayerBagpack.IsDataLoaded/* || shopInventory.ShopBagpack.IsDataLoaded*/)
                {
                    shopInventory.OpenInventory();
                }
                else
                {
                    shopInventory.OpenAndLoadInventory(GameSave.Instance.Player.Equipment, GameSave.Instance.Player.Bagpack, null);
                }
            }
            else
            {
                shopInventory.CloseInventory();
            }
        }


        public void AddTestItem()
        {
            Armor a = new Armor();
            Weapon b = new Weapon();
            a.VitalityBonus = new Statistics(20);
            a.Features.EnableAllFeatures();
            a.Icon.Rarity = ItemIndexManagement.GetItemRarityIndex(ItemRarity.Legendary);
            a.GoldValue = 60;
            a.Durability = 10;
            a.EquipmentType = EqType.Gloves;

            b.Features.EnableAllFeatures();
            b.BasicDamage = new Statistics(50);
            b.EquipmentType = EqType.Weapon;

            ItemGenerator itemGenerator = new ItemGenerator();

            Armor c = itemGenerator.GenerateArmor(1, ItemClass.Melle, ItemType.Trinket, EqType.Trinket);
            Weapon d = itemGenerator.GenerateWeapon(1, ItemClass.Melle);
            Item e = itemGenerator.GenerateJunk(ItemSubType.Junk_Gems);
            inventory.GetComponent<EqInventory>().PlayerBagpack.AddItem(e);
        }

        public void AddTestGold()
        {
            Item b = new Item();
            b.Icon.Index = (int)ItemIndex.Gold.Large;
            b.GoldValue = 500;
            inventory.GetComponent<EqInventory>().PlayerBagpack.AddItem(b);
        }


        public void OpenBuilding(ObjectType cityObjectType)
        {
            if (!this.inventory.activeSelf)
            {
                switch (cityObjectType)
                {
                    case ObjectType.BlackSmith:
                        {
                            this.blackSmith.SetActive(true);
                            this.cityAll.SetActive(false);
                            this.church.SetActive(false);
                            this.playerHouse.SetActive(false);
                            this.tawern.SetActive(false);
                            break;
                        }
                    case ObjectType.Church:
                        {
                            this.blackSmith.SetActive(false);
                            this.cityAll.SetActive(false);
                            this.church.SetActive(true);
                            this.playerHouse.SetActive(false);
                            this.tawern.SetActive(false);
                            break;
                        }
                    case ObjectType.CityAll:
                        {
                            this.blackSmith.SetActive(false);
                            this.cityAll.SetActive(true);
                            this.church.SetActive(false);
                            this.playerHouse.SetActive(false);
                            this.tawern.SetActive(false);
                            break;
                        }
                    case ObjectType.PlayerHouse:
                        {
                            this.blackSmith.SetActive(false);
                            this.cityAll.SetActive(false);
                            this.church.SetActive(false);
                            this.playerHouse.SetActive(true);
                            this.tawern.SetActive(false);
                            break;
                        }
                    case ObjectType.Tawern:
                        {
                            this.blackSmith.SetActive(false);
                            this.cityAll.SetActive(false);
                            this.church.SetActive(false);
                            this.playerHouse.SetActive(false);
                            this.tawern.SetActive(true);
                            break;
                        }
                    default:
                        {
                            this.blackSmith.SetActive(false);
                            this.cityAll.SetActive(true);
                            this.church.SetActive(false);
                            this.playerHouse.SetActive(false);
                            this.tawern.SetActive(false);
                            break;
                        }
                }
            }
        }
    }
}
