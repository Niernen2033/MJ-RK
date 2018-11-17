using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Items;
using SaveLoad;

namespace Prefabs.Inventory
{
    public class BagpackSlot : MonoBehaviour
    {
        public Image actionImage;
        public Image infoImage;
        public Image deleteImage;

        public bool IsEmpty { get; private set; }
        private Image icon;
        private Image iconRarity;

        private BagpackDeleteCallback bagpackDeleteCallback;
        private BagpackActivateCallback bagpackActivateCallback;
        private BagpackInfoCallback bagpackInfoCallback;
        public Item item { get; private set; }
        private int itemIndex;

        private void Awake()
        {

            this.IsEmpty = true;
            this.icon = this.gameObject.GetComponentsInChildren<Image>()[1];
            this.iconRarity = this.gameObject.GetComponentsInChildren<Image>()[2];

            this.bagpackDeleteCallback = null;
            this.bagpackInfoCallback = null;
            this.bagpackActivateCallback = null;
            this.item = null;
            this.itemIndex = -1;
        }

        private void Start()
        {

        }

        public void SetDeleteCallback(BagpackDeleteCallback bagpackDeleteCallback)
        {
            this.bagpackDeleteCallback = bagpackDeleteCallback;
        }

        public void SetActivateCallback(BagpackActivateCallback bagpackActivateCallback)
        {
            this.bagpackActivateCallback = bagpackActivateCallback;
        }

        public void SetInfoCallback(BagpackInfoCallback bagpackInfoCallback)
        {
            this.bagpackInfoCallback = bagpackInfoCallback;
        }

        public void AddItem(Item newItem, ItemFeaturesType[] bagpackTypeFeatures, int index)
        {
            if (newItem != null)
            {
                this.item = newItem;
                this.itemIndex = index;
                this.SetSlotIcons();
                this.SetSlotOptions(bagpackTypeFeatures);
                this.IsEmpty = false;
            }
            else
            {
                ClearSlot();
            }
        }

        private void SetSlotOptions(ItemFeaturesType[] bagpackTypeFeatures)
        {
            Sprite[] options_icons = Resources.LoadAll<Sprite>(SaveInfo.Paths.Resources.Images.Inventory.AllOptionsItems);

            int actionImageIndex = -1;
            int deleteImageIndex = -1;
            int infoImageIndex = -1;

            foreach (ItemFeaturesType bagpackItemFeature in bagpackTypeFeatures)
            {
                if(bagpackItemFeature == ItemFeaturesType.IsDeleteAble)
                {
                    if(this.item.Features.GetFeatureStatus(ItemFeaturesType.IsDeleteAble))
                    {
                        deleteImageIndex = (int)InventoryIndex.Options.Delete;
                    }
                }
                else if (bagpackItemFeature == ItemFeaturesType.IsInfoAble)
                {
                    if (this.item.Features.GetFeatureStatus(ItemFeaturesType.IsInfoAble))
                    {
                        infoImageIndex = (int)InventoryIndex.Options.Info;
                    }
                }
                else
                {
                    if (this.item.Features.GetFeatureStatus(bagpackItemFeature))
                    {
                        switch (bagpackItemFeature)
                        {
                            case ItemFeaturesType.IsEatAble:
                                {
                                    actionImageIndex = (int)InventoryIndex.Options.Eat;
                                    break;
                                }
                            case ItemFeaturesType.IsEquipAble:
                                {
                                    actionImageIndex = (int)InventoryIndex.Options.Equip;
                                    break;
                                }
                            case ItemFeaturesType.IsRepairAble:
                                {
                                    actionImageIndex = (int)InventoryIndex.Options.Repair;
                                    break;
                                }
                            case ItemFeaturesType.IsSellAble:
                                {
                                    actionImageIndex = (int)InventoryIndex.Options.Sell;
                                    break;
                                }
                            case ItemFeaturesType.IsUpgradeAble:
                                {
                                    actionImageIndex = (int)InventoryIndex.Options.Upgrade;
                                    break;
                                }
                        }
                    }
                }
            }

            if (actionImageIndex != -1)
            {
                bool delete = false;
                if((actionImageIndex == (int)InventoryIndex.Options.Sell) ||
                    (actionImageIndex == (int)InventoryIndex.Options.Equip) ||
                    (actionImageIndex == (int)InventoryIndex.Options.Eat))
                {
                    delete = true;
                }

                this.actionImage.sprite = options_icons[actionImageIndex];
                this.actionImage.enabled = true;
                this.actionImage.GetComponent<Button>().onClick.AddListener(() => this.OnActionClick(delete));
            }
            if (deleteImageIndex != -1)
            {
                this.deleteImage.sprite = options_icons[deleteImageIndex];
                this.deleteImage.enabled = true;
                this.deleteImage.GetComponent<Button>().onClick.AddListener(() => this.OnDeleteClick());
            }
            if (infoImageIndex != -1)
            {
                this.infoImage.sprite = options_icons[infoImageIndex];
                this.infoImage.enabled = true;
                this.infoImage.GetComponent<Button>().onClick.AddListener(() => this.OnInfoClick());
            }
        }

        private void OnActionClick(bool delete)
        {
            this.bagpackActivateCallback?.Invoke(this.item, this.itemIndex);
            if (delete)
            {
                this.ClearSlot();
            }
        }

        private void OnInfoClick()
        {
            this.bagpackInfoCallback?.Invoke(this.item);
        }

        private void OnDeleteClick()
        {
            this.bagpackDeleteCallback?.Invoke(this.item, this.itemIndex);
            this.ClearSlot();
        }

        private void SetSlotIcons()
        {
            Sprite[] items_icons = Resources.LoadAll<Sprite>(SaveInfo.Paths.Resources.Images.Inventory.AllItems);

            int iconIndex = this.item.Icon.Index;
            this.icon.sprite = items_icons[iconIndex];
            this.icon.enabled = true;

            ItemRarity iconRarityIndex = this.item.Icon.Rarity;
            if (iconRarityIndex != ItemRarity.None)
            {
                this.iconRarity.sprite = items_icons[(int)iconRarityIndex];
                this.iconRarity.enabled = true;
            }
        }

        private void ClearSlot()
        {
            this.icon.sprite = null;
            this.icon.enabled = false;

            this.iconRarity.sprite = null;
            this.iconRarity.enabled = false;

            this.actionImage.sprite = null;
            this.actionImage.enabled = false;

            this.deleteImage.sprite = null;
            this.deleteImage.enabled = false;

            this.infoImage.sprite = null;
            this.infoImage.enabled = false;

            this.IsEmpty = true;
            this.item = null;
            this.itemIndex = -1;
        }
    }
}
