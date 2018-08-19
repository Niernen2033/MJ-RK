using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;
using System;

public class XmlManager<T>
{
    //ON or OFF Debug informations
    private static bool DegubInfo = false;

    //Variables***********************
    //Type of class with we will serializable (set by default to type of generic class 'T')
    public Type Type;

    //Constructors**************************
    public XmlManager()
    {
        Type = typeof(T);
    }
    public XmlManager(Type type)
    {
        Type = type;
    }

    //Functions***************************

    /// <summary>
    /// Function load content to serializable class from xml file named by 'path'. Return TRUE and 'instance' of this class
    /// </summary>
    /// <param name="path"></param>
    /// <param name="instance"></param>
    /// <returns>TRUE if succeed or FALSE if failed</returns>
    public bool Load(string path, out T instance)
    {
        try
        {
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return true;
        }
        catch(Exception exc)
        {
            if (DegubInfo == true)
                Debug.Log("Class 'XmlManager' in 'Load' function:" + exc.ToString());
            instance = default(T);
            return false;
        }
    }

    /// <summary>
    /// Function save content from serializable class 'obj' to xml file named by 'path'.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    /// <returns>TRUE if succeed or FALSE if failed</returns>
    public bool Save(object obj, string path)
    {
        try
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
            return true;
        }
        catch(Exception exc)
        {
            if (DegubInfo == true)
                Debug.Log("Class 'XmlManager' in 'Save' function:" + exc.ToString());
            return false;
        }
    }
}
