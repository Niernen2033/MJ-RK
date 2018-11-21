using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Items;
using System;
using NPC;

namespace Prefabs.Inventory
{
    public delegate void BagpackDeleteCallback(Item item);
    public delegate void BagpackActivateCallback(Item item, ClearSlotCallback clearSlotCallback);
    public delegate void BagpackInfoCallback(Item item);

    public class Bagpack : MonoBehaviour
    {
        public GameObject AllBagpackSlots;
        public BagpackSlot BagpackSlot;
        public Text GoldTextValue;
        public Text WeightTextValue;
        public Image itemInfoPanel;
        public InvenotryType bagpackType;

        private List<ItemFeaturesType[]> bagpackTypeFeatures;
        private List<BagpackSlot> bagpackslots;
        private List<Item> items;
        private Champion bagpackOwner;

        private bool isDirty;
        private bool isItemInfoPanelEnabled;

        public bool IsDataLoaded { get; private set; }

        private double max_weight;
        private double inventory_weight;
        private int inventory_gold;

        private void Awake()
        {
            this.ClearData();
            this.bagpackTypeFeatures = new List<ItemFeaturesType[]>();
            this.bagpackslots = new List<BagpackSlot>();

            //Normal================================================================
            this.bagpackTypeFeatures.Add(new ItemFeaturesType[] { ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsEatAble, ItemFeaturesType.IsDeleteAble });

            //Shop=====================================================================
            this.bagpackTypeFeatures.Add(new ItemFeaturesType[] { ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsSellAble });

            //Repair================================================================
            this.bagpackTypeFeatures.Add(new ItemFeaturesType[] { ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsDeleteAble, ItemFeaturesType.IsRepairAble });

            //EQ====================================================================
            this.bagpackTypeFeatures.Add(new ItemFeaturesType[] { ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsEquipAble, ItemFeaturesType.IsDeleteAble });

            //Upgrade===============================================================
            this.bagpackTypeFeatures.Add(new ItemFeaturesType[] { ItemFeaturesType.IsInfoAble, ItemFeaturesType.IsUpgradeAble, ItemFeaturesType.IsDeleteAble });
        }

        // Use this for initialization
        private void Start()
        {
            this.isDirty = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if(this.isItemInfoPanelEnabled)
            {
                if(Input.GetMouseButton(0))
                {
                    this.CloseInfoPanel();
                }
            }

            if (this.isDirty)
            {
                this.PrepareInventory();
            }
        }

        public void AddItem(Item item)
        {
            if (item != null)
            {
                if ((this.inventory_weight + item.Weight) > this.max_weight)
                {
                    //to heavy
                    Debug.Log("to heavy");
                }
                else
                {
                    if (this.items == null)
                    {
                        this.items = new List<Item>();
                        Debug.Log("Bagpack is not set to a instance of class");
                    }
                    this.items.Add(item);
                    this.AddToBagpack(this.items[this.items.Count - 1]);
                }
            }
        }

