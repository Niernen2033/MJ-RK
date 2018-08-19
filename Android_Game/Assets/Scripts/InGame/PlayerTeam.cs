using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

using InGame;

public class PlayerTeam : MonoBehaviour
{
    public Player Player;

    public void Start()
    {
        this.Player = new Player(100, 10, 10, false);
    }

    public void Save()
    {
        XmlManager<Player> xmlManager = new XmlManager<Player>();
        xmlManager.Save(this.Player, "Player.xml");
    }

    public void StoreData()
    {

    }

}
