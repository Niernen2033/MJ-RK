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
        public Armor Shield { get; set; }
        public Armor Trinket { get; set; }

        public Equipment()
        {
            this.Helmet = null;
            this.Body = null;
            this.Boots = null;
            this.Weapon = null;
            this.Gloves = null;
            this.Shield = null;
            this.Trinket = null;
        }
    }
}
