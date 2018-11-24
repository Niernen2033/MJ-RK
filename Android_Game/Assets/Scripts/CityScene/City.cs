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
    public enum CityObjectType { BlackSmith, Tawern, Church, Dungeons, CityAll };

    public delegate void OpenBuildingCallback(CityObjectType cityObjectType);

    public class City : MonoBehaviour
    {
        public GameObject cityAll;
        public GameObject blackSmith;
        public GameObject tawern;
        public GameObject church;

        private CityData cityData;
        public bool BlockChangingBuilding { get; private set; }

        // Use this for initialization
        public void Awake()
        {
            if (GameGlobals.IsDebugState)
            {
                ProfileSave.Instance.Load();
                GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);
                //XmlManager.Save<GameSave>(GameSave.Instance, ProfileSave.Instance.AcctualSavePath, false);
            }

            this.BlockChangingBuilding = false;
            this.cityData = GameSave.Instance.CityData;
            this.cityData.Reload(1);

            //Debug.Log("Itemy po wczytaniu: " + GameSave.Instance.Player.Bagpack.Count);

            //ItemGenerator itemGenerator = new ItemGenerator();
            //GameSave.Instance.Player.Bagpack.Add(itemGenerator.GenerateGoldByValue(10000));

            //GameSave.Instance.Player.Bagpack.Clear();
            //GameSave.Instance.Update();

        }

        public void Start()
        {
            this.OpenBuilding(this.cityData.CityObjectType);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeBuildingBlockStatus(bool status)
        {
            this.BlockChangingBuilding = status;
        }

        public void OpenBuilding(CityObjectType cityObjectType)
        {
            switch (cityObjectType)
            {
                case CityObjectType.BlackSmith:
                    {
                        this.blackSmith.SetActive(true);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        this.cityData.CityObjectType = CityObjectType.BlackSmith;
                        break;
                    }
                case CityObjectType.Church:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(true);
                        this.tawern.SetActive(false);
                        this.cityData.CityObjectType = CityObjectType.Church;
                        break;
                    }
                case CityObjectType.CityAll:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(true);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        this.cityData.CityObjectType = CityObjectType.CityAll;
                        this.cityAll.GetComponent<CityAll>().RefreshPlayerTeam();
                        break;
                    }
                case CityObjectType.Tawern:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(false);
                        this.church.SetActive(false);
                        this.tawern.SetActive(true);
                        this.cityData.CityObjectType = CityObjectType.Tawern;
                        break;
                    }
                case CityObjectType.Dungeons:
                    {
                        //GameSave.Instance.SceneIndex = GameGlobals.SceneIndex.
                        //GameSave.Instance.Update();
                        //SceneManager.LoadScene((int)GameSave.Instance.SceneIndex);
                        break;
                    }
                default:
                    {
                        this.blackSmith.SetActive(false);
                        this.cityAll.SetActive(true);
                        this.church.SetActive(false);
                        this.tawern.SetActive(false);
                        this.cityData.CityObjectType = CityObjectType.CityAll;
                        this.cityAll.GetComponent<CityAll>().RefreshPlayerTeam();
                        break;
                    }
            }
            GameSave.Instance.Update();
        }
    }
}
