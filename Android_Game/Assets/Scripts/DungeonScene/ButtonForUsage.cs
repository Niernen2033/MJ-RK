using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonForUsage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject focusedHero;
    private float focusedHeroPosition;
    private GameObject dungeonCanvas;
    private DungeonsGenerator dungeonGenerator;
    private Corridor currentCorridor;
    private int currentCorridorNumber;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(focusedHeroPosition>=0 && focusedHeroPosition<=7)
        {
            Debug.Log("Mieści się w drzwiach wejściowych!");
        }
        else if (focusedHeroPosition >= (currentCorridor.getCorridorLength()-1)*7 && focusedHeroPosition <= currentCorridor.getCorridorLength() * 7)
        {
            Debug.Log("Mieści się w drzwiach wyjściowych!");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    // Use this for initialization
    void Start () {
        currentCorridorNumber = 0;
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        currentCorridor = dungeonGenerator.getCorridorFromList(currentCorridorNumber);
        focusedHero = GameObject.Find("HeroObject1");
        focusedHeroPosition = focusedHero.transform.position.x;
        Debug.Log("----" + currentCorridor.getCorridorLength());
    }

    // Update is called once per frame
    void Update () {
        focusedHeroPosition = focusedHero.transform.position.x;
    }
}
