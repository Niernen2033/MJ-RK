using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC;
using CityScene;
using System.IO;

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

        public bool CreateNewSave(string name)
        {
            if(name == string.Empty)
            {
                Debug.Log("Class 'Save' in 'CreateNewSave' function: Name is empty");
                return false;
            }
            GameSave newGameSave = new GameSave();
            newGameSave.Name = name;
            string new_save_path = SaveInfo.Paths.GlobalFolder + name + "_data_";
            int new_save_path_index = 1;
            while(true)
            {
                string temp_save_path = string.Copy(new_save_path) + new_save_path_index.ToString();
                if(!Directory.Exists(temp_save_path))
                {
                    Directory.CreateDirectory(temp_save_path);
                    new_save_path = string.Copy(temp_save_path) + "/";
                    break;
                }
                else
                {
                    new_save_path_index++;
                }
                    
            }
            if(!XmlManager.Save<GameSave>(newGameSave, new_save_path + name + ".xml", IsCryptoOn))
            {
                Debug.Log("Class 'Save' in 'CreateNewSave' function: Cannot save file");
                return true;
            }
            else
            {
                return false;
            }
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

