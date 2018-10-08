using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SavesEventArgs : System.EventArgs
{
    public List<SaveMember> SaveMembers { get; private set; }
    public bool IsHaveSaves { get; private set; }
    public bool IfSavesNeedsToBeReloaded { get; private set; }

    public SavesEventArgs(List<SaveMember> saveMembers, bool ifSavesNeedsToBeReloaded)
    {
        this.SaveMembers = saveMembers;
        if (this.SaveMembers.Count == 0)
            this.IsHaveSaves = false;
        else
            this.IsHaveSaves = true;

        this.IfSavesNeedsToBeReloaded = ifSavesNeedsToBeReloaded;
    }

    public SavesEventArgs(bool ifSavesNeedsToBeReloaded)
    {
        this.SaveMembers = new List<SaveMember>();
        this.IsHaveSaves = false;
        this.IfSavesNeedsToBeReloaded = ifSavesNeedsToBeReloaded;
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

