using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class RangedItem : Item
    {
        public RangedItem(string name, int defenceBonus, int attackBonus, float durability, int goldValue, float weight,
            bool used = false, bool broken = false) : base(name, defenceBonus, attackBonus, durability, goldValue, weight, used, broken)
        {

        }

        public RangedItem() : base()
        {

        }
    }
}
