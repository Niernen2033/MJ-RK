using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Items
{
    public class Equipment
    {
        public Armor Helmet { get; set; }
        public Armor Body { get; set; }
        public Armor Boots { get; set; }
        public Weapon Weapon { get; set; }
        public Armor Gloves { get; set; }
        public Armor SecondHand { get; set; }

        [XmlElement(Type = typeof(Armor), ElementName = "Trinket")]
        public List<Armor> Trinkets { get; set; }
    }
}
