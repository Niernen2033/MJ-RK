using System.Collections;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class GameGlobals
{
    public enum SceneIndex
    {
        None = -1,
        MianMenuScene = 0,
        CityScene = 1,
        DungeonScene = 2,
    }

    public static string CalculateIndyvidualHash(string data)
    {
        string result = string.Empty;

        try
        {
            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {

                byte[] byteResult = md5.ComputeHash(dataToEncrypt);
                result = Encoding.ASCII.GetString(byteResult);
            }
        }
        catch (Exception exc)
        {
            Debug.Log("Class 'GameGlobals' in 'CalculateIndyvidualHash' function: Cannot create hash " + exc.ToString());
            System.Random myRandom = new System.Random();
            result = (myRandom.Next(0, int.MaxValue - 1)).ToString();
        }

        return result;
    }

    public static bool IsDebugState { get; } = true;
}
