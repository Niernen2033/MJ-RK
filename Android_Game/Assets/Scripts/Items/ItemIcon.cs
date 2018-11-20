using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace Items
{
    public class ItemIcon
    {
        public int Index { get; set; }
        public int Rarity { get; set; }


        public ItemIcon()
        {
            this.Index = (int)ItemIndex.Special.No_Item;
            this.Rarity = (int)ItemRarity.None;
        }

        public ItemIcon(int itemIndex, int itemRarity)
        {
            this.Index = itemIndex;
            this.Rarity = itemRarity;
        }

        public ItemIcon(ItemIcon itemIcon)
        {
            this.Index = itemIcon.Index;
            this.Rarity = itemIcon.Rarity;
        }
    }
}
