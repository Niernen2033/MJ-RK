using NPC;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityScene
{
    public class RecruitPanel : MonoBehaviour
    {
        public GameObject champion1;
        public GameObject champion2;
        public GameObject champion3;
        public Button champion1Button;
        public Button champion2Button;
        public Button champion3Button;
        public GameObject infoPanel;

        private CityData cityData;
        private Player player;
        private bool isChampionInfoPanelEnabled;

        private void Awake()
        {
            this.cityData = GameSave.Instance.CityData;
            this.player = GameSave.Instance.Player;
            this.isChampionInfoPanelEnabled = false;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (this.isChampionInfoPanelEnabled)
            {
                if (Input.GetMouseButton(0))
                {
                    this.CloseInfoPanel();
                }
            }

        }

        public void GetInfo_1_Champion()
        {
            if(this.cityData.TawernChampions[0].Available)
            {
                this.InfoFromChampionTable(this.cityData.TawernChampions[0].Champion);
            }
        }

        public void GetInfo_2_Champion()
        {
            if (this.cityData.TawernChampions[1].Available)
            {
                this.InfoFromChampionTable(this.cityData.TawernChampions[1].Champion);
            }
        }

        public void GetInfo_3_Champion()
        {
            if (this.cityData.TawernChampions[2].Available)
            {
                this.InfoFromChampionTable(this.cityData.TawernChampions[2].Champion);
            }
        }

        public void Recruit_1_Champion()
        {
            bool playerRecruitSomeone = false;
            for(int i=0; i <this.player.Team.Length; i++)
            {
                if(this.player.Team[i].ChampionClass == ChampionClass.None || this.player.Team[i].ChampionType == ChampionType.None)
                {
                    playerRecruitSomeone = true;
                    this.player.Team[i] = this.cityData.TawernChampions[0].Champion;
                    this.cityData.TawernChampions[0].Available = false;
                    break;
                }
            }
            if (!playerRecruitSomeone)
            {
                this.PrintToInfoPanel("You dont have free slots");
            }
            else
            {
                this.SetRecruitTable();
            }
        }

        public void Recruit_2_Champion()
        {
            bool playerRecruitSomeone = false;
            for (int i = 0; i < this.player.Team.Length; i++)
            {
                if (this.player.Team[i].ChampionClass == ChampionClass.None || this.player.Team[i].ChampionType == ChampionType.None)
                {
                    playerRecruitSomeone = true;
                    this.player.Team[i] = this.cityData.TawernChampions[1].Champion;
                    this.cityData.TawernChampions[1].Available = false;
                    break;
                }
            }
            if (!playerRecruitSomeone)
            {
                this.PrintToInfoPanel("You dont have free slots");
            }
            else
            {
                this.SetRecruitTable();
            }
        }

        public void Recruit_3_Champion()
        {
            bool playerRecruitSomeone = false;
            for (int i = 0; i < this.player.Team.Length; i++)
            {
                if (this.player.Team[i].ChampionClass == ChampionClass.None || this.player.Team[i].ChampionType == ChampionType.None)
                {
                    playerRecruitSomeone = true;
                    this.player.Team[i] = this.cityData.TawernChampions[2].Champion;
                    this.cityData.TawernChampions[2].Available = false;
                    break;
                }
            }
            if (!playerRecruitSomeone)
            {
                this.PrintToInfoPanel("You dont have free slots");
            }
            else
            {
                this.SetRecruitTable();
            }
        }

        public void SetRecruitTable()
        {
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            if (!this.cityData.TawernChampions[i].Available)
                            {
                                this.champion1.GetComponent<Button>().enabled = false;
                                this.champion1Button.gameObject.SetActive(false);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (!this.cityData.TawernChampions[i].Available)
                            {
                                this.champion2.GetComponent<Button>().enabled = false;
                                this.champion2Button.gameObject.SetActive(false);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (!this.cityData.TawernChampions[i].Available)
                            {
                                this.champion3.GetComponent<Button>().enabled = false;
                                this.champion3Button.gameObject.SetActive(false);
                            }
                            break;
                        }
                }
            }
        }

        public void BackToTawern()
        {
            this.gameObject.SetActive(false);
        }

        private void PrintToInfoPanel(string info)
        {
            if (!this.isChampionInfoPanelEnabled)
            {
                this.OpenInfoPanel();
            }
            this.infoPanel.gameObject.GetComponentInChildren<Text>().text = info;
        }

        private void OpenInfoPanel()
        {
            this.infoPanel.gameObject.SetActive(true);
            this.isChampionInfoPanelEnabled = true;
        }

        private void CloseInfoPanel()
        {
            this.isChampionInfoPanelEnabled = false;
            this.infoPanel.gameObject.SetActive(false);
        }

        private void InfoFromChampionTable(Champion champion)
        {
            this.OpenInfoPanel();
            this.PrintToInfoPanel(this.GetItemInfo(champion));
        }

        private string GetItemInfo(Champion champion)
        {
            string all_info = string.Empty;

            string[] info = champion.ToString().Split(';');

            for (int i = 0; i < info.Length; i++)
            {
                all_info += info[i] + "\n";
            }
            this.infoPanel.gameObject.GetComponentInChildren<Text>().text = all_info;

            return all_info;
        }

    }
}
