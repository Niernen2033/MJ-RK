using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScene
{
    public class LoadGameMenu : MonoBehaviour
    {
        public GameObject loadButtonList;
        public Button loadButtonPrefab;
        private List<Button> saveButtons;
        private InvokePlayGameCallback invokePlayGameCallback;

        public void Awake()
        {
            this.saveButtons = new List<Button>();
            this.gameObject.GetComponentInParent<Menu>().LoadGameMenuInvoked += LoadGameMenu_LoadGameMenuInvoked;
            this.invokePlayGameCallback = GetComponentInParent<Menu>().InvokePlayGame;
        }

        private void LoadGameMenu_LoadGameMenuInvoked(object sender, SavesEventArgs e)
        {
            if (e.IfSavesNeedsToBeReloaded)
            {
                foreach (Button saveButtons in this.saveButtons)
                {
                    Destroy(saveButtons.gameObject);
                }

                if (e.IsHaveSaves)
                {
                    foreach (SaveMember saveMember in e.SaveMembers)
                    {
                        Button saveMemberButtonClone = Instantiate(this.loadButtonPrefab, loadButtonList.transform).GetComponent<Button>();
                        this.saveButtons.Add(saveMemberButtonClone);
                        this.saveButtons[this.saveButtons.Count - 1].gameObject.SetActive(true);
                        this.saveButtons[this.saveButtons.Count - 1].GetComponentInChildren<Text>().text = saveMember.SavePath;
                        this.saveButtons[this.saveButtons.Count - 1].onClick.AddListener(() => this.OnButtonClick(saveMember.SavePath));
                    }
                }
            }
        }

        private void OnButtonClick(string path)
        {
            Save.Paths.AcctualSave = path;
            this.invokePlayGameCallback();
        }
    }
}
