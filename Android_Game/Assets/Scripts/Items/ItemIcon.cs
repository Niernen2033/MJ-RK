using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemIcon
    {
        public int Index { get; set; }
        public ItemRarity Rarity { get; set; }

        public ItemIcon()
        {
            this.Index = (int)ItemIndex.Special.No_Item;
            this.Rarity = ItemRarity.None;
        }

        public ItemIcon(int index, ItemRarity itemRarity)
        {
            this.Index = index;
            this.Rarity = itemRarity;
        }
    }
}
