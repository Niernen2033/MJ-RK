using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuType { MainMenu, OptionsMenu, LoadGameMenu, CreateNewGameMenu }

public class Menu : MonoBehaviour
{
    private AudioSource audioSource;
    private MenuType currentMenuState;
    private MenuType lastMenuState;
    //private List<LoadButtonPrefab> loadButtonPrefab;
    private List<Save> allSaves;
    private int continueSaveIndex;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loadGameMenu;
    public GameObject createNewGameMenu;
    public Button continueGameButton;

    public void Awake()
    {
        this.continueSaveIndex = -1;
        this.audioSource = GetComponent<AudioSource>();
        //this.loadButtonPrefab = new List<LoadButtonPrefab>();
        this.allSaves = new List<Save>();

        //temporary*******************

        //temporary*******************
    }

    public void Start()
    {
        this.SetUpSavesAndScenes();
    }

    private void SetUpSavesAndScenes()
    {
        //we dont chave folder to save
        if (!Directory.Exists(Save.SavePaths.GlobalSavesPath))
        {
            //we dont folder to save so we can only create new game folder
            Directory.CreateDirectory(Save.SavePaths.GlobalSavesPath);

            this.continueGameButton.enabled = false;
        }
        //we chave folder to save
        else
        {
            List<string> allSavesFolderDirectories = Directory.GetDirectories(Save.SavePaths.GlobalSavesPath).ToList();

            if (allSavesFolderDirectories.Count == 0)
            {
                //we dont have game saves
                this.continueGameButton.enabled = false;
            }
            else
            {
                this.continueGameButton.enabled = false;

                //we have game saves
                foreach (string saveFile in allSavesFolderDirectories)
                {
                    List<string> filesPathInSaveFile = Directory.GetFiles(saveFile).ToList();
                    foreach (string savePath in filesPathInSaveFile)
                    {
                        if (savePath.Contains(".xml") && !savePath.Contains(".meta"))
                        {
                            if (!Save.Instance.LoadFile(savePath))
                            {
                                Debug.Log("Class: 'Menu' in 'SetUpSavesAndScenes' function: Cannot load save file in folder" + savePath);
                                continue;
                            }

                            //check if we have active save
                            if (Save.Instance.IsAcctualSave)
                            {
                                this.continueGameButton.enabled = true;
                                this.continueSaveIndex = this.allSaves.Count;
                            }

                            this.allSaves.Add(Save.Instance);
                        }
                    }
                }
            }
        }

        this.lastMenuState = MenuType.MainMenu;
        this.currentMenuState = MenuType.MainMenu;
        this.ChangeAcctualMenu();
    }

    private void ChangeAcctualMenu()
    {
        switch(this.currentMenuState)
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
        this.lastMenuState = this.currentMenuState;
        this.currentMenuState = MenuType.MainMenu;
        this.ChangeAcctualMenu();
    }

    public void OnLoadGameMenu()
    {
        this.lastMenuState = this.currentMenuState;
        this.currentMenuState = MenuType.LoadGameMenu;
        this.ChangeAcctualMenu();
    }

    public void OnCreateNewGameMenu()
    {
        this.lastMenuState = this.currentMenuState;
        this.currentMenuState = MenuType.CreateNewGameMenu;
        this.ChangeAcctualMenu();
    }

    public void OnOptionsMenu()
    {
        this.lastMenuState = this.currentMenuState;
        this.currentMenuState = MenuType.OptionsMenu;
        this.ChangeAcctualMenu();
    }

    public void OnBackToLastMenu()
    {
        MenuType last_menu_remember = this.lastMenuState;
        this.lastMenuState = this.currentMenuState;
        this.currentMenuState = last_menu_remember;
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
