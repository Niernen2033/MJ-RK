using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SavesEventArgs : System.EventArgs
{
    public List<SaveMember> SaveMembers { get; private set; }
    public bool HaveSaves { get; private set; }

    public SavesEventArgs(List<SaveMember> saveMembers)
    {
        this.SaveMembers = saveMembers;
        if (this.SaveMembers.Count == 0)
            this.HaveSaves = false;
        else
            this.HaveSaves = true;
    }

    public SavesEventArgs()
    {
        this.SaveMembers = new List<SaveMember>();
        this.HaveSaves = false;
    }
}

public class SaveMember
{
    public Texture2D Texture { get; set; }
    public Text Description { get; set; }

    public SaveMember()
    {
        this.Texture = null;
        this.Description = null;
    }
}

