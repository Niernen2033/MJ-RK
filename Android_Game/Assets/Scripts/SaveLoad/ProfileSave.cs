using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveLoad
{
    public class ProfileSave
    {
        public float MenuVolume { get; set; }
        
        private static ProfileSave instance = null;
        private static string Path = SaveInfo.Paths.GlobalFolder + "Profile.xml";

        public static ProfileSave Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProfileSave();
                }
                return instance;
            }
        }

        private ProfileSave()
        {
            this.MenuVolume = 1f;
        }

        public bool Load()
        {
            if (!XmlManager.Load<ProfileSave>(Path, out instance))
            {
                Debug.Log("Class 'ProfileSave' in 'Load' function: Cannot load file");
                return false;
            }
            return true;
        }

        public bool Update()
        {
            if (!XmlManager.Save<ProfileSave>(instance, Path))
            {
                Debug.Log("Class 'ProfileSave' in 'Update' function: Cannot save file");
                return false;
            }
            return true;
        }
    }
}