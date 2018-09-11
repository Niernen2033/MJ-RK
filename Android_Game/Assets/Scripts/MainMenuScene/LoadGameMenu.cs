using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject loadButtonList;
    public Button loadButtonPrefab;

    public void Start()
    {
        /*
        for (int i = 0; i < 2; i++)
        {
            LoadButtonPrefab loadButtonPrefabClone = Instantiate(loadButtonPrefab, loadButtonList.transform).GetComponent<LoadButtonPrefab>();
            loadButtonPrefabClone.gameObject.SetActive(true);
            loadButtonPrefabClone.Text.text = "aa";
        }
        */
    }
}
