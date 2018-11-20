using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using NPC;

namespace Prefabs.Inventory
{
    public class UpgradeInventory : MonoBehaviour
    {
        public int GetUpgradeTickCost { get; private set; } = 60;

        public bool IsOpen { get; private set; }
        public Bagpack PlayerBagpack { get; private set; }

        private void Awake()
        {
            this.IsOpen = false;
            this.PlayerBagpack = this.gameObject.GetComponentsInChildren<Bagpack>()[0];
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
                this.IsOpen = true;
            }
        }

        public void OpenAndLoadInventory(List<Item> player_items, Champion champion = null)
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
                if (player_items != null)
                {
                    this.PlayerBagpack.SetBagpack(player_items);
                }
                if (champion != null)
                {
                    this.PlayerBagpack.SetChampion(champion);
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
                this.IsOpen = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
