using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemIcon
    {
        public int Index { get; set; }
        public int SlotIndex { get; set; }
        public ItemRarity Rarity { get; set; }

        public ItemIcon()
        {
            this.Index = (int)ItemIconIndex.Special.No_Item;
            this.Rarity = ItemRarity.None;
            this.SlotIndex = -1;
        }

        public ItemIcon(int index, ItemRarity itemRarity)
        {
            this.Index = index;
            this.Rarity = itemRarity;
            this.SlotIndex = -1;
        }
    }
}
