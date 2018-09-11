using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Save
{

    public bool IsAcctualSave { get; set; }
    public string SaveName { get; set; }
    public string SavePath { get; set; }


    private static Save instance = null;

    public static Save Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Save();
            }
            return instance;
        }
    }

    private Save()
    {
        this.IsAcctualSave = false;
        this.SaveName = string.Empty;
        this.SavePath = string.Empty;
    }

    public bool LoadFile(string loadPath)
    {
        if(!XmlManager.Load<Save>(loadPath, out instance))
        {
            Debug.Log("Class 'Save' in 'Load' function: Cannot load file");
            return false;
        }
        return true;
    }

    public bool SaveFile(string savePath)
    {
        if(!XmlManager.Save<Save>(instance, savePath))
        {
            Debug.Log("Class 'Save' in 'Save' function: Cannot save file");
            return false;
        }
        return true;
    }

    //class
    public static class SavePaths
    {
        public static string GlobalSavesPath = @"Assets/Saves/";
    }
}

