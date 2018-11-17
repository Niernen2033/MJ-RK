﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatisticsModifierClass { LevelUP, Durability, Spell, Talent }
public enum StatisticsModifierType { AddFlat, AddPercent, MinusFlat, MinusPercent }

public sealed class StatisticsModifier
{
    public double Value { get; set; }
    public StatisticsModifierType modifierType { get; set; }
    public StatisticsModifierClass modifierClass { get; set; }

    public StatisticsModifier(StatisticsModifierClass modifierClass, StatisticsModifierType modifierType, double value)
    {
        this.modifierClass = modifierClass;
        this.Value = value;
        this.modifierType = modifierType;
    }

    public StatisticsModifier(ref StatisticsModifier statisticsModifier)
    {
        this.Value = statisticsModifier.Value;
        this.modifierType = statisticsModifier.modifierType;
        this.modifierClass = statisticsModifier.modifierClass;
    }
}

