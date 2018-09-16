﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum MenuType { MainMenu, OptionsMenu, LoadGameMenu, CreateNewGameMenu }

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

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loadGameMenu;
    public GameObject createNewGameMenu;
    public Button continueGameButton;

    public event EventHandler<SavesEventArgs> SavesIsSetUp;

    protected void OnSavesIsSetUp(SavesEventArgs e)
    {
        this.SavesIsSetUp?.Invoke(this, e);
    }

    public void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.saveMembers = new List<SaveMember>();


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

    private void SetUpSaves()
    {
        string continueSavePath = string.Empty;

        //we dont chave folder to save
        if (!Directory.Exists(Save.SavePaths.GlobalSavesPath))
        {
            //we dont folder to save so we can only create new game folder
            Directory.CreateDirectory(Save.SavePaths.GlobalSavesPath);

            this.continueGameButton.interactable = false;
            this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);
        }
        //we chave folder to save
        else
        {
            List<string> allSavesFolderDirectories = Directory.GetDirectories(Save.SavePaths.GlobalSavesPath).ToList();

            if (allSavesFolderDirectories.Count == 0)
            {
                //we dont have game saves
                this.continueGameButton.interactable = false;
                this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);
            }
            else
            {
                this.continueGameButton.interactable = false;
                this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.6352941f, 0.6431373f, 0.5411765f);

                //we have game saves
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

                            //check if we have active save
                            if (Save.Instance.IsAcctualSave)
                            {
                                this.continueGameButton.interactable = true;
                                this.continueGameButton.GetComponentInChildren<Text>().color = new Color(0.9372549f, 1f, 0f);
                                continueSavePath = savePath;
                            }

                            this.saveMembers.Add(new SaveMember());
                        }
                    }
                }
            }
        }

        this.OnSavesIsSetUp(new SavesEventArgs(this.saveMembers));

        if (continueSavePath != string.Empty)
        {
            if (!Save.Instance.Load(continueSavePath))
            {
                Debug.Log("Class: 'Menu' in 'SetUpSavesAndScenes' function: Cannot load save file in folder" + continueSavePath);
            }
        }
    }

    private void ChangeAcctualMenu()
    {
        switch(this.menuState.Current)
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

    public void OnMainMenu()
    {
        this.menuState.Last = this.menuState.Current;
        this.menuState.Current = MenuType.MainMenu;
        this.ChangeAcctualMenu();
    }

    public void OnLoadGameMenu()
    {
        this.menuState.Last = this.menuState.Current;
        this.menuState.Current = MenuType.LoadGameMenu;
        this.ChangeAcctualMenu();
    }

    public void OnCreateNewGameMenu()
    {
        this.menuState.Last = this.menuState.Current;
        this.menuState.Current = MenuType.CreateNewGameMenu;
        this.ChangeAcctualMenu();
    }

    public void OnOptionsMenu()
    {
        this.menuState.Last = this.menuState.Current;
        this.menuState.Current = MenuType.OptionsMenu;
        this.ChangeAcctualMenu();
    }

    public void OnBackToLastMenu()
    {
        MenuType last_menu_remember = this.menuState.Last;
        this.menuState.Last = this.menuState.Current;
        this.menuState.Current = last_menu_remember;
        this.ChangeAcctualMenu();
    }

    public void OnOffAudio()
    {
        if (this.audioSource.mute == true)
            this.audioSource.mute = false;
        else
            this.audioSource.mute = true;
    }

    public void OnPlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


}
