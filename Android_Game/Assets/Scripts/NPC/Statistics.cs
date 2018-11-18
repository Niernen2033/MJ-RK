using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using Items;

using UnityEngine;

public class StatisticsEventArgs : EventArgs
{
    public readonly StatisticsModifier Modifier;

    public StatisticsEventArgs(StatisticsModifier statisticsModifier)
    {
        this.Modifier = statisticsModifier;
    }

    public StatisticsEventArgs()
    {
        this.Modifier = null;
    }
}

public sealed class Statistics
{
    //Basic item bonus
    public double Basic { get; set; }

    [XmlElement(ElementName = "StatisticsModifiersItem")]
    public List<StatisticsModifier> StatisticsModifiers { get; set; }

    [XmlIgnore]
    //Acctual bonus of item
    public double Acctual { get; private set; }

    public event EventHandler<StatisticsEventArgs> AddedModifer;
    public event EventHandler<StatisticsEventArgs> RemovedModifer;
    public event EventHandler<StatisticsEventArgs> RemovedAllModifers;

    public Statistics(double basicBonus)
    {
        this.Basic = basicBonus;
        this.Acctual = basicBonus;

        this.StatisticsModifiers = new List<StatisticsModifier>();
    }

    public Statistics()
    {
        this.Basic = 0;
        this.Acctual = 0;

        this.StatisticsModifiers = new List<StatisticsModifier>();
    }

    public Statistics(ref Statistics itemBonus)
    {
        this.Basic = itemBonus.Basic;
        this.Acctual = itemBonus.Acctual;

        this.StatisticsModifiers = new List<StatisticsModifier>();
    }

    public void ChangeAcctualValue(double acctualBonus)
    {
        this.Acctual = acctualBonus;
        //this.CalculateAcctualStatistics();
    }

    public void ChangeBasicValue(double basicBonus)
    {
        this.Basic = basicBonus;
        this.CalculateAcctualStatistics();
    }

    public bool AddModifier(StatisticsModifier modifier)
    {
        try
        {
            this.StatisticsModifiers.Add(modifier);
            if (!this.CalculateAcctualStatistics())
            {
                this.StatisticsModifiers.Remove(modifier);
                Debug.Log("Class 'Statistics' in 'AddModifier' function: Error in calculating modifires");

                return false;
            }

            this.OnAddedModifer(new StatisticsEventArgs(new StatisticsModifier(ref modifier)));
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'AddModifier' function:" + exc.ToString());
            return false;
        }
    }

    public bool RemoveModifier(StatisticsModifier modifier)
    {
        try
        {
            if (this.StatisticsModifiers.Remove(modifier))
            {
                if (!this.CalculateAcctualStatistics())
                {
                    this.StatisticsModifiers.Add(modifier);
                    Debug.Log("Class 'Statistics' in 'RemoveModifier' function: Error in calculating modifires");

                    return false;
                }

                this.OnRemoveModifer(new StatisticsEventArgs(new StatisticsModifier(ref modifier)));
                return true;
            }
            else
            {
                Debug.Log("Class 'Statistics' in 'RemoveModifier' function: Error in removing modifire");

                return false;
            }

        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveModifier' function:" + exc.ToString());
            return false;
        }
    }

    public bool RemoveAllModifiers()
    {
        try
        {
            this.StatisticsModifiers.Clear();
            this.OnRemovedAllModifers(new StatisticsEventArgs());
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveAllModifiers' function:" + exc.ToString());

            return false;
        }
    }

    public bool RemoveAllModifiers(StatisticsModifierType modifierType)
    {
        try
        {
            this.StatisticsModifiers.RemoveAll(item => (item.ModifierType == modifierType));
            this.OnRemovedAllModifers(new StatisticsEventArgs());
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveAllModifiers|Type' function: " + exc.ToString());

            return false;
        }
    }

    public bool RemoveAllModifiers(StatisticsModifierClass modifierClass)
    {
        try
        {
            this.StatisticsModifiers.RemoveAll(item => (item.ModifierClass == modifierClass));
            this.OnRemovedAllModifers(new StatisticsEventArgs());
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveAllModifiers|Class' function: " + exc.ToString());

            return false;
        }
    }

    public bool RemoveAllModifiers(EqType modifierEqItem)
    {
        if(modifierEqItem == EqType.None)
        {
            return false;
        }

        try
        {
            this.StatisticsModifiers.RemoveAll(item => (item.ModifierEqItem == modifierEqItem));
            this.OnRemovedAllModifers(new StatisticsEventArgs());
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveAllModifiers|Class' function: " + exc.ToString());

            return false;
        }
    }

    public bool RemoveAllModifiers(double value)
    {
        try
        {
            this.StatisticsModifiers.RemoveAll(item => (item.Value == value));
            this.OnRemovedAllModifers(new StatisticsEventArgs());
            return true;
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'Statistics' in 'RemoveAllModifiers|Value' function: " + exc.ToString());

            return false;
        }
    }

    public bool CalculateAcctualStatistics()
    {
        double lastAcctualValue = this.Acctual;
        this.Acctual = this.Basic;

        foreach (StatisticsModifier modifier in this.StatisticsModifiers)
        {
            if (modifier.ModifierType == StatisticsModifierType.AddFlat)
            {
                try
                {
                    this.Acctual += modifier.Value;
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'Statistics' in 'CalculateAcctualStatistics' function:" + exc.ToString());

                    this.Acctual = lastAcctualValue;
                    return false;
                }
            }
            else if (modifier.ModifierType == StatisticsModifierType.AddPercent)
            {
                try
                {
                    this.Acctual += this.Basic * (1 + (modifier.Value / 100));
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'Statistics' in 'CalculateAcctualStatistics' function:" + exc.ToString());

                    this.Acctual = lastAcctualValue;
                    return false;
                }
            }
            else if (modifier.ModifierType == StatisticsModifierType.MinusFlat)
            {
                try
                {
                    this.Acctual -= modifier.Value;
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'Statistics' in 'CalculateAcctualStatistics' function:" + exc.ToString());

                    this.Acctual = lastAcctualValue;
                    return false;
                }
            }
            else if (modifier.ModifierType == StatisticsModifierType.MinusPercent)
            {
                try
                {
                    this.Acctual -= this.Basic * (modifier.Value / 100);
                }
                catch (Exception exc)
                {
                    Debug.Log("Class 'Statistics' in 'CalculateAcctualStatistics' function:" + exc.ToString());

                    this.Acctual = lastAcctualValue;
                    return false;
                }
            }
        }

        this.Acctual = (float)Math.Round(this.Acctual, 2, MidpointRounding.AwayFromZero);
        if (this.Acctual < 0)
            this.Acctual = 0;

        return true;
    }

    //EVENTS***************************************************
    private void OnAddedModifer(StatisticsEventArgs e)
    {
        this.AddedModifer?.Invoke(this, e);
    }

    private void OnRemoveModifer(StatisticsEventArgs e)
    {
        this.RemovedModifer?.Invoke(this, e);
    }

    private void OnRemovedAllModifers(StatisticsEventArgs e)
    {
        this.RemovedAllModifers?.Invoke(this, e);
    }

}

