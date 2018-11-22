using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prefabs.Inventory;
using SaveLoad;

namespace CityScene
{
    public class BlackSmith : MonoBehaviour
    {
        public GameObject shopInventory;
        public GameObject upgradeInventory;
        public GameObject repairInventory;
        public GameObject anvil;
        public GameObject shop;
        public GameObject wheel;
        public Button closeInventoryButton;
        public Button backToCityButton;

        private ShopInventory ShopInventory;
        private UpgradeInventory UpgradeInventory;
        private RepairInventory RepairInventory;
        private OpenBuildingCallback openBuildingCallback;

        private void Awake()
        {
            this.ShopInventory = shopInventory.GetComponent<ShopInventory>();
            this.UpgradeInventory = this.upgradeInventory.GetComponent<UpgradeInventory>();
            this.RepairInventory = this.repairInventory.GetComponent<RepairInventory>();

            this.anvil.gameObject.GetComponent<Button>().onClick.AddListener(() => this.OpenBlackSmithAnvil());
            this.shop.gameObject.GetComponent<Button>().onClick.AddListener(() => this.OpenBlackSmithShop());
            this.wheel.gameObject.GetComponent<Button>().onClick.AddListener(() => this.OpenBlackSmithWheel());

            this.openBuildingCallback = this.gameObject.GetComponentInParent<City>().OpenBuilding;
        }

        private void OpenBlackSmithAnvil()
        {
            if (!this.RepairInventory.IsOpen && !this.shopInventory.activeSelf && !this.upgradeInventory.activeSelf)
            {
                this.closeInventoryButton.gameObject.SetActive(true);
                this.backToCityButton.gameObject.SetActive(false);
                if (this.RepairInventory.PlayerBagpack == null)
                {
                    this.RepairInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                }
                if (this.RepairInventory.PlayerBagpack.IsDataLoaded)
                {
                    this.RepairInventory.OpenInventory();
                }
                else
                {
                    this.RepairInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                }
            }
        }

        private void OpenBlackSmithWheel()
        {
            if (!this.UpgradeInventory.IsOpen && !this.shopInventory.activeSelf && !this.repairInventory.activeSelf)
            {
                this.closeInventoryButton.gameObject.SetActive(true);
                this.backToCityButton.gameObject.SetActive(false);
                if (this.UpgradeInventory.PlayerBagpack == null)
                {
                    this.UpgradeInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                }
                if (this.UpgradeInventory.PlayerBagpack.IsDataLoaded)
                {
                    this.UpgradeInventory.OpenInventory();
                }
                else
                {
                    this.UpgradeInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                }
            }
        }

        private void OpenBlackSmithShop()
        {
            if (!this.ShopInventory.IsOpen && !this.upgradeInventory.activeSelf && !this.repairInventory.activeSelf)
            {
                this.closeInventoryButton.gameObject.SetActive(true);
                this.backToCityButton.gameObject.SetActive(false);

                if (this.ShopInventory.PlayerBagpack == null)
                {
                    this.ShopInventory.OpenAndLoadInventory(GameSave.Instance.CityData.BlackSmithShopBagpack, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
                }
                if (this.ShopInventory.PlayerBagpack.IsDataLoaded || this.ShopInventory.ShopBagpack.IsDataLoaded)
                {
                    this.ShopInventory.OpenInventory();
                }
                else
                {
                    this.ShopInventory.OpenAndLoadInventory(GameSave.Instance.CityData.BlackSmithShopBagpack, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
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
                }
            }

            if (this.UpgradeInventory != null)
            {
                if (this.wheel.activeSelf)
                {
                    this.UpgradeInventory.CloseInventory();
                    GameSave.Instance.Update();
                    this.closeInventoryButton.gameObject.SetActive(false);
                    this.backToCityButton.gameObject.SetActive(true);
                }
            }

            if(this.RepairInventory != null)
            {

                if (this.anvil.activeSelf)
                {
                    this.RepairInventory.CloseInventory();
                    GameSave.Instance.Update();
                    this.closeInventoryButton.gameObject.SetActive(false);
                    this.backToCityButton.gameObject.SetActive(true);
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
