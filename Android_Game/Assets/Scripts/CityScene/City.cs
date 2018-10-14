﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CityScene
{
    public enum CityObjectType { BlackSmith, Tawern, Church, PlayerHouse, Dungeons, CityAll };
    public delegate void OpenBuildingCallback(CityObjectType cityObjectType);

    public class City : MonoBehaviour
    {
        private Player player;
        public GameObject cityAll;
        public GameObject blackSmithHouse;

        // Use this for initialization
        public void Awake()
        {
            if (GameGlobals.IsDebugState)
            {
                Save.Paths.AcctualSave = @"D:\Repos\MJ-RK\Android_Game\Assets\Saves\testy\tt.xml";

                if (!File.Exists(Save.Paths.AcctualSave))
                {
                    Debug.Log("Class 'Menu' in 'Start' function: File doesn't exist");
                }
                else
                {
                    Save.Instance.Load(Save.Paths.AcctualSave);
                }
            }
        }

        public void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenBuilding(CityObjectType cityObjectType)
        {
            switch (cityObjectType)
            {
                case CityObjectType.BlackSmith:
                    {
                        this.blackSmithHouse.SetActive(true);
                        this.cityAll.SetActive(false);
                        break;
                    }
                case CityObjectType.Church:
                    {
                        break;
                    }
                case CityObjectType.Dungeons:
                    {
                        break;
                    }
                case CityObjectType.PlayerHouse:
                    {
                        break;
                    }
                case CityObjectType.Tawern:
                    {
                        break;
                    }
                case CityObjectType.CityAll:
                    {
                        break;
                    }
            }
        }
    }
}
