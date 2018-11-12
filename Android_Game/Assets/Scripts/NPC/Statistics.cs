using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

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

    private readonly List<StatisticsModifier> statisticsModifiers;

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

        this.statisticsModifiers = new List<StatisticsModifier>();
    }

    public Statistics()
    {
        this.Basic = 0;
        this.Acctual = 0;

        this.statisticsModifiers = new List<StatisticsModifier>();
    }

    public Statistics(ref Statistics itemBonus)
    {
        this.Basic = itemBonus.Basic;
        this.Acctual = itemBonus.Acctual;

        this.statisticsModifiers = new List<StatisticsModifier>();
    }

    public void ChangeAcctualValue(double acctualBonus)
    {
        this.Acctual = acctualBonus;
        //this.CalculateAcctualStatistics();
    }

    public bool AddModifier(StatisticsModifier modifier)
    {
        try
        {
            this.statisticsModifiers.Add(modifier);
            if (!this.CalculateAcctualStatistics())
            {
                this.statisticsModifiers.Remove(modifier);
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
            if (this.statisticsModifiers.Remove(modifier))
            {
                if (!this.CalculateAcctualStatistics())
                {
                    this.statisticsModifiers.Add(modifier);
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
            this.statisticsModifiers.Clear();
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
            this.statisticsModifiers.RemoveAll(item => (item.modifierType == modifierType));
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
            this.statisticsModifiers.RemoveAll(item => (item.modifierClass == modifierClass));
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
            this.statisticsModifiers.RemoveAll(item => (item.Value == value));
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

        foreach (StatisticsModifier modifier in this.statisticsModifiers)
        {
            if (modifier.modifierType == StatisticsModifierType.AddFlat)
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
            else if (modifier.modifierType == StatisticsModifierType.AddPercent)
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
            else if (modifier.modifierType == StatisticsModifierType.MinusFlat)
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
            else if (modifier.modifierType == StatisticsModifierType.MinusPercent)
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

