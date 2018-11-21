using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using System;
using NPC;

namespace Prefabs.Inventory
{
    public class Eq : MonoBehaviour
    {

        public BagpackSlot HelmetSlot;
        public BagpackSlot BodySlot;
        public BagpackSlot WeaponSlot;
        public BagpackSlot ShieldSlot;
        public BagpackSlot TrinketSlot;
        public BagpackSlot GlovesSlot;
        public BagpackSlot BootsSlot;
        public Image itemInfoPanel;

        private ItemFeaturesType[] eqFeatures;
        private Champion eqOwner;
        private Equipment equipment;

        private bool isDirty;
        private bool isItemInfoPanelEnabled;
        public bool IsCallbacksSeted { get; private set; }
        private Item swapItem;
        public bool IsIHaveItemToSwap { get; private set; }

        public bool IsDataLoaded { get; private set; }

        public void SetCallbacks(BagpackActivateCallback bagpackActivateCallback)
        {
            this.HelmetSlot.SetActivateCallback(bagpackActivateCallback);
            this.HelmetSlot.SetInfoCallback(this.InfoFromEquipment);
            this.BodySlot.SetActivateCallback(bagpackActivateCallback);
            this.BodySlot.SetInfoCallback(this.InfoFromEquipment);
            this.WeaponSlot.SetActivateCallback(bagpackActivateCallback);
            this.WeaponSlot.SetInfoCallback(this.InfoFromEquipment);
            this.ShieldSlot.SetActivateCallback(bagpackActivateCallback);
            this.ShieldSlot.SetInfoCallback(this.InfoFromEquipment);
            this.TrinketSlot.SetActivateCallback(bagpackActivateCallback);
            this.TrinketSlot.SetInfoCallback(this.InfoFromEquipment);
            this.GlovesSlot.SetActivateCallback(bagpackActivateCallback);
            this.GlovesSlot.SetInfoCallback(this.InfoFromEquipment);
            this.BootsSlot.SetActivateCallback(bagpackActivateCallback);
            this.BootsSlot.SetInfoCallback(this.InfoFromEquipment);
            this.IsCallbacksSeted = true;
        }

        private void Awake()
        {
            this.ClearData();

            this.eqFeatures = new ItemFeaturesType[2] { ItemFeaturesType.IsEquipAble, ItemFeaturesType.IsInfoAble };
        }

        private void Start()
        {
            this.FreeSlots();
            this.isDirty = true;
        }

        private void ClearData()
        {
            this.swapItem = null;
            this.IsIHaveItemToSwap = false;
            this.IsDataLoaded = false;
            this.isItemInfoPanelEnabled = false;
            this.isDirty = false;
            this.eqOwner = null;
            this.equipment = null;
            this.IsCallbacksSeted = false;
        }

        public void SetBagpack(Equipment equipment)
        {
            this.ClearData();

            if (this.equipment != equipment && equipment != null)
            {
                this.equipment = equipment;
                this.isDirty = true;
                this.IsDataLoaded = true;
            }
        }

        public Item GetSwapItem()
        {
            Item result = null;
            if(this.IsIHaveItemToSwap)
            {
                result = this.swapItem;
                this.swapItem = null;
                this.IsIHaveItemToSwap = false;
            }

            return result;
        }

        public void SetChampion(Champion champion)
        {
            this.eqOwner = champion;
        }