        private bool IfICanSellBuyItem(int bagpackGold, int itemGold)
        {
            if(bagpackGold < itemGold)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SellBuyItem(Item item, ClearSlotCallback clearSlotCallback)
        {
            if (item != null)
            {
                Bagpack PlayerBagpack = this.gameObject.GetComponentInParent<ShopInventory>().PlayerBagpack;
                Bagpack ShopBagpack = this.gameObject.GetComponentInParent<ShopInventory>().ShopBagpack;
                if (PlayerBagpack == this)
                {
                    //this is player bagpack (player sell item)
                    if (this.IfICanSellBuyItem(ShopBagpack.inventory_gold, item.GoldValue))
                    {
                        ShopBagpack.AddItem(item);

                        ShopBagpack.AddItem(this.GetGoldByValue(-item.GoldValue));
                        PlayerBagpack.AddItem(this.GetGoldByValue(item.GoldValue));

                        PlayerBagpack.DeleteFromBagpack(item);
                        clearSlotCallback();
                    }
                    else
                    {
                        this.PrintToInfoPanel("Shop owner dont have gold to buy this item");
                    }
                }
                else
                {
                    //this is shop bagpack (player buy item)
                    if (this.IfICanSellBuyItem(PlayerBagpack.inventory_gold, item.GoldValue))
                    {
                        PlayerBagpack.AddItem(item);

                        PlayerBagpack.AddItem(this.GetGoldByValue(-item.GoldValue));
                        ShopBagpack.AddItem(this.GetGoldByValue(item.GoldValue));

                        ShopBagpack.DeleteFromBagpack(item);
                        clearSlotCallback();
                    }
                    else
                    {
                        this.PrintToInfoPanel("You dont have gold to buy this item");
                    }
                }


                //Debug.Log("Player: " + this.gameObject.GetComponentInParent<ShopInventory>().PlayerBagpack.items.Count);
                //Debug.Log("Shop: " + this.gameObject.GetComponentInParent<ShopInventory>().ShopBagpack.items.Count);
            }
        }

        private bool IsGold(Item item)
        {
            if (item != null)
            {
                if (item.Icon.Index == (int)ItemIndex.Gold.Large)
                {
                    return true;
                }
            }
            return false;
        }

        private void IncreaseDecreaseGold(int value)
        {
            this.AddItem(this.GetGoldByValue(value));
        }

        private Item GetGoldByValue(int goldValue)
        {
            Item goldPrefab = new Item();
            goldPrefab.Icon.Index = (int)ItemIndex.Gold.Large;
            goldPrefab.GoldValue = goldValue;
            return goldPrefab;
        }

        private void AddToBagpack(Item item)
        {
            if (item != null)
            {
                bool isAdded = false;
                for (int i = 0; i < this.bagpackslots.Count; i++)
                {
                    //Debug.Log("Slot nr " + i + " jest: " + this.bagpackslots[i].IsEmpty);
                    if (this.bagpackslots[i].IsEmpty)
                    {
                        //Debug.Log("Dodaje do slota: " + i + " item o indexie: " + index);
                        if(!this.IsGold(item))
                        {
                            this.bagpackslots[i].AddItem(item, bagpackTypeFeatures[(int)this.bagpackType]);
                        }
                        else
                        {
                            bool ifGoldInSlots = false;
                            foreach(BagpackSlot bagpackSlot in this.bagpackslots)
                            {
                                if(this.IsGold(bagpackSlot.item))
                                {
                                    this.inventory_gold += item.GoldValue;
                                    ifGoldInSlots = true;
                                    break;
                                }
                            }
                            if(!ifGoldInSlots)
                            {
                                this.bagpackslots[i].AddItem(item, bagpackTypeFeatures[(int)this.bagpackType]);
                                this.inventory_gold += item.GoldValue;
                            }
                        }
                        
                        this.inventory_weight += item.Weight;
                        isAdded = true;
                        break;
                    }
                }
                if (!isAdded)
                {
                    this.ResizeInventorySlots();
                    this.AddToBagpack(item);
                }
                else
                {
                    this.WeightTextValue.text = this.inventory_weight.ToString();
                    this.GoldTextValue.text = this.inventory_gold.ToString();
                }
            }
        }

        private void DeleteFromBagpack(Item item)
        {
            if (item != null)
            {
                try
                {
                    double itemWeight = item.Weight;
                    this.items.Remove(item);
                    this.inventory_weight -= item.Weight;
                    this.RefreshBagpack();
                }
                catch (Exception ex)
                {
                    Debug.Log("Class: 'Bagpack' in 'DeleteFromBagpack' function : " + ex.Message);
                }
            }
        }

        private void ConsumeItem(Item item)
        {
            if (item is ConsumeableItem)
            {
                ConsumeableItem eatableItem = (ConsumeableItem)item;
                eatableItem.Eat(this.bagpackOwner);
            }
            this.DeleteFromBagpack(item);
        }

        private void RepairItem(Item item)
        {
            if (item != null)
            {
                if (item is EquipmentItem)
                {
                    double increaseValue = this.gameObject.GetComponentInParent<RepairInventory>().RepairValue;
                    int increaseCost = this.gameObject.GetComponentInParent<RepairInventory>().RepairTickCost;
                    if (this.IfICanSellBuyItem(this.inventory_gold, increaseCost))
                    {
                        EquipmentItem equipmentItem = (EquipmentItem)item;
                        if (equipmentItem.Durability < 100)
                        {
                            equipmentItem.Repair(increaseValue);
                            this.IncreaseDecreaseGold(-increaseCost);
                        }
                    }
                    else
                    {
                        this.PrintToInfoPanel("You doesnt have enough gold");
                    }
                }
            }
        }

        private void UpgradeItem(Item item)
        {
            if (item != null)
            {
                if (item is EquipmentItem)
                {
                    if (item is Armor)
                    {
                        Armor armor = (Armor)item;

                        int upgradeCost = (int)(this.gameObject.GetComponentInParent<UpgradeInventory>().GetUpgradeTickCost * Math.Exp(armor.UpgradeLevel));
                        if (this.IfICanSellBuyItem(this.inventory_gold, upgradeCost))
                        {
                            if (armor.UpgradeLevel < 5)
                            {
                                armor.LevelUp();
                                this.IncreaseDecreaseGold(-upgradeCost);
                            }
                            else
                            {
                                this.PrintToInfoPanel("Item max level");
                            }
                        }
                        else
                        {
                            if (armor.UpgradeLevel < 5)
                            {
                                this.PrintToInfoPanel("You doesnt have enough gold");
                            }
                            else
                            {
                                this.PrintToInfoPanel("Item max level");
                            }
                        }
                    }
                    else if (item is Weapon)
                    {
                        Weapon weapon = (Weapon)item;

                        int upgradeCost = (int)(this.gameObject.GetComponentInParent<UpgradeInventory>().GetUpgradeTickCost * Math.Exp(weapon.UpgradeLevel));
                        if (this.IfICanSellBuyItem(this.inventory_gold, upgradeCost))
                        {
                            if (weapon.UpgradeLevel < 5)
                            {
                                weapon.LevelUp();
                                this.IncreaseDecreaseGold(-upgradeCost);
                            }
                            else
                            {
                                this.PrintToInfoPanel("Item max level");
                            }
                        }
                        else
                        {
                            if (weapon.UpgradeLevel < 5)
                            {
                                this.PrintToInfoPanel("You doesnt have enough gold");
                            }
                            else
                            {
                                this.PrintToInfoPanel("Item max level");
                            }
                        }
                    }
                }
            }
        }

        private void EquipItem(Item item, ClearSlotCallback clearSlotCallback)
        {
            if(item != null)
            {
                Eq eq = this.gameObject.GetComponentInParent<EqInventory>().ChampionEquipment; 

                if(item is EquipmentItem)
                {
                    EquipmentItem equipmentItem = (EquipmentItem)item;

                    if(!eq.IsCallbacksSeted)
                    {
                        eq.SetCallbacks(this.ActivateFromBagpack);
                    }

                    if(equipmentItem.IsEquiped)
                    {
                        //remove from equipment
                        Item tempItem = eq.RemoveFromEQ(equipmentItem.EquipmentType);
                        if (tempItem != null)
                        {
                            this.AddItem(tempItem);
                        }
                    }
                    else
                    {
                        //add or swap item in equipment 

                        if(eq.AddToEQ(equipmentItem, equipmentItem.EquipmentType))
                        {
                            this.DeleteFromBagpack(item);
                            clearSlotCallback();
                        }

                        if(eq.IsIHaveItemToSwap)
                        {
                            Item swapItem = eq.GetSwapItem();
                            this.AddItem(swapItem);
                            //this.ReloadBagpack();
                        }
                    }
                }
            }
        }

        private void ActivateFromBagpack(Item item, ClearSlotCallback clearSlotCallback)
        {
            switch(this.bagpackType)
            {
                case InvenotryType.EQ:
                    {
                        EquipItem(item, clearSlotCallback);
                        break;
                    }
                case InvenotryType.Normal:
                    {
                        this.ConsumeItem(item);
                        break;
                    }
                case InvenotryType.Repair:
                    {
                        this.RepairItem(item);
                        break;
                    }
                case InvenotryType.Shop:
                    {
                        this.SellBuyItem(item, clearSlotCallback);
                        break;
                    }
                case InvenotryType.Upgrade:
                    {
                        this.UpgradeItem(item);
                        break;
                    }
            }
        }

        private void InfoFromBagpack(Item item)
        {
            //we have info feature (Armor or Weapon or Comsuable)
            this.OpenInfoPanel();
            this.PrintToInfoPanel(this.GetItemInfo(item));
        }

        private void PrintToInfoPanel(string info)
        {
            if(!this.isItemInfoPanelEnabled)
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
            else if (item is ConsumeableItem)
            {
                ConsumeableItem eatableItem = (ConsumeableItem)item;
                string[] info = eatableItem.ToString().Split(';');

                for (int i = 0; i < info.Length; i++)
                {
                    all_info += info[i] + "\n";
                }

            }

            return all_info;
        }

        private void ResizeInventorySlots()
        {
            double inventory_width = this.AllBagpackSlots.GetComponent<RectTransform>().rect.width;
            double slot_width = this.BagpackSlot.GetComponent<RectTransform>().rect.width;
            int slots_to_create_cout = (int)(inventory_width / slot_width);

            for (int i = 0; i < slots_to_create_cout; i++)
            {
                BagpackSlot bagpackSlotClone = Instantiate(this.BagpackSlot, this.AllBagpackSlots.transform).GetComponent<BagpackSlot>();
                this.bagpackslots.Add(bagpackSlotClone);
                this.bagpackslots[this.bagpackslots.Count - 1].gameObject.SetActive(true);

                //callback
                this.bagpackslots[this.bagpackslots.Count - 1].SetDeleteCallback(this.DeleteFromBagpack);
                this.bagpackslots[this.bagpackslots.Count - 1].SetActivateCallback(this.ActivateFromBagpack);
                this.bagpackslots[this.bagpackslots.Count - 1].SetInfoCallback(this.InfoFromBagpack);
            }
        }

        private void PrepareInventory()
        {
            this.ResizeInventorySlots();
            this.inventory_weight = 0;
            this.inventory_gold = 0;
            if (this.items != null)
            {
                for(int i=0; i<this.items.Count; i++)
                {
                    this.AddToBagpack(this.items[i]);
                }
            }
            this.WeightTextValue.text = this.inventory_weight.ToString();
            this.GoldTextValue.text = this.inventory_gold.ToString();

            this.isDirty = false;
        }

        public void SetBagpack(List<Item> items)
        {
            this.ClearData();

            if (this.items != items && items != null)
            {
                this.items = items;
                this.isDirty = true;
                this.IsDataLoaded = true;
            }
        }

        public void SetChampion(Champion champion)
        {
            if(champion != null)
            {
                this.bagpackOwner = champion;
            }
        }

        private void ClearData()
        {
            this.isDirty = false;
            this.IsDataLoaded = false;
            this.isItemInfoPanelEnabled = false;
            this.items = null;
            this.bagpackOwner = null;
            this.inventory_weight = 0;
            this.inventory_gold = 0;
            this.max_weight = 100;
        }

        private void FreeSlots()
        {
            if (this.bagpackslots != null)
            {
                foreach (BagpackSlot slot in this.bagpackslots)
                {
                    try
                    {
                        Destroy(slot.gameObject);
                    }
                    catch(Exception exc)
                    {
                        Debug.Log("Class 'Bagpack' in 'FreeSlots' function: Cannot destroy slot : " + exc.Message);
                    }
                }
                this.bagpackslots = new List<BagpackSlot>();
            }
        }

        public void FreeBagpackMemory()
        {
            this.FreeSlots();
            this.ClearData();
        }

        public void ReloadBagpack()
        {
            if(this.items != null)
            {
                this.FreeSlots();
                this.PrepareInventory();
            }
        }

        private void RefreshBagpack()
        {
            this.WeightTextValue.text = this.inventory_weight.ToString();
            this.GoldTextValue.text = this.inventory_gold.ToString();
        }
    }
}
