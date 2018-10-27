using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

using SaveLoad;

namespace MainMenuScene
{
    public enum MenuType { MainMenu, OptionsMenu, LoadGameMenu }
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
        public Button continueGameButton;

        public event EventHandler<SavesEventArgs> LoadGameMenuInvoked;

        private void SetUpSaves()
        {
            //we dont chave global folder to save
            if (!Directory.Exists(SaveInfo.Paths.GlobalFolder))
            {
                //we dont have global folder to save so we can only create it
                Directory.CreateDirectory(SaveInfo.Paths.GlobalFolder);

                this.continueGameButton.interactable = false;
                this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);
            }
            //we chave global folder to save
            else
            {
                List<string> allSavesFolderDirectories = Directory.GetDirectories(SaveInfo.Paths.GlobalFolder).ToList();

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
                                if (!GameSave.Instance.Load(savePath))
                                {
                                    Debug.Log("Class: 'Menu' in 'SetUpSavesAndScenes' function: Cannot load save file in folder" + savePath);
                                    continue;
                                }

                                this.saveMembers.Add(new SaveMember(GameSave.Instance.Texture, GameSave.Instance.Name, savePath));
                            }
                        }
                    }

                    //check if this save is active save
                    if (ProfileSave.Instance.AcctualSavePath != null)
                    {
                        if (File.Exists(ProfileSave.Instance.AcctualSavePath))
                        {
                            this.continueGameButton.interactable = true;
                            this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.9372549f, 1f, 0f);
                        }
                    }
                }
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
                        break;
                    }
                case (MenuType.OptionsMenu):
                    {
                        this.mainMenu.SetActive(false);
                        this.optionsMenu.SetActive(true);
                        this.loadGameMenu.SetActive(false);
                        break;
                    }
                case (MenuType.LoadGameMenu):
                    {
                        this.mainMenu.SetActive(false);
                        this.optionsMenu.SetActive(false);
                        this.loadGameMenu.SetActive(true);
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

            this.LoadSceneData();

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

        private void LoadSceneData()
        {
            if (!ProfileSave.Instance.Load())
            {
                Debug.Log("Trying to create profile save");
                if (!ProfileSave.Instance.Update())
                {
                    Debug.Log("Cannot create profile save");
                }
            }
            else
            {
                this.SetVolume(ProfileSave.Instance.MenuVolume);
                this.optionsMenu.GetComponentInChildren<Slider>().value = ProfileSave.Instance.MenuVolume;
            }
        }

        public void InvokeMainMenu()
        {
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.MainMenu;
            this.ChangeAcctualMenu();
        }

        public void InvokeLoadGameMenu()
        {
            this.menuState.Last = this.menuState.Current;
            this.menuState.Current = MenuType.LoadGameMenu;
            this.ChangeAcctualMenu();

            this.OnLoadGameMenuInvoked(new SavesEventArgs(this.saveMembers, this.ifSavesNeedsToBeReloaded));
            if (this.ifSavesNeedsToBeReloaded)
            {
                this.ifSavesNeedsToBeReloaded = false;
            }
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

        public void InvokeNewGame()
        {
            SceneManager.LoadScene((int)GameGlobals.SceneIndex.NewGameScene);
        }

        public void InvokePlayGame()
        {
            if (GameGlobals.IsDebugState)
            {
                ProfileSave.Instance.AcctualSavePath = @"C:\Users\Michal\Documents\Unity\MJ-RK\Android_Game\Assets\Saves\testy\tt.xml";
            }
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);
            SceneManager.LoadScene((int)GameGlobals.SceneIndex.CityScene);
        }

        public void InvokeQuitGame()
        {
            Debug.Log("QUIT");

            Application.Quit();
        }

        public void SetVolume(float vol)
        {
            ProfileSave.Instance.MenuVolume = vol;
            this.audioSource.volume = ProfileSave.Instance.MenuVolume;
            ProfileSave.Instance.Update();
        }

    }
}