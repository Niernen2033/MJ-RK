using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject loadButtonList;
    public Button loadButtonPrefab;
    private List<Button> saveButtons;

    public void Awake()
    {
        this.saveButtons = new List<Button>();
        this.gameObject.GetComponentInParent<Menu>().LoadGameMenuInvoked += LoadGameMenu_LoadGameMenuInvoked;
    }

    private void LoadGameMenu_LoadGameMenuInvoked(object sender, SavesEventArgs e)
    {
        if (e.IfSavesNeedsToBeReloaded)
        {
            foreach(Button saveButtons in this.saveButtons)
            {
                Destroy(saveButtons.gameObject);
            }

            if (e.IsHaveSaves)
            {
                foreach (SaveMember saveMember in e.SaveMembers)
                {
                    Button saveMemberClone = Instantiate(this.loadButtonPrefab, loadButtonList.transform).GetComponent<Button>();
                    this.saveButtons.Add(saveMemberClone);
                    this.saveButtons[this.saveButtons.Count - 1].gameObject.SetActive(true);
                }
            }
        }
    }
}
