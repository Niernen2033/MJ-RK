using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonForUsage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject focusedHero;
    private float focusedHeroPosition;
    private GameObject dungeonCanvas;
    private DungeonsGenerator dungeonGenerator;
    private Corridor currentCorridor;
    private int currentCorridorNumber;
    private static GameObject objectForDarkening;
    private static bool shouldApplyTransition;
    private static SpriteRenderer objectForDarkeningRenderer;
    private static Color alphaColor;

    private static float speedTimer;
    //timeLimit is for making sure that camera is moved every few frames
    private double timeLimit = 0.025;

    [SerializeField]
    private Button useButton;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public int randomizeChoiseOfCorridor()
    {
        System.Random randomNumber = new System.Random();
        return randomNumber.Next(0, 1);
    }

    public void doorTransition()
    {
        currentCorridorNumber = dungeonGenerator.getIdOfCorridor();//to do reading new nmber
        currentCorridor = dungeonGenerator.getCorridorFromList(currentCorridorNumber);

        //For development purposes we're gonna get random corridor to go. Normally we could choose from few of them.
        //Now we should randomize choise. I'll make function for that
        int rand = randomizeChoiseOfCorridor();//here additionaly use randomized number to acces this object from connectionmap

        if (focusedHeroPosition >= 0 && focusedHeroPosition <= 7)
        {
            Debug.Log("Registered use on entrance door!");
            dungeonGenerator.loadAnotherLevel(rand);
            shouldApplyTransition = true;
        }
        else if (focusedHeroPosition >= (currentCorridor.getCorridorLength() - 1) * 7 && focusedHeroPosition <= currentCorridor.getCorridorLength() * 7)
        {
            Debug.Log("Registered use on exit doors!");
            dungeonGenerator.loadAnotherLevel(rand);
            shouldApplyTransition = true;
        }
        Debug.Log("CurrentCorridorNumber is: " + currentCorridorNumber);
    }

    // Use this for initialization
    void Start () {
        currentCorridorNumber = 0;//to do reading new nmber
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        currentCorridor = dungeonGenerator.getCorridorFromList(currentCorridorNumber);
        focusedHero = GameObject.Find("HeroObject1");
        focusedHeroPosition = focusedHero.transform.position.x;
        useButton.onClick.AddListener(doorTransition);
        objectForDarkening = GameObject.Find("Transition");
        objectForDarkeningRenderer = objectForDarkening.GetComponent<SpriteRenderer>();
        //trans = objectForDarkening.GetComponent<Image>();
        shouldApplyTransition = false;
        alphaColor = objectForDarkening.GetComponent<SpriteRenderer>().material.color;
        Debug.Log("Zee ->: " + alphaColor.a);
        speedTimer = 0;
        objectForDarkeningRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        //trans.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        //objectForDarkeningRenderer.
    }

    // Update is called once per frame
    void Update () {
        focusedHeroPosition = focusedHero.transform.position.x;

        speedTimer += Time.deltaTime;
        if (speedTimer > timeLimit)
        {
            transitionDarkerning();
        }
    }

    public void transitionDarkerning()
    {
        if (alphaColor.a < 1.0f && shouldApplyTransition == true)
        {
            alphaColor.a += 0.02f;
            Debug.Log("Alpha ->: " + alphaColor.a);
        }
        else if (alphaColor.a >= 1.0f && shouldApplyTransition == true)//">" for making sure
        {
            shouldApplyTransition = false;
            Debug.Log("Beta ->: ");
        }
        else if (alphaColor.a > 0.0f && shouldApplyTransition == false)
        {
            alphaColor.a -= 0.02f;
            Debug.Log("Gamma ->: " + alphaColor.a);
        }
        objectForDarkeningRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, alphaColor.a);
        //trans.color = new Color(1.0f, 1.0f, 1.0f, alphaColor.a);
        speedTimer = 0;
    }
}
