using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using NPC;

namespace Prefabs.Inventory
{
    public class EqInventory : MonoBehaviour
    {
        public bool IsOpen { get; private set; }
        public Bagpack PlayerBagpack { get; private set; }
        public Eq ChampionEquipment { get; private set; }

        private void Awake()
        {
            this.IsOpen = false;
            this.PlayerBagpack = this.gameObject.GetComponentInChildren<Bagpack>();
            this.ChampionEquipment = this.gameObject.GetComponentInChildren<Eq>();
        }

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }



        public void OpenInventory()
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
                this.PlayerBagpack.ReloadBagpack();
                this.ChampionEquipment.ReloadEquipment();
                this.IsOpen = true;
            }
        }

        public void OpenAndLoadInventory(Equipment equipment, List<Item> player_items, Champion champion = null)
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
                if (player_items != null)
                {
                    this.PlayerBagpack.SetBagpack(player_items);
                }
                if(equipment != null)
                {
                    this.ChampionEquipment.SetBagpack(equipment);
                }
                if (champion != null)
                {
                    this.PlayerBagpack.SetChampion(champion);
                    this.ChampionEquipment.SetChampion(champion);
                }
                this.IsOpen = true;
            }
        }

        public void CloseInventory()
        {
            if (this.gameObject.activeSelf == true)
            {
                this.IsOpen = false;
                this.gameObject.SetActive(false);
            }
        }

        public void CloseAndDestroyInventory()
        {
            if (this.gameObject.activeSelf == true)
            {
                this.PlayerBagpack.FreeBagpackMemory();
                this.ChampionEquipment.FreeEquipmentMemory();
                this.IsOpen = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
