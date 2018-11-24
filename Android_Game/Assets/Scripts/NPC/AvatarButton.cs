using CityScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NPC
{
    public class AvatarButton : MonoBehaviour, IPointerClickHandler
    {
        public int AvatarIndex;
        public bool IsSelected { get; set; }

        private Image selectedSprite;
        private bool isNeedToReload;

        public void OnPointerClick(PointerEventData eventData)
        {
            this.IsSelected = true;
            this.gameObject.GetComponentInParent<CityAll>().SetChooseChampion(this.AvatarIndex);
        }

        private void Awake()
        {
            this.selectedSprite = this.gameObject.GetComponentsInChildren<Image>()[1];
            this.selectedSprite.gameObject.SetActive(false);
            this.IsSelected = false;
            this.isNeedToReload = true;
        }

        public void ReloadImage()
        {
            this.isNeedToReload = true;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (this.isNeedToReload)
            {
                if (this.IsSelected == false)
                {
                    this.selectedSprite.gameObject.SetActive(false);
                }
                else
                {
                    this.selectedSprite.gameObject.SetActive(true);
                }
                this.isNeedToReload = false;
            }
        }
    }
}
