using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace InGame
{
    [System.Serializable]
    public class Player : Champion
    {
        [XmlElement(Type = typeof(MagicItem), ElementName = "MagicItem")]
        [XmlElement(Type = typeof(RangedItem), ElementName = "RangedItem")]
        [XmlElement(Type = typeof(MelleItem), ElementName = "MelleItem")]
        public List<Item> Items { get; set; }

        public Player(int hp, int basicAttack, int basicDefence, bool illness) : base(hp, basicAttack, basicDefence, illness)
        {
            this.Items = new List<Item>();
            this.Items.Add(new RangedItem("bb", 12, 3, 1, 27, 99));
            this.Items.Add(new MelleItem("cb", 12, 3, 1, 27, 99));
            Debug.Log("Saved");
        }

        public Player() : base()
        {

        }
    }
}
