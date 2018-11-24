using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prefabs.Options
{
    public class InGameOptions : MonoBehaviour
    {
        public GameObject gameOptions;
        public bool IsOpen { get; private set; }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            this.IsOpen = false;
        }

        public void OpenOptions()
        {
            this.gameObject.SetActive(true);
            this.IsOpen = true;

        }

        public void CloseOptions()
        {
            this.IsOpen = false;
            this.gameObject.SetActive(false);
        }

        public void BackToMainMenu()
        {
            GameSave.Instance.Update();
            SceneManager.LoadScene((int)GameGlobals.SceneIndex.MianMenuScene);
        }

        public void CloseGame()
        {
            GameSave.Instance.Update();
            Application.Quit();
        }

    }
}
