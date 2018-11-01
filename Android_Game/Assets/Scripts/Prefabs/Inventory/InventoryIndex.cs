using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prefabs.Inventory
{
    public sealed class InventoryIndex
    {
        public enum Options
        {
            Sell = 0,
            Delete = 1,
            Eat = 2,
            Equip = 3,
            Info = 4,
            Repair = 5,
            Disequip = 6,
            Upgrade = 7,
        }
    }
}