        public bool AddToEQ(Item item, EqType eqType)
        {
            if (item != null && eqType != EqType.None)
            {
                bool canIEquip = true;
                switch (eqType)
                {
                    case EqType.Body:
                        {
                            if (this.BodySlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.BodySlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.BodySlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.BodySlot.ClearSlot();
                                this.BodySlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                    case EqType.Boots:
                        {
                            if (this.BootsSlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.BootsSlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.BootsSlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.BootsSlot.ClearSlot();
                                this.BootsSlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                    case EqType.Gloves:
                        {
                            if (this.GlovesSlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.GlovesSlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.GlovesSlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.GlovesSlot.ClearSlot();
                                this.GlovesSlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                    case EqType.Helmet:
                        {
                            if (this.HelmetSlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.HelmetSlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.HelmetSlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.HelmetSlot.ClearSlot();
                                this.HelmetSlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                    case EqType.Shield:
                        {
                            if(this.eqOwner != null)
                            {
                                if(this.eqOwner.ChampionClass == ChampionClass.Range)
                                {
                                    canIEquip = false;
                                }
                            }
                            if (canIEquip)
                            {
                                if (this.ShieldSlot.IsEmpty)
                                {
                                    this.equipment.EquipItem(eqType, item, this.eqOwner);
                                    this.ShieldSlot.AddItem(item, this.eqFeatures);
                                }
                                else
                                {
                                    //swap
                                    this.IsIHaveItemToSwap = true;
                                    this.swapItem = this.ShieldSlot.item;
                                    this.equipment.UnequipItem(eqType, this.eqOwner);

                                    this.equipment.EquipItem(eqType, item, this.eqOwner);
                                    this.ShieldSlot.ClearSlot();
                                    this.ShieldSlot.AddItem(item, this.eqFeatures);
                                }
                            }
                            break;
                        }
                    case EqType.Trinket:
                        {
                            if (this.TrinketSlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.TrinketSlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.TrinketSlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.TrinketSlot.ClearSlot();
                                this.TrinketSlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                    case EqType.Weapon:
                        {
                            if (this.WeaponSlot.IsEmpty)
                            {
                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.WeaponSlot.AddItem(item, this.eqFeatures);
                            }
                            else
                            {
                                //swap
                                this.IsIHaveItemToSwap = true;
                                this.swapItem = this.WeaponSlot.item;
                                this.equipment.UnequipItem(eqType, this.eqOwner);

                                this.equipment.EquipItem(eqType, item, this.eqOwner);
                                this.WeaponSlot.ClearSlot();
                                this.WeaponSlot.AddItem(item, this.eqFeatures);
                            }
                            break;
                        }
                }
                if (canIEquip)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Item RemoveFromEQ(EqType eqItemType)
        {
            if(eqItemType == EqType.None)
            {
                return null;
            }

            Item result = null;

            switch (eqItemType)
            {
                case EqType.Body:
                    {
                        result = this.BodySlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.BodySlot.ClearSlot();
                        break;
                    }
                case EqType.Boots:
                    {
                        result = this.BootsSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.BootsSlot.ClearSlot();
                        break;
                    }
                case EqType.Gloves:
                    {
                        result = this.GlovesSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.GlovesSlot.ClearSlot();
                        break;
                    }
                case EqType.Helmet:
                    {
                        result = this.HelmetSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.HelmetSlot.ClearSlot();
                        break;
                    }
                case EqType.Shield:
                    {
                        result = this.ShieldSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.ShieldSlot.ClearSlot();
                        break;
                    }
                case EqType.Trinket:
                    {
                        result = this.TrinketSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.TrinketSlot.ClearSlot();
                        break;
                    }
                case EqType.Weapon:
                    {
                        result = this.WeaponSlot.item;
                        this.equipment.UnequipItem(eqItemType, this.eqOwner);
                        this.WeaponSlot.ClearSlot();
                        break;
                    }
            }

            return result;
        }

        private void PrepareEquipment()
        {
            if (this.equipment != null)
            {
                for (int i = 0; i < Enum.GetValues(typeof(EqType)).Length - 1; i++)
                {
                    EquipmentItem equipmentItem = this.equipment.GetItemByType((EqType)i);
                    if (equipmentItem != null)
                    {
                        if (equipmentItem.EquipmentType == (EqType)i)
                        {
                            this.AddToEQ(equipmentItem, (EqType)i);
                        }
                    }
                }
            }

            this.isDirty = false;
        }

        private void Update()
        {
            if (this.isItemInfoPanelEnabled)
            {
                if (Input.GetMouseButton(0))
                {
                    this.CloseInfoPanel();
                }
            }

            if (this.isDirty)
            {
                this.PrepareEquipment();
            }
        }

        private void FreeSlots()
        {
            this.HelmetSlot.ClearSlot();
            this.BodySlot.ClearSlot();
            this.WeaponSlot.ClearSlot();
            this.ShieldSlot.ClearSlot();
            this.TrinketSlot.ClearSlot();
            this.GlovesSlot.ClearSlot();
            this.BootsSlot.ClearSlot();
        }

        public void FreeEquipmentMemory()
        {
            this.FreeSlots();
            this.ClearData();
        }

        public void ReloadEquipment()
        {
            this.FreeSlots();
            this.PrepareEquipment();
        }

        private void PrintToInfoPanel(string info)
        {
            if (!this.isItemInfoPanelEnabled)
            {
                this.OpenInfoPanel();
            }
            this.itemInfoPanel.gameObject.GetComponentInChildren<Text>().text = info;
        }

        private void OpenInfoPanel()
        {
            this.itemInfoPanel.gameObject.SetActive(true);
            this.isItemInfoPanelEnabled = true;
        }

        private void CloseInfoPanel()
        {
            this.isItemInfoPanelEnabled = false;
            this.itemInfoPanel.gameObject.SetActive(false);
        }

        private void InfoFromEquipment(Item item)
        {
            //we have info feature (Armor or Weapon or Comsuable)
            this.OpenInfoPanel();
            this.PrintToInfoPanel(this.GetItemInfo(item));
        }

        private string GetItemInfo(Item item)
        {
            string all_info = string.Empty;
            if (item is Armor)
            {
                Armor armor = (Armor)item;
                string[] info = armor.ToString().Split(';');

                for (int i = 0; i < info.Length; i++)
                {
                    all_info += info[i] + "\n";
                }
                this.itemInfoPanel.gameObject.GetComponentInChildren<Text>().text = all_info;
            }
            else if (item is Weapon)
            {
                Weapon weapon = (Weapon)item;
                string[] info = weapon.ToString().Split(';');

                for (int i = 0; i < info.Length; i++)
                {
                    all_info += info[i] + "\n";
                }
                this.itemInfoPanel.gameObject.GetComponentInChildren<Text>().text = all_info;
            }

            return all_info;
        }
    }
}
