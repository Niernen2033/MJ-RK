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

        private void Awake()
        {
            this.IsEmpty = true;
            this.icon = this.gameObject.GetComponentsInChildren<Image>()[1];
            this.iconRarity = this.gameObject.GetComponentsInChildren<Image>()[2];
        }

        private void Start()
        {

        }

        public void AddItem(Item newItem, BagpackType bagpackType)
        {
            this.SetSlotIcons(newItem);
            this.SetSlotOptions(newItem, bagpackType);
            this.IsEmpty = false;
        }

        private void SetSlotOptions(Item item, BagpackType bagpackType)
        {
            Sprite[] options_icons = Resources.LoadAll<Sprite>(SaveInfo.Paths.Resources.Images.Inventory.AllOptionsItems);

            int actionImageIndex = -1;
            int deleteImageIndex = -1;
            int infoImageIndex = -1;
            List<FeaturesType> itemFeatures = item.Features.GetAvailableFeatures();
            switch (bagpackType)
            {
                case BagpackType.EQ:
                    {

                        break;
                    }
                case BagpackType.Normal:
                    {
                        if (itemFeatures.Contains(FeaturesType.IsEatAble))
                        {
                            actionImageIndex = (int)InventoryIndex.Options.Eat;
                        }
                        if (itemFeatures.Contains(FeaturesType.IsInfoAble))
                        {
                            infoImageIndex = (int)InventoryIndex.Options.Info;
                        }

                        deleteImageIndex = (int)InventoryIndex.Options.Delete;

                        break;
                    }
                case BagpackType.Repair:
                    {
                        break;
                    }
                case BagpackType.Shop:
                    {
                        break;
                    }
            }

            if (actionImageIndex != -1)
            {
                this.actionImage.sprite = options_icons[actionImageIndex];
                this.actionImage.enabled = true;
            }
            if (deleteImageIndex != -1)
            {
                this.deleteImage.sprite = options_icons[deleteImageIndex];
                this.deleteImage.enabled = true;
            }
            if (infoImageIndex != -1)
            {
                this.infoImage.sprite = options_icons[infoImageIndex];
                this.infoImage.enabled = true;
            }
        }

        private void SetSlotIcons(Item item)
        {
            Sprite[] items_icons = Resources.LoadAll<Sprite>(SaveInfo.Paths.Resources.Images.Inventory.AllItems);

            int iconIndex = item.Icon.Index;
            this.icon.sprite = items_icons[iconIndex];
            this.icon.enabled = true;

            int iconRarityIndex = (int)item.Icon.Rarity;
            this.iconRarity.sprite = items_icons[iconRarityIndex];
            this.iconRarity.enabled = true;
        }

        public void ClearSlot()
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
        }
    }
}
