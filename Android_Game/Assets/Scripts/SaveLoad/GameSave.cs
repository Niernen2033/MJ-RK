using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC;
using CityScene;

namespace SaveLoad
{
    public sealed class GameSave
    {
        public GameGlobals.SceneIndex SceneIndex { get; set; }

        public string Name { get; set; }
        public string Texture { get; set; }

        public Player Player { get; set; }

        public CityData CityData { get; set; }

        private static GameSave instance = null;
        private readonly bool IsCryptoOn = false;

        public static GameSave Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameSave();
                }
                return instance;
            }
        }

        private GameSave()
        {
            this.SceneIndex = GameGlobals.SceneIndex.None;
            this.Name = string.Empty;
            this.Texture = string.Empty;
            this.Player = new Player();
            this.CityData = new CityData();
        }

        public bool Load(string loadPath)
        {
            if (!XmlManager.Load<GameSave>(loadPath, out instance, IsCryptoOn))
            {
                Debug.Log("Class 'Save' in 'Load' function: Cannot load file");
                return false;
            }

            this.ExecutePostInstantiate();

            return true;
        }

        private void ExecutePostInstantiate()
        {
            Instance.Player.PostInstantiate();
            Instance.CityData.PostInstantiate();
        }

        public bool Update()
        {
            if (!XmlManager.Save<GameSave>(instance, ProfileSave.Instance.AcctualSavePath, IsCryptoOn))
            {
                Debug.Log("Class 'Save' in 'Update' function: Cannot save file");
                return false;
            }
            return true;
        }
    }
}

