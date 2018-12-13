using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForChargingAtack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private static GameObject dungeonCanvas;
    private static GameObject buttonForAtackCharging;
    private static string filePath;
    private static string[] typeOfElement;

    // Use this for initialization
    void Start () {
        dungeonCanvas = GameObject.Find("Dungeon");
        buttonForAtackCharging = GameObject.Find("ChargeAtackButton");
        buttonForAtackCharging.SetActive(false);

        filePath = "UI_Elements/";
        typeOfElement = new string[]{ "ChargeBar", "ChargeBarPointer" };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void displayChargeBar()
    {

    }
}
