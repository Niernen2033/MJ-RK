using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonForCameraMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject dungeonCanvas;
    private DungeonsGenerator dungeonGenerator;
    private static GameObject[] heroesObjects;
    private SpriteRenderer render;

    private bool isButtonPressed;
    private float speedTimer;
    //timeLimit is for making sure that camera is moved every few frames
    private double timeLimit = 0.125;
    private int sizeOfParty = 4;
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

    public void Start()
    {
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        isFacingRight = true;

        heroesObjects = new GameObject[sizeOfParty];

        for (int i = 0; i < sizeOfParty; i++)
        {
            heroesObjects[i] = GameObject.Find("HeroObject" + (i + 1).ToString());
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

        if (isButtonPressed)
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
                            for (int i = 0; i < sizeOfParty; i++)
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
                            for(int i=0;i< sizeOfParty;i++)
                            {
                                Debug.Log(" " + heroesObjects[i].transform.position.x + ",");
                            }
                        }
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
                            for (int i = 0; i < sizeOfParty; i++)
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
}