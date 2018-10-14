using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace MainMenuScene
{
    public enum MenuType { MainMenu, OptionsMenu, LoadGameMenu, CreateNewGameMenu }
    public delegate void InvokePlayGameCallback();

    public class Menu : MonoBehaviour
    {
        private struct MenuState
        {
            public MenuType Current;
            public MenuType Last;
        };
        private AudioSource audioSource;
        private MenuState menuState;
        private List<SaveMember> saveMembers;
        private bool ifSavesNeedsToBeReloaded;

        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject loadGameMenu;
        public GameObject createNewGameMenu;
        public Button continueGameButton;

        public event EventHandler<SavesEventArgs> LoadGameMenuInvoked;

        private void SetUpSaves()
        {
            string continueSavePath = string.Empty;

            //we dont chave global folder to save
            if (!Directory.Exists(Save.Paths.GlobalFolder))
            {
                //we dont have global folder to save so we can only create it
                Directory.CreateDirectory(Save.Paths.GlobalFolder);

                this.continueGameButton.interactable = false;
                this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);
            }
            //we chave global folder to save
            else
            {
                List<string> allSavesFolderDirectories = Directory.GetDirectories(Save.Paths.GlobalFolder).ToList();

                //we dont have game profiles
                if (allSavesFolderDirectories.Count == 0)
                {
                    this.continueGameButton.interactable = false;
                    this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);
                }
                //we have game profiles
                else
                {
                    this.continueGameButton.interactable = false;
                    this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);

                    //check if we have game saves
                    foreach (string saveFile in allSavesFolderDirectories)
                    {
                        List<string> filesPathInSaveFile = Directory.GetFiles(saveFile).ToList();

                        foreach (string savePath in filesPathInSaveFile)
                        {
                            if (savePath.Contains(".xml") && !savePath.Contains(".meta"))
                            {
                                if (!Save.Instance.Load(savePath))
                                {
                                    Debug.Log("Class: 'Menu' in 'SetUpSavesAndScenes' function: Cannot load save file in folder" + savePath);
                                    continue;
                                }

                                //check if this save is active save
                                if (Save.Instance.IsAcctual)
                                {
                                    this.continueGameButton.interactable = true;
                                    this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.9372549f, 1f, 0f);
                                    continueSavePath = savePath;
                                }
                                this.saveMembers.Add(new SaveMember(Save.Instance.Texture, Save.Instance.Name, Save.Instance.Path));
                            }
                        }
                    }
                }
            }

            if (continueSavePath != string.Empty)
            {
                Save.Paths.AcctualSave = continueSavePath;
            }
        }

        private void ChangeAcctualMenu()
        {
            switch (this.menuState.Current)
            {
                case (MenuType.MainMenu):
                    {
                        this.mainMenu.SetActive(true);
                        this.optionsMenu.SetActive(false);
                        this.loadGameMenu.SetActive(false);
                        this.createNewGameMenu.SetActive(false);
                        break;
                    }
                case (MenuType.OptionsMenu):
                    {
                        this.mainMenu.SetActive(false);
                        this.optionsMenu.SetActive(true);
                        this.loadGameMenu.SetActive(false);
                        this.createNewGameMenu.SetActive(false);
                        break;
                    }
                case (MenuType.LoadGameMenu):
                    {
                        this.mainMenu.SetActive(false);
                        this.optionsMenu.SetActive(false);
                        this.loadGameMenu.SetActive(true);
                        this.createNewGameMenu.SetActive(false);
                        break;
                    }
                case (MenuType.CreateNewGameMenu):
                    {
                        this.mainMenu.SetActive(false);
                        this.optionsMenu.SetActive(false);
                        this.loadGameMenu.SetActive(false);
                        this.createNewGameMenu.SetActive(true);
                        break;
                    }
            }
        }

        protected void OnLoadGameMenuInvoked(SavesEventArgs e)
        {
            this.LoadGameMenuInvoked?.Invoke(this, e);
        }

        public void Awake()
        {
            this.audioSource = GetComponent<AudioSource>();
            this.saveMembers = new List<SaveMember>();
            this.ifSavesNeedsToBeReloaded = true;

            this.menuState.Current = MenuType.MainMenu;
            this.menuState.Last = MenuType.MainMenu;
            this.ChangeAcctualMenu();
            //temporary*******************

            //temporary*******************
        }

        public void Start()
        {
            this.SetUpSaves();
        }

        public void InvokeMainMenu()
        {
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.MainMenu;
            this.ChangeAcctualMenu();
        }

        public void InvokeLoadGameMenu()
        {
            Debug.Log(saveMembers[0].SaveName);
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.LoadGameMenu;
            this.ChangeAcctualMenu();
            Debug.Log(saveMembers[0].SavePath);

            this.OnLoadGameMenuInvoked(new SavesEventArgs(this.saveMembers, this.ifSavesNeedsToBeReloaded));
            if (this.ifSavesNeedsToBeReloaded)
            {
                this.ifSavesNeedsToBeReloaded = false;
            }
        }

        public void InvokeCreateNewGameMenu()
        {
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.CreateNewGameMenu;
            this.ChangeAcctualMenu();
        }

        public void InvokeOptionsMenu()
        {
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.OptionsMenu;
            this.ChangeAcctualMenu();
        }

        public void InvokeBackToLastMenu()
        {
            MenuType last_menu_remember = this.menuState.Last;
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = last_menu_remember;
            this.ChangeAcctualMenu();
        }

        public void InvokeOffAudio()
        {
            if (this.audioSource.mute == true)
                this.audioSource.mute = false;
            else
                this.audioSource.mute = true;
        }

        public void InvokePlayGame()
        {
            if (GameGlobals.IsDebugState)
            {
                Save.Paths.AcctualSave = @"D:\Repos\MJ-RK\Android_Game\Assets\Saves\testy\tt.xml";
            }

            if (!File.Exists(Save.Paths.AcctualSave))
            {
                Debug.Log("Class 'Menu' in 'Start' function: File doesn't exist");
            }
            else
            {
                Save.Instance.Load(Save.Paths.AcctualSave);
            }
            SceneManager.LoadScene((int)GameGlobals.SceneIndex.CityScene);
        }

        public void InvokeQuitGame()
        {
            Debug.Log("QUIT");

            Application.Quit();
        }

    }
}