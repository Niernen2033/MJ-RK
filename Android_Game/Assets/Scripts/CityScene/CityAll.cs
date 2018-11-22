using Prefabs.Inventory;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityAll : MonoBehaviour
{
    public GameObject normalInventory;
    public GameObject equipmentInventory;
    public Button closeInventoryButton;
    public Button normalInventoryButton;
    public Button equipmentInventoryButton;

    private NormalInventory NormalInventory;
    private EqInventory EqInventory;

    private void Awake()
    {
        this.NormalInventory = this.normalInventory.GetComponent<NormalInventory>();
        this.EqInventory = this.equipmentInventory.GetComponent<EqInventory>();
    }

    public void OpenBagpack()
    {
        if (!this.NormalInventory.IsOpen)
        {
            this.normalInventoryButton.gameObject.SetActive(false);
            this.equipmentInventoryButton.gameObject.SetActive(false);
            this.closeInventoryButton.gameObject.SetActive(true);

            if (this.NormalInventory.PlayerBagpack == null)
            {
                this.NormalInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
            }
            if (this.NormalInventory.PlayerBagpack.IsDataLoaded)
            {
                this.NormalInventory.OpenInventory();
            }
            else
            {
                this.NormalInventory.OpenAndLoadInventory(GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
            }
        }
    }

    public void OpenInventory()
    {
        if (!this.EqInventory.IsOpen)
        {
            this.normalInventoryButton.gameObject.SetActive(false);
            this.equipmentInventoryButton.gameObject.SetActive(false);
            this.closeInventoryButton.gameObject.SetActive(true);

            if (this.EqInventory.PlayerBagpack == null)
            {
                this.EqInventory.OpenAndLoadInventory(GameSave.Instance.Player.Equipment, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
            }
            if (this.EqInventory.PlayerBagpack.IsDataLoaded)
            {
                this.EqInventory.OpenInventory();
            }
            else
            {
                this.EqInventory.OpenAndLoadInventory(GameSave.Instance.Player.Equipment, GameSave.Instance.Player.Bagpack, GameSave.Instance.Player);
            }
        }
    }

    public void CloseInventory()
    {
        if (this.EqInventory != null)
        {
            if (this.equipmentInventory.activeSelf)
            {
                this.EqInventory.CloseInventory();
                GameSave.Instance.Update();
                this.closeInventoryButton.gameObject.SetActive(false);
                this.normalInventoryButton.gameObject.SetActive(true);
                this.equipmentInventoryButton.gameObject.SetActive(true);
            }
        }

        if(this.NormalInventory != null)
        {
            if(this.normalInventory.activeSelf)
            {
                this.NormalInventory.CloseInventory();
                GameSave.Instance.Update();
                this.closeInventoryButton.gameObject.SetActive(false);
                this.normalInventoryButton.gameObject.SetActive(true);
                this.equipmentInventoryButton.gameObject.SetActive(true);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
