using Prefabs.Inventory;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAll : MonoBehaviour
{
    public GameObject normalInventory;
    public GameObject equipmentInventory;

    private NormalInventory NormalInventory;
    private EqInventory EqInventory;

    private void Awake()
    {
        this.NormalInventory = this.normalInventory.GetComponent<NormalInventory>();
        this.EqInventory = this.equipmentInventory.GetComponent<EqInventory>();
    }

    public void OpenBagpack()
    {

    }

    public void OpenInventory()
    {
        if (!this.EqInventory.IsOpen && !this.NormalInventory.IsOpen)
        {
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
        else if (this.EqInventory.IsOpen && !this.NormalInventory.IsOpen)
        {
            this.EqInventory.CloseInventory();
            GameSave.Instance.Update();
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
