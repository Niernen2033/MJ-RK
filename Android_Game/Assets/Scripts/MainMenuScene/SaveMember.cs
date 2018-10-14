using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScene
{
    public sealed class SavesEventArgs : System.EventArgs
    {
        public List<SaveMember> SaveMembers { get; private set; }
        public bool IsHaveSaves { get; private set; }
        public bool IfSavesNeedsToBeReloaded { get; private set; }

        public SavesEventArgs(List<SaveMember> saveMembers, bool ifSavesNeedsToBeReloaded)
        {
            this.SaveMembers = new List<SaveMember>(saveMembers);
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
        public string TexturePath { get; private set; }
        public string SaveName { get; private set; }
        public string SavePath { get; private set; }

        public SaveMember()
        {
            this.TexturePath = string.Empty;
            this.SaveName = string.Empty;
            this.SavePath = string.Empty;
        }

        public SaveMember(string texturePath, string saveDescription, string savePath)
        {
            if (texturePath != null)
            {
                this.TexturePath = string.Copy(texturePath);
            }
            else
            {
                this.TexturePath = string.Empty;
            }
            if(saveDescription != null)
            {
                this.SaveName = string.Copy(saveDescription);
            }
            else
            {
                this.SaveName = string.Empty;
            }
            if (savePath != null)
            {
                this.SavePath = string.Copy(savePath);
            }
            else
            {
                this.SavePath = string.Empty;
            }
        }

        public SaveMember(SaveMember saveMember)
        {
            this.TexturePath = saveMember.TexturePath;
            this.SaveName = saveMember.SaveName;
            this.SavePath = saveMember.SavePath;
        }
    }
}

