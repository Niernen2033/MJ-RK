using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Items;
using SaveLoad;

public class BagpackSlot : MonoBehaviour
{
    private Image icon;
    private Image iconRarity;

    private void Awake()
    {
        this.icon = this.gameObject.GetComponentsInChildren<Image>()[1];
        this.iconRarity = this.gameObject.GetComponentsInChildren<Image>()[2];
    }

    public void AddItem(Item newItem)
    {
        string iconPath = SaveInfo.Paths.Resources.Images.Inventory.AllItems;
        int iconIndex = (int)ItemIcon.RangedArmor.Weapon.Crossbow1;
        int iconRarityIndex = (int)ItemIcon.ItemClass.Legendary;
        /*
        if(!int.TryParse(newItem.ItemIconName.Replace("all_items_", ""), out iconIndex))
        {
            iconIndex = 6;
        }*/
        this.icon.sprite = Resources.LoadAll<Sprite>(iconPath)[iconIndex];
        this.iconRarity.sprite = Resources.LoadAll<Sprite>(iconPath)[iconRarityIndex];
        this.icon.enabled = true;
        this.iconRarity.enabled = true;
    }

    public void ClearSlot()
    {
        this.icon.sprite = null;
        this.icon.enabled = false;
        this.iconRarity.sprite = null;
        this.iconRarity.enabled = false;
    }
}
