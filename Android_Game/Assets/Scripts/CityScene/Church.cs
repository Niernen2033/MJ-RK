using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prefabs.Inventory;
using SaveLoad;

namespace CityScene
{
    public class Church : MonoBehaviour
    {
        public GameObject shopInventory;
        public GameObject shop;
        public Button closeInventoryButton;
        public Button backToCityButton;

        private ShopInventory ShopInventory;
        private OpenBuildingCallback openBuildingCallback;

        private void Awake()
        {
            this.ShopInventory = shopInventory.GetComponent<ShopInventory>();

            this.shop.gameObject.GetComponent<Button>().onClick.AddListener(() => this.OpenChurchShop());

            this.openBuildingCallback = this.gameObject.GetComponentInParent<City>().OpenBuilding;
        }

        private void OpenChurchShop()
        {
            if (!this.ShopInventory.IsOpen)
            {
                this.closeInventoryButton.gameObject.SetActive(true);
                this.backToCityButton.gameObject.SetActive(false);

                if (this.ShopInventory.PlayerBagpack == null)
                {
                    this.ShopInventory.OpenAndLoadInventory(GameSave.Instance.CityData.ChurchShopBagpack, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                }
                if (this.ShopInventory.PlayerBagpack.IsDataLoaded || this.ShopInventory.ShopBagpack.IsDataLoaded)
                {
                    this.ShopInventory.OpenInventory();
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                }
                else
                {
                    this.ShopInventory.OpenAndLoadInventory(GameSave.Instance.CityData.ChurchShopBagpack, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(true);
                }
            }
        }

        public void CloseInventory()
        {
            if (this.ShopInventory != null)
            {
                if (this.shop.activeSelf)
                {
                    this.ShopInventory.CloseInventory();
                    GameSave.Instance.Update();
                    this.closeInventoryButton.gameObject.SetActive(false);
                    this.backToCityButton.gameObject.SetActive(true);
                    this.gameObject.GetComponentInParent<City>().ChangeBuildingBlockStatus(false);
                }
            }

        }

        public void BackToCity()
        {
            this.openBuildingCallback(CityObjectType.CityAll);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
