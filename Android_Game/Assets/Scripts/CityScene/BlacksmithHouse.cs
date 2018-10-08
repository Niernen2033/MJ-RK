using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlacksmithHouse : MonoBehaviour, IPointerClickHandler
{
    public void Awake()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("klik");
    }
}
