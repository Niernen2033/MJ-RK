using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public abstract class Champion
    {
        //
        public int HP;

        public bool Illness;

        public int BasicAttack;

        public int BasicDefence;

        protected Champion(int hp, int basicAttack, int basicDefence, bool illness)
        {
            this.HP = hp;
            this.BasicAttack = basicAttack;
            this.BasicDefence = basicDefence;
            this.Illness = illness;
        }

        protected Champion()
        {
            this.HP = 1;
            this.BasicAttack = 0;
            this.BasicDefence = 0;
            this.Illness = false;
        }

        protected Champion(ref Champion champion)
        {
            this.HP = champion.HP;
            this.BasicAttack = champion.BasicAttack;
            this.BasicDefence = champion.BasicDefence;
            this.Illness = champion.Illness;
        }
    }

}