using System.Collections;
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
        public enum ObjectType { BlackSmith, Tawern, Church, Dungeons, CityAll };
        public GameObject cityAll;
        public GameObject blackSmith;
        public GameObject tawern;
        public GameObject church;
        public GameObject inventory;

        // Use this for initialization
        public void Awake()
        {
            if (GameGlobals.IsDebugState)
            {
                ProfileSave.Instance.Load();
            }
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);
            //XmlManager.Save<GameSave>(GameSave.Instance, ProfileSave.Instance.AcctualSavePath, false);
            GameSave.Instance.CityData.Reload(1);

            //GameSave.Instance.Player.Bagpack.Clear();

            //ItemGenerator itemGenerator = new ItemGenerator();
            //Item item = itemGenerator.GenerateGoldByValue(1000);
            //GameSave.Instance.Player.Bagpack.Add(item);
            //GameSave.Instance.Update();
        }

        public void Start()
        {
            this.OpenBuilding(ObjectType.CityAll);
        }

        // Update is called once per frame
        void Update()
        {

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
            switch (cityObjectType)
            {
                case ObjectType.BlackSmith:
                    {
                        this.blackSmith.SetActive(true);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        break;
                    }
                case ObjectType.Church:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(true);
                        this.tawern.SetActive(false);
                        break;
                    }
                case ObjectType.CityAll:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(true);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        break;
                    }
                case ObjectType.Tawern:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(false);
                        this.tawern.SetActive(true);
                        break;
                    }
                default:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(true);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        break;
                    }
            }
        }
    }
}
