using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class City : MonoBehaviour
{
    Player player;

	// Use this for initialization
	void Start()
    {
        if(!File.Exists(@"Assets/Saves/Player.xml"))
        {
            Debug.Log("new");
            this.player = new Player();
            XmlManager.Save<Player>(this.player, "Player.xml");
        }
        else
        {
            Debug.Log("load");
            XmlManager.Load<Player>("Player.xml", out this.player);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
