using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class ItemBonus
    {
        //Basic item bonus
        public int BasicBonus { get; set; }

        //Acctual bonus of item
        public int AcctualBonus { get; set; }

        public ItemBonus(int basicBonus)
        {
            this.BasicBonus = basicBonus;
            this.AcctualBonus = basicBonus;
        }

        public ItemBonus(int basicBonus, int acctualBonus)
        {
            this.BasicBonus = basicBonus;
            this.AcctualBonus = acctualBonus;
        }

        public ItemBonus()
        {
            this.BasicBonus = 0;
            this.AcctualBonus = 0;
        }

        public ItemBonus(ref ItemBonus itemBonus)
        {
            this.BasicBonus = itemBonus.BasicBonus;
            this.AcctualBonus = itemBonus.AcctualBonus;
        }
    }
}
