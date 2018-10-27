using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobals
{
    public enum SceneIndex
    {
        MianMenuScene = 0,
        CityScene = 1
    }

    public static bool IsDebugState { get; } = true;
}
