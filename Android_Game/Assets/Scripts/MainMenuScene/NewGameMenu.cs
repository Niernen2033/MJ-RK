using NPC;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScene
{
    public class NewGameMenu : MonoBehaviour
    {
        public InputField inputField;

        private InvokePlayGameCallback invokePlayGameCallback;
        private void Awake()
        {
            this.invokePlayGameCallback = this.gameObject.GetComponentInParent<Menu>().InvokePlayGame;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateMage()
        {
            string name = this.inputField.text;
            GameSave.Instance.CreateNewSave(name);
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);

            ChampionGenerator championGenerator = new ChampionGenerator();
            Player player = championGenerator.GenerateNewPlayer(ChampionClass.Mage, name);

            GameSave.Instance.Player = player;
            GameSave.Instance.Update();
            this.invokePlayGameCallback();
        }

        public void CreateRange()
        {
            string name = this.inputField.text;
            GameSave.Instance.CreateNewSave(name);
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);

            ChampionGenerator championGenerator = new ChampionGenerator();
            Player player = championGenerator.GenerateNewPlayer(ChampionClass.Range, name);

            GameSave.Instance.Player = player;
            GameSave.Instance.Update();
            this.invokePlayGameCallback();
        }

        public void CreateWarrior()
        {
            string name = this.inputField.text;
            GameSave.Instance.CreateNewSave(name);
            GameSave.Instance.Load(ProfileSave.Instance.AcctualSavePath);

            ChampionGenerator championGenerator = new ChampionGenerator();
            Player player = championGenerator.GenerateNewPlayer(ChampionClass.Warrior, name);

            GameSave.Instance.Player = player;
            GameSave.Instance.Update();
            this.invokePlayGameCallback();
        }
    }
}
