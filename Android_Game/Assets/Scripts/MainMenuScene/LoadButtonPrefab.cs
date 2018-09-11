using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonPrefab : MonoBehaviour
{
    public Text Text;
    public Image Image;
    
    public LoadButtonPrefab()
    {
        this.Text =  null;
        this.Image = null;
    }

    public LoadButtonPrefab(string text, string path_to_image)
    {
        this.Text.text = text;
        this.Image = Resources.Load<Image>(path_to_image);
    }
}
