using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaveLoad
{
    public sealed class GameSave
    {
        public string Name { get; set; }
        public string Texture { get; set; }

        public Player Player { get; set; }

        private static GameSave instance = null;
        private string SavePath;

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
            this.Name = string.Empty;
            this.SavePath = string.Empty;
            this.Player = new Player();
        }

        public bool Load(string loadPath)
        {
            if (!XmlManager.Load<GameSave>(loadPath, out instance))
            {
                Debug.Log("Class 'Save' in 'Load' function: Cannot load file");
                return false;
            }
            this.SavePath = loadPath;
            return true;
        }

        public bool Update()
        {
            if (!XmlManager.Save<GameSave>(instance, this.SavePath))
            {
                Debug.Log("Class 'Save' in 'Update' function: Cannot save file");
                return false;
            }
            return true;
        }
    }
}

