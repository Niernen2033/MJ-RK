using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;
using System;

public static class XmlManager
{
    //Functions***************************

    /// <summary>
    /// Function load content to serializable class from xml file named by 'name'. Return TRUE and 'instance' of this class
    /// </summary>
    /// <param name="path"></param>
    /// <param name="instance"></param>
    /// <returns>TRUE if succeed or FALSE if failed</returns>
    public static bool Load<T>(string name, out T instance)
    {
        try
        {
            using (TextReader reader = new StreamReader(name))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                instance = (T)xml.Deserialize(reader);
            }
            return true;
        }
        catch(Exception exc)
        {
            Debug.Log("Class 'XmlManager' in 'Load' function:" + exc.ToString());
            instance = default(T);
            return false;
        }
    }

    /// <summary>
    /// Function save content from serializable class 'obj' to xml file named by 'name'.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    /// <returns>TRUE if succeed or FALSE if failed</returns>
    public static bool Save<T>(T obj, string name)
    {
        try
        {
            using (TextWriter writer = new StreamWriter(name))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(writer, obj);
            }
            return true;
        }
        catch(Exception exc)
        {
            Debug.Log("Class 'XmlManager' in 'Save' function:" + exc.ToString());
            return false;
        }
    }
}
