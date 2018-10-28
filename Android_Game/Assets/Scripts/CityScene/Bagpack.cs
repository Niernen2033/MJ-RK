using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Items;

public class Bagpack : MonoBehaviour
{
    Armor armor;
    private void Awake()
    {
        this.armor = new Armor();      
    }
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponentInChildren<BagpackSlot>().AddItem(armor);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
