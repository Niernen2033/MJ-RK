using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class City : MonoBehaviour
{
    Player player;

	// Use this for initialization
	public void Awake()
    {
        if(!File.Exists(Save.Paths.AcctualSave))
        {
            Debug.Log("Class 'City' in 'Start' function: File doesn't exist");
        }
        else
        {
            Save.Instance.Load(Save.Paths.AcctualSave);
        }
	}

    public void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void Test()
    {
        SceneManager.LoadScene((int)GameGlobals.SceneIndex.MianMenuScene);
    }
}
