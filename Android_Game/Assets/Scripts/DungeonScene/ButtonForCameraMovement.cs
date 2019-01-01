using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonForCameraMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject dungeonCanvas;
    private DungeonsGenerator dungeonGenerator;
    private static List<GameObject> heroesObjects;
    private SpriteRenderer render;
    private static ButtonForUsage buttonForUsage;

    private bool isButtonPressed;
    private float speedTimer;
    //timeLimit is for making sure that camera is moved every few frames
    private double timeLimit = 0.125;
    //We will need to read it from the city level then it will be hub to share this information ( that is -> Fight mode)
    //also mind that it will need to be updated when one of the heroes dies or sth;
    private static int sizeOfParty = 4;
    private float focusedHeroPosition;
    private static bool isFacingRight;

    [SerializeField]
    private short cameraMovementDirection;
    //There are two movement directions -1 (left direction) and 1 (right direction)

    [SerializeField]
    private UnityEvent buttonHold;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        speedTimer = 0;
    }

    public void getThemToTheEntrance()
    {
        Camera.main.transform.Translate(new Vector3(-heroesObjects[0].transform.position.x, 0, 0));
    }

    public void getThemToTheExit()
    {
        Camera.main.transform.Translate(new Vector3(dungeonGenerator.getRightBoundPossition()-heroesObjects[0].transform.position.x, 0, 0));
    }

    public void getThemStepBackAfterFight()
    {
        Camera.main.transform.Translate(Vector2.left);
    }

    public void Start()
    {
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        buttonForUsage = GameObject.Find("UseButton").GetComponent<ButtonForUsage>();
        isFacingRight = true;

        heroesObjects = new List<GameObject>();

        for (int i = 0; i < sizeOfParty; i++)
        {
            heroesObjects.Add(GameObject.Find("HeroObject" + (i + 1).ToString()));
        }

        focusedHeroPosition = heroesObjects[0].transform.position.x;
    }

    public void Update()
    {
        if (isFacingRight == true)
        {
            //focusedHeroPosition = heroesObjects[0].transform.position.x;
            focusedHeroPosition = heroesObjects[0].transform.position.x;
        }
        else
        {
            focusedHeroPosition = heroesObjects[1].transform.position.x;
        }

        //If button is pressed and there is no buttons lock from useButton due to corridor choosing and transition
        if (isButtonPressed && !buttonForUsage.getShouldButtonsBeLocked())
        {
            speedTimer += Time.deltaTime;
            if (speedTimer > timeLimit)
            {
                if (cameraMovementDirection == 1)
                {
                    if (Camera.main.transform.position.x < dungeonGenerator.getRightBoundPossition())
                    {

                        if (isFacingRight == false)
                        {
                            Debug.Log("ButtonForCameraMovement || Camera x:" + focusedHeroPosition);
                            for (int i = 0; i < heroesObjects.Count; i++)
                            {
                                if (heroesObjects[i].transform.position.x < Camera.main.transform.position.x)
                                {
                                    heroesObjects[i].transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.position.x - heroesObjects[i].transform.position.x), heroesObjects[i].transform.position.y, heroesObjects[i].transform.position.z);
                                }
                                else
                                {
                                    heroesObjects[i].transform.position = new Vector3(Camera.main.transform.position.x - (heroesObjects[i].transform.position.x - Camera.main.transform.position.x), heroesObjects[i].transform.position.y, heroesObjects[i].transform.position.z);
                                }
                                render = heroesObjects[i].GetComponent<SpriteRenderer>();
                                render.flipY = false;
                            }
                            isFacingRight = true;
                            Debug.Log("ButtonForCameraMovement || Last camera coords: " + Camera.main.transform.position.x + " and hero coords ");
                            for (int i = 0; i < heroesObjects.Count; i++)
                            {
                                Debug.Log(" " + heroesObjects[i].transform.position.x + ",");
                            }
                        }
                        //Moving party by one step to the right
                        Camera.main.transform.Translate(Vector2.right);
                    }
                }
                else
                if (cameraMovementDirection == -1)
                {
                    if (Camera.main.transform.position.x > 0)
                    {

                        if (isFacingRight == true)
                        {
                            for (int i = 0; i < heroesObjects.Count; i++)
                            {
                                if (heroesObjects[i].transform.position.x < Camera.main.transform.position.x)
                                {
                                    heroesObjects[i].transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.position.x - heroesObjects[i].transform.position.x), heroesObjects[i].transform.position.y, heroesObjects[i].transform.position.z);
                                }
                                else
                                {
                                    heroesObjects[i].transform.position = new Vector3(Camera.main.transform.position.x - (heroesObjects[i].transform.position.x - Camera.main.transform.position.x), heroesObjects[i].transform.position.y, heroesObjects[i].transform.position.z);
                                }
                                render = heroesObjects[i].GetComponent<SpriteRenderer>();
                                render.flipY = true;
                            }
                            isFacingRight = false;
                            Debug.Log("ButtonForCameraMovement ||  Last camera coords: " + Camera.main.transform.position.x + " and hero coords " + heroesObjects[0].transform.position.x);
                        }
                        Camera.main.transform.Translate(Vector2.left);
                    }
                }
                speedTimer = 0;
            }
        }
        //Debug.Log("Is facing right(koniec): " + isFacingRight);
    }

    public void setSizeOfParty(int sizeOfPartyToSet)
    {
        sizeOfParty = sizeOfPartyToSet;
    }

    public void setHeroesObjects(List<GameObject> heroesObjectsToSet)
    {
        heroesObjects = heroesObjectsToSet;
    }

    public void setIsButtonPressed(bool isIt)
    {
        isButtonPressed = isIt;
    }

    public int getSizeOfParty()
    {
        return sizeOfParty;
    }

    public List<GameObject> getHeroesObjects()
    {
        return heroesObjects;
    }

    public bool getIsButtonPressed()
    {
        return isButtonPressed;
    }
}