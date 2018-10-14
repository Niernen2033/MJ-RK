using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public sealed class Save
{
    public bool IsAcctual { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Texture { get; set; }
    public Player Player { get; set; }

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
        this.IsAcctual = false;
        this.Name = string.Empty;
        this.Path = string.Empty;
        this.Player = new Player();
    }

    public bool Load(string loadPath)
    {
        if(!XmlManager.Load<Save>(loadPath, out instance))
        {
            Debug.Log("Class 'Save' in 'Load' function: Cannot load file");
            return false;
        }
        return true;
    }

    public bool Update()
    {
        if(!XmlManager.Save<Save>(instance, this.Path))
        {
            Debug.Log("Class 'Save' in 'Update' function: Cannot save file");
            return false;
        }
        return true;
    }

    //class
    public static class Paths
    {
        public static readonly string GlobalFolder = @"Assets/Saves/";
        public static string AcctualSave = "";
    }
}

