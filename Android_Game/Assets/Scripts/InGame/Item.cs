using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InGame
{
    public abstract class Item
    {
        //ON or OFF Debug informations
        private bool DegubInfo = false;

        //BASIC VARIABLES ====================================================================================

        //Randomizer
        protected System.Random RandomGenerator;

        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Bonus to basic champions defence (will be add to basic champion statistic) (MIN VALUE: 0)
        /// </summary>
        public ItemBonus DefenceBonus { get; set; }

        /// <summary>
        /// Bonus to basic champions attack (will be add to basic champion statistic) (MIN VALUE: 0)
        /// </summary>
        public ItemBonus AttackBonus { get; set; }

        /// <summary>
        /// Establish that champion used this item or not (set to false[not used] by default]
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// Establish that item is broken or not (set to false[not broken] by default]
        /// </summary>
        public bool Broken { get; set; }

        /// <summary>
        /// Durability of item (MAX VALUE: 100 || MIN VALUE: 0)
        /// </summary>
        public float Durability { get; set; }

        /// <summary>
        /// Item basic value
        /// </summary>
        public int GoldValue { get; set; }

        /// <summary>
        /// Item basic weight
        /// </summary>
        public float Weight { get; set; }


        //BASIC CONSTRUCTORS ====================================================================================
        protected Item(string name, int defenceBonus, int attackBonus, float durability, int goldValue, float weight, bool used, bool broken)
        {
            this.RandomGenerator = new System.Random();

            this.Name = name;

            this.DefenceBonus = new ItemBonus(defenceBonus);
            this.AttackBonus = new ItemBonus(attackBonus);

            this.Durability = durability;
            this.GoldValue = goldValue;
            this.Used = used;
            this.Broken = broken;
            this.Weight = weight;
        }

        protected Item()
        {
            this.RandomGenerator = new System.Random();

            this.Name = string.Empty;

            this.DefenceBonus = new ItemBonus(0);
            this.AttackBonus = new ItemBonus(0);

            this.Durability = 0;
            this.GoldValue = 0;
            this.Used = false;
            this.Broken = false;
            this.Weight = 0;
        }

        //Copy constructor
        protected Item(ref Item championItem)
        {
            this.RandomGenerator = new System.Random();
            this.Name = championItem.Name;

            ItemBonus defenceBonus = new ItemBonus(championItem.DefenceBonus.BasicBonus, championItem.DefenceBonus.AcctualBonus);
            ItemBonus attackBonus = new ItemBonus(championItem.AttackBonus.BasicBonus, championItem.AttackBonus.AcctualBonus);
            this.DefenceBonus = new ItemBonus(ref defenceBonus);
            this.AttackBonus = new ItemBonus(ref attackBonus);

            this.Durability = championItem.Durability;
            this.GoldValue = championItem.GoldValue;
            this.Used = championItem.Used;
            this.Broken = championItem.Broken;
            this.Weight = championItem.Weight;
        }

        //BASIC FUNCTIONS ====================================================================================

        /// <summary>
        /// Function change basic item name to 'name'
        /// </summary>
        /// <param name="name"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool ChangeItemName(string name)
        {
            try
            {
                this.Name = name;
                return true;
            }
            catch (Exception exc)
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'ChangeItemName' function:" + exc.ToString());
                return false;
            }
        }

        /// <summary>
        /// Function decrease ACCTUAL(not basic) attack bonus by 'decreaseCount'.  MIN VALUE = 0.
        /// Function acction has chance to fail if 'chance' is smaller than 100.
        /// 'chance' is set by default to 100 (100% success)
        /// </summary>
        /// <param name="decreaseCount"></param>
        /// <param name="chance"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool DecreaseAttackBonus(int decreaseCount, int chance = 100)
        {
            int RandomChanceNumber = this.RandomGenerator.Next(0, 100);
            bool ifRNGSuccess = false;
            if (RandomChanceNumber <= chance)
                ifRNGSuccess = true;

            if (ifRNGSuccess == false)
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'DecreaseMagicAttackBonus' function: RNG fail");
                return false;
            }

            if ((decreaseCount >= 0) && (this.AttackBonus.AcctualBonus != 0))
            {
                try
                {
                    this.AttackBonus.AcctualBonus -= decreaseCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'DecreaseMagicAttackBonus' function:" + exc.ToString());
                    return false;
                }

                if (this.AttackBonus.AcctualBonus < 0)
                    this.AttackBonus.AcctualBonus = 0;

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'DecreaseMagicAttackBonus' function: Wrong magicDecreaseCount number or bonus is equal to 0");
                return false;
            }
        }

        /// <summary>
        /// Function decrease ACCTUAL(not basic) defence bonus by 'decreaseCount'. MIN VALUE = 0.
        /// Function acction has chance to fail if 'chance' is smaller than 100.
        /// 'chance' is set by default to 100 (100% success)
        /// </summary>
        /// <param name="decreaseCount"></param>
        /// <param name="chance"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool DecreaseDefenceBonus(int decreaseCount, int chance = 100)
        {
            int RandomChanceNumber = this.RandomGenerator.Next(0, 100);
            bool ifRNGSuccess = false;
            if (RandomChanceNumber <= chance)
                ifRNGSuccess = true;

            if (ifRNGSuccess == false)
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'DecreaseMagicDefenceBonus' function: RNG fail");
                return false;
            }

            if ((decreaseCount >= 0) && (this.DefenceBonus.AcctualBonus != 0))
            {
                try
                {
                    this.DefenceBonus.AcctualBonus -= decreaseCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'DecreaseMagicDefenceBonus' function:" + exc.ToString());
                    return false;
                }

                if (this.DefenceBonus.AcctualBonus < 0)
                    this.DefenceBonus.AcctualBonus = 0;

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'DecreaseMagicDefenceBonus' function: Wrong magicDecreaseCount number or bonus is equal to 0");
                return false;
            }
        }

        /// <summary>
        /// Function boost ACCTUAL(not basic) attact bonus by 'boostCount'.
        /// Function acction has chance to fail if 'chance' is smaller than 100.
        /// 'chance' is set by default to 100 (100% success)
        /// </summary>
        /// <param name="boostCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool BoostAttackBonus(int boostCount)
        {
            if (boostCount >= 0)
            {
                try
                {
                    this.AttackBonus.AcctualBonus += boostCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'BoostAttackBonus' function:" + exc.ToString());
                    return false;
                }

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'BoostAttackBonus' function: Wrong boostCount number");
                return false;
            }
        }

        /// <summary>
        /// Function boost ACCTUAL(not basic) defence bonus by 'boostCount'.
        /// Function acction has chance to fail if 'chance' is smaller than 100.
        /// 'chance' is set by default to 100 (100% success)
        /// </summary>
        /// <param name="boostCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool BoostDefenceBonus(int boostCount)
        {
            if (boostCount >= 0)
            {
                try
                {
                    this.DefenceBonus.AcctualBonus += boostCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'BoostDefenceBonus' function:" + exc.ToString());
                    return false;
                }

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'BoostDefenceBonus' function: Wrong boostCount number");
                return false;
            }
        }

        /// <summary>
        /// Function set ACCTUAL(not basic) attack bonus to default BASIC bonus. 
        /// </summary>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool SetAttackBonusToDefault()
        {
            try
            {
                this.AttackBonus.AcctualBonus = this.AttackBonus.BasicBonus;
            }
            catch (Exception exc)
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'SetAttackBonusToDefault' function:" + exc.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Function set ACCTUAL(not basic) defence bonus to default BASIC bonus. 
        /// </summary>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool SetDefenceBonusToDefault()
        {
            try
            {
                this.DefenceBonus.AcctualBonus = this.DefenceBonus.BasicBonus;
            }
            catch (Exception exc)
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'SetAttackBonusToDefault' function:" + exc.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Function take off item from player
        /// </summary>
        /// <returns>TRUE if succeed (item is off now) or FALSE if failed (item is already on)</returns>
        protected bool TakeOffItem()
        {
            if (this.Used == true)
            {
                this.Used = false;
                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'TakeOffItem' function: Item is already off");
                return false;
            }
        }

        /// <summary>
        /// Function take on item from player
        /// </summary>
        /// <returns>TRUE if succeed (item is on now) or FALSE if failed (item is already off)</returns>
        protected bool TakeOnItem()
        {
            if (this.Used == false)
            {
                this.Used = true;
                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'TakeOnItem' function: Item is already on");
                return false;
            }
        }

        /// <summary>
        /// Function decrease durability of item by 'durabilityDecreaseCount'.
        /// </summary>
        /// <param name="durabilityDecreaseCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool DecreaseDurability(float durabilityDecreaseCount)
        {
            if ((this.Durability > 0) && (durabilityDecreaseCount >= 0))
            {
                try
                {
                    this.Durability -= durabilityDecreaseCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'DecreaseDurability' function:" + exc.ToString());
                    return false;
                }

                if (this.Durability < 0)
                    this.Durability = 0;

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class 'ChampionItem' in 'DecreaseDurability' function: Wrong durabilityDecreaseCount number or durability is equal to 0");
                return false;
            }
        }

        /// <summary>
        /// Function increase durability of item(repair item) by 'durabilityIncreaseCount'.
        /// </summary>
        /// <param name="durabilityIncreaseCount"></param>
        /// <returns>TRUE if succeed or FALSE if failed</returns>
        protected bool IncreaseDurability(int durabilityIncreaseCount)
        {
            if ((this.Durability < 100) && (durabilityIncreaseCount >= 0))
            {
                try
                {
                    this.Durability += durabilityIncreaseCount;
                }
                catch (Exception exc)
                {
                    if (DegubInfo == true)
                        Debug.Log("Class 'ChampionItem' in 'RepairItem' function:" + exc.ToString());
                    return false;
                }

                if (this.Durability > 100)
                    this.Durability = 100;

                return true;
            }
            else
            {
                if (DegubInfo == true)
                    Debug.Log("Class: 'ChampionItem' in 'RepairItem' function: Wrong durabilityIncreaseCount number or durability is equal to 100");
                return false;
            }
        }

    }
}
