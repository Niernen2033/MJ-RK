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
    private static DungeonsGenerator dungeonGenerator;
    private static DungeonManager dungeonManager;
    private static ChooseCorridor chooseCorridor;
    private DungeonLevel currentCorridor;
    private int currentCorridorId;
    private int previousCorridorId;
    private static GameObject objectForDarkening;
    private static bool shouldApplyTransition;
    private static SpriteRenderer objectForDarkeningRenderer;
    private static Color alphaColor;

    private static float speedTimer;
    //timeLimit is for making sure that camera is moved every few frames
    private double timeLimit = 0.025;
    private static bool shouldButtonsBeLocked;
    private static int choosenCorridor;
    private static bool wasTransitionCalled;

    [SerializeField]
    private Button useButton;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    // Use this for initialization
    void Start()
    {
        wasTransitionCalled = false;
        shouldButtonsBeLocked = false;
        //We will always start on level 0
        currentCorridorId = 0;
        previousCorridorId = currentCorridorId;
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();
        currentCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId);
        focusedHero = GameObject.Find("HeroObject1");
        chooseCorridor = GameObject.Find("ChooseCorridorPopup").GetComponent<ChooseCorridor>();
        focusedHeroPosition = focusedHero.transform.position.x;
        useButton.onClick.AddListener(doorTransition);
        objectForDarkening = GameObject.Find("Transition");
        objectForDarkeningRenderer = objectForDarkening.GetComponent<SpriteRenderer>();
        shouldApplyTransition = false;
        alphaColor = objectForDarkening.GetComponent<SpriteRenderer>().material.color;
        speedTimer = 0;
        objectForDarkeningRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public int randomizeChoiseOfCorridor()
    {
        int howManyAreThere;
        int randomizedOne;
        ConnectionMap conMap = dungeonCanvas.GetComponent<ConnectionMap>();
        System.Random randomNumber = new System.Random();
        howManyAreThere = conMap.getCorridorDependenciesList()[currentCorridorId].getNeighbourCorridor().Count;
        randomizedOne = randomNumber.Next(0, howManyAreThere);
        string devLog = "Out of following neighbours: ";

        //Debug.Log("Out of following neighbours of corridor ");
        for (int i = 0; i < howManyAreThere; i++)
        {
            //Debug.Log(" " + conMap.getCorridorDependenciesList()[currentCorridorNumber].getSpecificNeighbourCorridor(i));
            devLog += " " + conMap.getCorridorDependenciesList()[currentCorridorId].getSpecificNeighbourCorridor(i).ToString();
        }
        Debug.Log(devLog + " of corridor: " + currentCorridorId);
        return conMap.getCorridorDependenciesList()[currentCorridorId].getSpecificNeighbourCorridor(randomizedOne);
    }

    public void doorTransition()
    {
        if (!shouldButtonsBeLocked)
        {
            currentCorridorId = dungeonGenerator.getIdOfCorridor();//to do reading new nmber
            currentCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId);

            //Here we're calling popup to allow user to pick next corridor
            //For that we're going to send our current corridor id
            if (focusedHeroPosition >= 0 && focusedHeroPosition <= 7)
            {
                if (currentCorridorId == 0)
                {
                    //Here we will need to set returning to town
                }
                else
                {
                    Debug.Log("ButtonForUsage || doorTransition || Registered use on entrance door!");
                    chooseCorridor.showCorridorDecissionPopup(previousCorridorId);
                    shouldButtonsBeLocked = true;
                }
            }
            else if (focusedHeroPosition >= ((currentCorridor.getNumberOfChunks() - 1) * 7) && focusedHeroPosition <= ((currentCorridor.getNumberOfChunks()) * 7))
            {
                Debug.Log("ButtonForUsage || doorTransition || Registered use on exit doors!");
                chooseCorridor.showCorridorDecissionPopup(currentCorridorId);
                shouldButtonsBeLocked = true;
            }


            Debug.Log("ButtonForUsage || doorTransition || (" + focusedHeroPosition + " >= " + ((currentCorridor.getNumberOfChunks() - 1) * 7) + " && " + focusedHeroPosition + " <= " + (currentCorridor.getNumberOfChunks() * 7) + ")");
            Debug.Log("ButtonForUsage || doorTransition || CurrentCorridorNumber is: " + currentCorridorId);
        }
        else
        {
            Debug.Log("ButtonForUsage || doorTransition || Buttons are locked!");
        }
    }

    public void doTransitionPreparation(int choosenCorridorId)
    {
        dungeonGenerator.loadAnotherLevel(choosenCorridorId);
        previousCorridorId = currentCorridorId;
        currentCorridorId = choosenCorridorId;
        Debug.Log("ButtonForUsage || doorTransition || doTransitionPreparation || currentCorridorNumber: " + currentCorridorId);
        Debug.Log("ButtonForUsage || doorTransition || doTransitionPreparation || Length of corridorList from dungeonGenerator: " + dungeonGenerator.getCorridorList().Count);
        currentCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId);
        //shouldApplyTransition = true;
    }

    // Update is called once per frame
    void Update () {
        focusedHeroPosition = focusedHero.transform.position.x;

        speedTimer += Time.deltaTime;
        if (speedTimer > timeLimit)
        {
            transitionDarkerning();
        }

        if (chooseCorridor.getIsCorridorChoosen())
        {
            choosenCorridor = chooseCorridor.getChoosenCorridorId();
            chooseCorridor.setIsCorridorChoosen(false);
            shouldApplyTransition = true;
            //It is used to inform that transition was called and it didnt's ended cycle yet
            wasTransitionCalled = true;
        }

        //Alpha color and shouldApplyTransition(second part of cycle) are do make sure the cycle ended and was initiaded
        if (alphaColor.a == 0 && shouldButtonsBeLocked && wasTransitionCalled && !shouldApplyTransition)
        {
            shouldButtonsBeLocked = false;
            wasTransitionCalled = false;
        }

        //Debug.Log("ButtonForUsage || doorTransition || doTransitionPreparation || " + shouldButtonsBeLocked + " " + alphaColor.a);
    }

    public void transitionDarkerning()
    {
        if (alphaColor.a < 1.0f && shouldApplyTransition == true)
        {
            alphaColor.a += 0.02f;
            //Debug.Log("Alpha ->: " + alphaColor.a);
        }
        else if (alphaColor.a >= 1.0f && shouldApplyTransition == true)//">" for making sure
        {
            shouldApplyTransition = false;
            doTransitionPreparation(choosenCorridor);
            //Debug.Log("Beta ->: ");
        }
        else if (alphaColor.a > 0.0f && shouldApplyTransition == false)
        {
            alphaColor.a -= 0.02f;
            //Debug.Log("Gamma ->: " + alphaColor.a);
        }
        else if (alphaColor.a < 0.0f && shouldApplyTransition == false)
        {
            alphaColor.a = 0.00f;
        }
        objectForDarkeningRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, alphaColor.a);

        //trans.color = new Color(1.0f, 1.0f, 1.0f, alphaColor.a);
        speedTimer = 0;
    }

    public void setShouldButtonsBeLocked(bool shouldThey)
    {
        shouldButtonsBeLocked = shouldThey;
    }

    public bool getShouldButtonsBeLocked()
    {
        return shouldButtonsBeLocked;
    }
}
