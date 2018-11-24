using NPC;
using Prefabs.Inventory;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityScene
{
    public class CityAll : MonoBehaviour
    {
        public GameObject normalInventory;
        public GameObject equipmentInventory;
        public Button closeInventoryButton;
        public Button normalInventoryButton;
        public Button equipmentInventoryButton;
        public GameObject teamChoose;

        private NormalInventory NormalInventory;
        private EqInventory EqInventory;
        private Player player;
        private Champion choosenChampion;
        private AvatarButton[] championsButtons;
        private int selectedAvatarIndex;

        private void Awake()
        {
            this.NormalInventory = this.normalInventory.GetComponent<NormalInventory>();
            this.EqInventory = this.equipmentInventory.GetComponent<EqInventory>();
            this.championsButtons = this.teamChoose.GetComponentsInChildren<AvatarButton>();
            this.player = GameSave.Instance.Player;
        }

        private void SetUpAvatars()
        {
            this.RefreshPlayerTeam();

            this.selectedAvatarIndex = 0;
            this.choosenChampion = this.player;
            this.championsButtons[0].IsSelected = true;
            this.championsButtons[0].ReloadImage();
        }

        public void RefreshPlayerTeam()
        {
            this.choosenChampion = this.player;
            this.championsButtons[0].gameObject.GetComponentInChildren<Text>().text = this.player.Name;

            int activeButtonIndex = 1;
            for (int i = 0; i < this.player.Team.Length; i++)
            {
                this.championsButtons[i + 1].gameObject.SetActive(false);
                if (this.player.Team[i].ChampionClass != ChampionClass.None)
                {
                    this.championsButtons[activeButtonIndex].gameObject.SetActive(true);
                    this.championsButtons[activeButtonIndex].gameObject.GetComponentInChildren<Text>().text = this.player.Team[i].Name;
                    activeButtonIndex++;
                }
            }
        }

        public void OpenBagpack()
        {
            if (!this.NormalInventory.IsOpen)
            {
                this.normalInventoryButton.gameObject.SetActive(false);
                this.equipmentInventoryButton.gameObject.SetActive(false);
                this.closeInventoryButton.gameObject.SetActive(true);

                if (this.NormalInventory.PlayerBagpack == null)
                {
                    this.NormalInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
                if (this.NormalInventory.PlayerBagpack.IsDataLoaded)
                {
                    this.NormalInventory.OpenInventory();
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
                else
                {
                    this.NormalInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
            }
        }

        public void OpenInventory()
        {
            if (!this.EqInventory.IsOpen)
            {
                this.normalInventoryButton.gameObject.SetActive(false);
                this.equipmentInventoryButton.gameObject.SetActive(false);
                this.closeInventoryButton.gameObject.SetActive(true);

                if (this.EqInventory.PlayerBagpack == null)
                {
                    this.EqInventory.OpenAndLoadInventory(this.choosenChampion.Equipment, GameSave.Instance.Player.Bagpack, this.choosenChampion);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
                if (this.EqInventory.PlayerBagpack.IsDataLoaded)
                {
                    this.EqInventory.OpenInventory();
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
                else
                {
                    this.EqInventory.OpenAndLoadInventory(this.choosenChampion.Equipment, GameSave.Instance.Player.Bagpack, this.choosenChampion);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                    this.teamChoose.SetActive(false);
                }
            }
        }

        public void CloseInventory()
        {
            if (this.EqInventory != null)
            {
                if (this.equipmentInventory.activeSelf)
                {
                    this.EqInventory.CloseInventory();
                    GameSave.Instance.Update();
                    this.closeInventoryButton.gameObject.SetActive(false);
                    this.normalInventoryButton.gameObject.SetActive(true);
                    this.equipmentInventoryButton.gameObject.SetActive(true);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(false);
                    this.teamChoose.SetActive(true);
                }
            }

            if (this.NormalInventory != null)
            {
                if (this.normalInventory.activeSelf)
                {
                    this.NormalInventory.CloseInventory();
                    GameSave.Instance.Update();
                    this.closeInventoryButton.gameObject.SetActive(false);
                    this.normalInventoryButton.gameObject.SetActive(true);
                    this.equipmentInventoryButton.gameObject.SetActive(true);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(false);
                    this.teamChoose.SetActive(true);
                }
            }
        }

        public void SetChooseChampion(int index)
        {
            if (this.IfSelectedOtherAvatarsCount())
            {
                this.championsButtons[this.selectedAvatarIndex].IsSelected = false;
                this.championsButtons[this.selectedAvatarIndex].ReloadImage();
                foreach (AvatarButton avatarButton in this.championsButtons)
                {
                    if (avatarButton.IsSelected)
                    {
                        this.selectedAvatarIndex = index;
                        this.championsButtons[this.selectedAvatarIndex].ReloadImage();
                        if (this.selectedAvatarIndex == 0)
                        {
                            this.choosenChampion = this.player;
                        }
                        else
                        {
                            this.choosenChampion = this.player.Team[this.selectedAvatarIndex - 1];
                        }

                        this.EqInventory.ChangeEquipment(this.choosenChampion.Equipment, this.choosenChampion);
                    }
                }
            }
        }

        private bool IfSelectedOtherAvatarsCount()
        {
            int selected_count = 0;
            foreach (AvatarButton avatarButton in this.championsButtons)
            {
                if (avatarButton.IsSelected)
                {
                    selected_count++;
                }
            }
            if(selected_count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Use this for initialization
        void Start()
        {
            this.SetUpAvatars();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
