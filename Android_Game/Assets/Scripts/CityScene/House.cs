using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CityScene
{
    public class House : MonoBehaviour, IPointerClickHandler
    {
        public City.ObjectType houseType;
        private OpenBuildingCallback openBuildingCallback;

        public void Awake()
        {
            this.openBuildingCallback = GetComponentInParent<City>().OpenBuilding;
        }

        public void Start()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
           this.openBuildingCallback(this.houseType);
        }
    }
}
