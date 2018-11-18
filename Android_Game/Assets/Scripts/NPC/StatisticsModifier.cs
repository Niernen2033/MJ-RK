using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public enum StatisticsModifierClass { LevelUP, Durability, Spell, Talent, ItemBonus }
public enum StatisticsModifierType { AddFlat, AddPercent, MinusFlat, MinusPercent }

public sealed class StatisticsModifier
{
    public double Value { get; set; }
    public StatisticsModifierType ModifierType { get; set; }
    public StatisticsModifierClass ModifierClass { get; set; }
    public EqType ModifierEqItem { get; set; }

    public StatisticsModifier(StatisticsModifierClass modifierClass, StatisticsModifierType modifierType, double value, EqType eqItemType = EqType.None)
    {
        this.ModifierClass = modifierClass;
        this.Value = value;
        this.ModifierType = modifierType;
        this.ModifierEqItem = eqItemType;
    }

    public StatisticsModifier(ref StatisticsModifier statisticsModifier)
    {
        this.Value = statisticsModifier.Value;
        this.ModifierType = statisticsModifier.ModifierType;
        this.ModifierClass = statisticsModifier.ModifierClass;
        this.ModifierEqItem = statisticsModifier.ModifierEqItem;
    }
}

