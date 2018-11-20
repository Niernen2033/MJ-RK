using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Items
{
    public class ItemNameData
    {
        [XmlElement(Type = typeof(string), ElementName = "ArmorBodyItemNamesItem")]
        public List<string> ArmorBodyItemNames { get; set; }


        [XmlElement(Type = typeof(string), ElementName = "ArmorShieldItemNamesItem")]
        public List<string> ArmorShieldItemNames { get; set; }


        [XmlElement(Type = typeof(string), ElementName = "ArmorHelmetItemNamesItem")]
        public List<string> ArmorHelmetItemNames { get; set; }


        [XmlElement(Type = typeof(string), ElementName = "ArmorBootsItemNamesItem")]
        public List<string> ArmorBootsItemNames { get; set; }


        [XmlElement(Type = typeof(string), ElementName = "ArmorGlovesItemNamesItem")]
        public List<string> ArmorGlovesItemNames { get; set; }


        [XmlElement(Type = typeof(string), ElementName = "WeaponMelleItemNamesItem")]
        public List<string> WeaponMelleItemNames { get; set; }

        [XmlElement(Type = typeof(string), ElementName = "WeaponMagicItemNamesItem")]
        public List<string> WeaponMagicItemNames { get; set; }

        [XmlElement(Type = typeof(string), ElementName = "WeaponRangedItemNamesItem")]
        public List<string> WeaponRangedItemNames { get; set; }

        [XmlElement(Type = typeof(string), ElementName = "TrinketItemNamesItem")]
        public List<string> TrinketItemNames { get; set; }

        public ItemNameData()
        {
            this.WeaponMagicItemNames = new List<string>();
            this.WeaponMelleItemNames = new List<string>();
            this.WeaponRangedItemNames = new List<string>();

            this.ArmorBodyItemNames = new List<string>();
            this.ArmorBootsItemNames = new List<string>();
            this.ArmorGlovesItemNames = new List<string>();
            this.ArmorHelmetItemNames = new List<string>();
            this.ArmorShieldItemNames = new List<string>();

            this.TrinketItemNames = new List<string>();
        }
    }
}
