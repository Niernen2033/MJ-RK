using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using SaveLoad;

namespace Prefabs.Inventory
{
    public class NormalInventory : MonoBehaviour
    {
        public Bagpack Bagpack { get; private set; }

        private void Awake()
        {      
            this.Bagpack = this.gameObject.GetComponentInChildren<Bagpack>();
        }

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void OpenClose()
        {
            if (this.gameObject.activeSelf == true)
            {
                this.Bagpack.FreeSlots();
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
