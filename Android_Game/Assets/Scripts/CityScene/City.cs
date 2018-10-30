using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using NPC;
using Items;

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
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);
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
            inventory.GetComponent<Inventory>().Open(GameSave.Instance.Player.Backpack, new Vector2(500, 250));
        }

        public void CloseInventory()
        {
            inventory.GetComponent<Inventory>().Close();
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
