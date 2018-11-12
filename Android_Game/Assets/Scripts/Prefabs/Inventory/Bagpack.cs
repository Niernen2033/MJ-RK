using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Items;
using System;

namespace Prefabs.Inventory
{
    public enum InvenotryType { Normal, Shop, Repair, EQ };
    public delegate void BagpackDeleteCallback(Item item);
    public delegate void BagpackActivateCallback(Item item);

    public class Bagpack : MonoBehaviour
    {
        public GameObject AllBagpackSlots;
        public BagpackSlot BagpackSlot;
        public Text GoldTextValue;
        public Text WeightTextValue;
        public InvenotryType bagpackType;

        private List<ItemFeaturesType[]> bagpackTypeFeatures;
        private List<BagpackSlot> bagpackslots;
        private List<Item> items;
        private Champion bagpackOwner;
        private bool isDirty;
        private double max_weight;
        private double inventory_weight;
        private double inventory_gold;

        private void Awake()
        {
            this.ClearData();
            this.bagpackTypeFeatures = new List<ItemFeaturesType[]>();

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
            //Resize begining inventory
            this.ResizeInventorySlots();
        }

        // Update is called once per frame
        private void Update()
        {
            if (this.isDirty)
            {
                this.PrepareInventory();
            }
        }

        public void AddItem(Item item)
        {
            if ((this.inventory_weight + item.Weight) > this.max_weight)
            {
                //to heavy
            }
            else
            {
                this.items.Add(item);
                this.AddToBagpack(item);
            }
        }

        private bool IsGold(Item item)
        {
            bool result = false;
            if(item.Icon.Index == (int)ItemIndex.Gold.Large
                || item.Icon.Index == (int)ItemIndex.Gold.Medium
                || item.Icon.Index == (int)ItemIndex.Gold.Small)
            {
                result = true;
            }
            return result;
        }

        private void AddToBagpack(Item item)
        {
            bool isAdded = false;
            for (int i = 0; i < this.bagpackslots.Count; i++)
            {
                if (this.bagpackslots[i].IsEmpty)
                {
                    this.bagpackslots[i].AddItem(item, bagpackTypeFeatures[(int)this.bagpackType], i);
                    this.inventory_weight += item.Weight;
                    if(this.IsGold(item))
                    {
                        this.inventory_gold += item.GoldValue;
                    }
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

        private void DeleteFromBagpack(Item item)
        {
            try
            {
                this.items.Remove(item);
            }
            catch (Exception ex)
            {
                Debug.Log("Class: 'Bagpack' in 'DeleteFromBagpack' function: Cannot delete item | " + ex.Message);
            }
        }

        private void ActivateFromBagpack(Item item)
        {
            if(item.Features.GetAvailableFeatures().Contains(ItemFeaturesType.IsEatAble))
            {
                if(item is EatableItem)
                {
                    EatableItem eatableItem = (EatableItem)item;
                    eatableItem.Eat(this.bagpackOwner);
                    this.DeleteFromBagpack(item);
                }
                else
                {

                }
            }
            else if (item.Features.GetAvailableFeatures().Contains(ItemFeaturesType.IsEquipAble))
            {

            }
            else if (item.Features.GetAvailableFeatures().Contains(ItemFeaturesType.IsRepairAble))
            {

            }
            else if (item.Features.GetAvailableFeatures().Contains(ItemFeaturesType.IsSellAble))
            {

            }
            else if (item.Features.GetAvailableFeatures().Contains(ItemFeaturesType.IsUpgradeAble))
            {

            }
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
                this.bagpackslots[this.bagpackslots.Count - 1].SetActivateCallbac(this.ActivateFromBagpack);
            }
        }

        private void PrepareInventory()
        {
            this.inventory_weight = 0;
            this.inventory_gold = 0;
            if (this.items != null)
            {
                foreach (Item item in this.items)
                {
                    this.AddToBagpack(item);
                }
            }
            this.WeightTextValue.text = this.inventory_weight.ToString();
            this.GoldTextValue.text = this.inventory_gold.ToString();

            this.isDirty = false;
        }

        public void SetBagpack(List<Item> items)
        {
            this.ClearData();

            this.isDirty = false;
            if (this.items != items)
            {
                this.items = items;
                this.isDirty = true;
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
            this.items = null;
            this.bagpackOwner = null;
            this.inventory_weight = 0;
            this.max_weight = 0;

            this.FreeSlots();

            this.bagpackslots = new List<BagpackSlot>();
        }

        public void FreeSlots()
        {
            if (this.bagpackslots != null)
            {
                foreach (BagpackSlot slot in this.bagpackslots)
                {
                    try
                    {
                        Destroy(slot);
                    }
                    catch(Exception exc)
                    {
                        Debug.Log("Class 'Bagpack' in 'FreeSlots' function: Cannot destroy slot : " + exc.Message);
                    }
                }
            }
        }
    }
}
