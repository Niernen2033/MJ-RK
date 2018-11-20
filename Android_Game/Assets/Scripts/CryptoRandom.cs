using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using UnityEngine;

public static class CryptoRandom
{
    public static int Next()
    {
        uint result = 0;
        byte[] cryptoResult = new byte[4];

        using (RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider())
        {
            rNG.GetBytes(cryptoResult);
        }

        try
        {
            result = BitConverter.ToUInt32(cryptoResult, 0);
        }
        catch (Exception exc)
        {
            Debug.Log("CryptoRandom error : " + exc.Message);
        }

        return (int)result;
    }

    public static int Next(int min, int max)
    {
        uint result = 0;
        byte[] cryptoResult = new byte[4];

        using (RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider())
        {
            rNG.GetBytes(cryptoResult);
        }

        try
        {
            result = BitConverter.ToUInt32(cryptoResult, 0);
            result = (uint)((result % (max - min + 1)) + min);
        }
        catch (Exception exc)
        {
            Debug.Log("CryptoRandom error : " + exc.Message);
        }

        return (int)result;
    }

    public static double NextDouble(double min, double max)
    {
        double result = 0;
        byte[] cryptoResult = new byte[4];

        using (RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider())
        {
            rNG.GetBytes(cryptoResult);
        }

        try
        {
            uint tempresult = BitConverter.ToUInt32(cryptoResult, 0);
            result = tempresult / (double)uint.MaxValue;
            result = (result * (max - min)) + min;
        }
        catch (Exception exc)
        {
            Debug.Log("CryptoRandom error : " + exc.Message);
        }

        return result;
    }

    public static double NextDouble()
    {
        double result = 0;
        byte[] cryptoResult = new byte[4];

        using (RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider())
        {
            rNG.GetBytes(cryptoResult);
        }

        try
        {
            uint tempresult = BitConverter.ToUInt32(cryptoResult, 0);
            result = tempresult / (double)uint.MaxValue;
        }
        catch (Exception exc)
        {
            Debug.Log("CryptoRandom error : " + exc.Message);
        }

        return result;
    }
}
