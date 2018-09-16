using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject loadButtonList;
    public Button loadButtonPrefab;

    public void Awake()
    {
        this.gameObject.GetComponentInParent<Menu>().SavesIsSetUp += LoadGameMenu_SavesIsSetUp;
    }

    private void LoadGameMenu_SavesIsSetUp(object sender, SavesEventArgs e)
    {
        if(e.HaveSaves)
        {
            foreach(SaveMember saveMember in e.SaveMembers)
            {
                Button saveMemberClone = Instantiate(this.loadButtonPrefab, loadButtonList.transform).GetComponent<Button>();
                saveMemberClone.gameObject.SetActive(true);
            }
        }
    }
}
