using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public enum StatisticsModifierClass { None = -1, LevelUP, Durability, Spell, Talent, ItemBonus };
public enum StatisticsModifierType { None = -1, AddFlat, AddPercent, MinusFlat, MinusPercent };

public sealed class StatisticsModifier
{
    public double Value { get; set; }
    public StatisticsModifierType ModifierType { get; set; }
    public StatisticsModifierClass ModifierClass { get; set; }
    public EqType ModifierEqItem { get; set; }

    public StatisticsModifier()
    {
        this.ModifierClass = StatisticsModifierClass.None;
        this.Value = 0;
        this.ModifierType = StatisticsModifierType.None;
        this.ModifierEqItem = EqType.None;
    }

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

