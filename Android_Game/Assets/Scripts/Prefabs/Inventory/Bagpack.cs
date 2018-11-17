using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Items;
using System;
using System.Runtime.InteropServices;

namespace Prefabs.Inventory
{
    public enum BagpackType { Player, Shop };

    public delegate void BagpackDeleteCallback(Item item, int index);
    public delegate void BagpackActivateCallback(Item item, int index);
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

        public event EventHandler<BagpackEventArgs> SellItem;

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

        protected void OnSellItem(BagpackEventArgs bagpackEventArgs)
        {
            this.SellItem?.Invoke(this, bagpackEventArgs);
        }

        public void AddItem(Item item)
        {
            if (item != null)
            {
                if ((this.inventory_weight + item.Weight) > this.max_weight)
                {
                    //to heavy
                }
                else
                {
                    if (this.items == null)
                    {
                        this.items = new List<Item>();
                        Debug.Log("Bagpack is not set to a instance of class");
                    }
                    this.items.Add(item);
                    this.AddToBagpack(this.items[this.items.Count - 1], this.items.Count - 1);
                }
            }
        }

        private bool IfICanSellItem(int bagpackGold, int itemGold)
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
        /*
        private void SellItem(Item item, int index)
        {
            if (item != null)
            {
                Bagpack PlayerBagpack = this.gameObject.GetComponentInParent<ShopInventory>().PlayerBagpack;
                Bagpack ShopBagpack = this.gameObject.GetComponentInParent<ShopInventory>().ShopBagpack;
                if (PlayerBagpack == this)
                {
                    //this is player bagpack
                    ShopBagpack.AddItem(item);
                    PlayerBagpack.DeleteFromBagpack(item, index);
                }
                else
                {
                    //this is shop bagpack
                    if (this.IfICanSellItem(PlayerBagpack.inventory_gold, item.GoldValue))
                    {
                        PlayerBagpack.AddItem(item);
                        ShopBagpack.DeleteFromBagpack(item, index);
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
        */
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



        private void AddToBagpack(Item item, int index)
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
                            this.bagpackslots[i].AddItem(item, bagpackTypeFeatures[(int)this.bagpackType], index);
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
                                this.bagpackslots[i].AddItem(item, bagpackTypeFeatures[(int)this.bagpackType], index);
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
                    this.AddToBagpack(item, index);
                }
                else
                {
                    this.WeightTextValue.text = this.inventory_weight.ToString();
                    this.GoldTextValue.text = this.inventory_gold.ToString();
                }
            }
        }

        private void DeleteFromBagpack(Item item, int index)
        {
            if (item != null)
            {
                try
                {
                    //Debug.Log("Kasuje item o indexie: " + index);
                    this.items.RemoveAt(index);
                }
                catch (Exception ex)
                {
                    Debug.Log("Class: 'Bagpack' in 'DeleteFromBagpack' function: Cannot delete item | " + ex.Message);
                }
            }
        }

        private void ActivateFromBagpack(Item item, int index)
        {
            switch(this.bagpackType)
            {
                case InvenotryType.EQ:
                    {
                        break;
                    }
                case InvenotryType.Normal:
                    {
                        if (item is EatableItem)
                        {
                            EatableItem eatableItem = (EatableItem)item;
                            eatableItem.Eat(this.bagpackOwner);
                        }
                        this.DeleteFromBagpack(item, index);

                        break;
                    }
                case InvenotryType.Repair:
                    {
                        break;
                    }
                case InvenotryType.Shop:
                    {
                        //this.SellItem(item, index);
                        BagpackType bagpack;
                        if (this.gameObject.GetComponentInParent<ShopInventory>().PlayerBagpack == this)
                        {
                            bagpack = BagpackType.Player;
                        }
                        else
                        {
                            bagpack = BagpackType.Shop;
                        }
                        this.OnSellItem(new BagpackEventArgs(bagpack));
                        break;
                    }
                case InvenotryType.Upgrade:
                    {
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
            else if (item is EatableItem)
            {
                EatableItem eatableItem = (EatableItem)item;
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
                    this.AddToBagpack(this.items[i], i);
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
            this.max_weight = 0;
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
    }
    public class BagpackEventArgs : EventArgs
    {
        public BagpackType BagpackType { get; private set; }

        public BagpackEventArgs(BagpackType bagpackType)
        {
            this.BagpackType = bagpackType;
        }
    }
}
