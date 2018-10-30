using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using SaveLoad;

public class Inventory : MonoBehaviour
{
    private List<Item> items;
    private bool isDirty;
    private double max_inventory_weight;
    private double inventory_weight;

    private void Awake()
    {
        this.ClearData();
    }

    // Use this for initialization
    private void Start ()
    {

	}

    // Update is called once per frame
    private void Update ()
    {
		if(this.isDirty)
        {
            this.PrepareInventory();
        }
	}

    public void AddItem(Item item)
    {
        if((this.inventory_weight + item.Weight) > this.max_inventory_weight)
        {
            //to heavy
        }
        else
        {
            this.AddToInventory(item);
        }
    }

    private void AddToInventory(Item item)
    {
        BagpackSlot[] bagpackSlots = this.gameObject.GetComponentsInChildren<BagpackSlot>();
        if (item.Icon.SlotIndex != -1)
        {
            bagpackSlots[item.Icon.SlotIndex].AddItem(item);
        }
        else
        {
            for (int i = 0; i < bagpackSlots.Length; i++)
            {
                if (bagpackSlots[i].IsEmpty)
                {
                    bagpackSlots[i].AddItem(item);
                    item.Icon.SlotIndex = i;
                    this.inventory_weight += item.Weight;
                    break;
                }
            }
        }
    }

    private void PrepareInventory()
    {
        this.inventory_weight = 0;
        if (this.items != null)
        {
            foreach (Item item in this.items)
            {
                this.AddToInventory(item);
            }
        }

        this.isDirty = false;
    }

    public void Open(List<Item> items, Vector2 position)
    {
        this.gameObject.SetActive(true);

        this.isDirty = false;
        this.gameObject.GetComponentInChildren<Bagpack>().transform.position = position;
        if (this.items != items)
        {
            this.items = items;
            this.isDirty = true;
        }
    }

    public void Close()
    {
        this.ClearData();
        this.gameObject.SetActive(false);
    }

    private void ClearData()
    {
        this.isDirty = false;
        this.items = null;
        this.inventory_weight = 0;
        this.max_inventory_weight = 0;
    }
}
