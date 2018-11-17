using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Prefabs.Inventory
{
    public enum InvenotryType { Normal, Shop, Repair, EQ, Upgrade };

    public class ShopInventory : MonoBehaviour
    {
        public bool IsOpen { get; private set; }
        public Bagpack PlayerBagpack { get; private set; }
        public Bagpack ShopBagpack { get; private set; }

        private void Awake()
        {
            this.IsOpen = false;
            this.PlayerBagpack = this.gameObject.GetComponentsInChildren<Bagpack>()[0];
            this.ShopBagpack = this.gameObject.GetComponentsInChildren<Bagpack>()[1];
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
                this.ShopBagpack.ReloadBagpack();
                this.IsOpen = true;
            }
        }

        public void OpenAndLoadInventory(List<Item> shop_items, List<Item> player_items, Champion champion = null)
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
                if (shop_items != null)
                {
                    this.ShopBagpack.SetBagpack(shop_items);
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
                this.ShopBagpack.FreeBagpackMemory();
                this.IsOpen = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
