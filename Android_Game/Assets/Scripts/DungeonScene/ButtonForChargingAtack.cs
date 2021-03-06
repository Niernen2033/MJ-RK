﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForChargingAtack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private static GameObject dungeonCanvas;
    private static GameObject buttonForAtackCharging;
    private static ObjectSelector objectSelector;
    private static DungeonManager dungeonManager;
    private static DisplayParty displayParty;
    private static DungeonsGenerator dungeonsGenerator;
    private static FightMode fightMode;
    private static string filePath;
    private static string[] typeOfElement;
    private static GameObject factoryObject;
    private static bool buttonIsHeld;
    private static GameObject chargeBar;
    private static GameObject chargeBarPointer;
    private static float chargeBarWidth, chargeBarHeight;
    private static bool chargeReachedPeak;
    private static float speedTimer;
    private static float chargeMultiplier;
    private static bool enemyIsWipedOut;
    //timeLimit is for actualization every few frames
    private const double timeLimit = 0.025;
    private const float speedOfCharging = 0.1f;

    //For dealDamageToEnemy (making sure race won't occure)
    private static int idOfCorridor;
    private static int idOfEnemyParty;
    private static int idOfChoosenEnemy;

    //Dev purpose
    private System.Random randomNumber;

    // Use this for initialization
    void Start()
    {
        dungeonCanvas = GameObject.Find("Dungeon");
        buttonForAtackCharging = GameObject.Find("ChargeAtackButton");
        objectSelector = dungeonCanvas.GetComponent<ObjectSelector>();
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();
        displayParty = dungeonCanvas.GetComponent<DisplayParty>();
        dungeonsGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        fightMode = dungeonCanvas.GetComponent<FightMode>();
        buttonForAtackCharging.SetActive(false);
        buttonIsHeld = false;
        chargeReachedPeak = false;
        speedTimer = 0;
        randomNumber = new System.Random();

        filePath = "UI_Elements/";
        typeOfElement = new string[] { "ChargeBar", "ChargeBarPointer" };

        if (GameObject.Find("FactoryObject") == null)
        {
            factoryObject = new GameObject();
            factoryObject.name = "FactoryObject";
            factoryObject.layer = 0;
            factoryObject.AddComponent<SpriteRenderer>();
            Debug.Log("ButtonForChargingAtack || Start || Created new FactoryObject");
        }
        else
        {
            Debug.Log("ButtonForChargingAtack || Start || Found FactoryObject");
            factoryObject = GameObject.Find("FactoryObject");
        }

        initializeChargeBar();
        loadChargeBarSize();
    }

    // Update is called once per frame
    void Update()
    {
        speedTimer += Time.deltaTime;
        if (speedTimer > timeLimit)
        {
            chargeItNow();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonIsHeld = true;
        chargeReachedPeak = false;
        displayChargeBar();
        actualizeChargeBarPossition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonIsHeld = false;
        hideChargeBar();

        //Calling enemy turn
        //For development purposes we're gonna randomize dmg dealt and target of atack
        displayParty.dealDamageToHero(randomNumber.Next(0,displayParty.getNumberOfHeroesAlive()), randomNumber.Next(9,30));
    }

    public void initializeChargeBar()
    {
        GameObject tempObject;
        SpriteRenderer spriteRenderer;
        for (int i = 0; i < 2; i++)
        {
            tempObject = Instantiate(factoryObject);
            spriteRenderer = tempObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>(filePath + typeOfElement[i]);
            tempObject.name = typeOfElement[i] + "Object";
            tempObject.SetActive(false);

            if(i==0)
            {
                spriteRenderer.sortingOrder = 0;
                chargeBar = tempObject;
            }
            else
            {
                spriteRenderer.sortingOrder = 1;
                chargeBarPointer = tempObject;
            }
        }
        Debug.Log("ButtonForChargingAtack || initializeChargeBar || ChargeBarInitialized!");
    }

    public void destroyChargeBar()
    {
        Debug.Log("ButtonForChargingAtack || destroyChargeBar || Trying to destroy bar elements");

        if (buttonIsHeld == false)
        {
            if (GameObject.Find(typeOfElement[0] + "Object") != null)
            {
                Destroy(GameObject.Find(typeOfElement[0] + "Object"));
                Debug.Log("ButtonForChargingAtack || destroyChargeBar || Bar destroyed!");
            }
            if (GameObject.Find(typeOfElement[1] + "Object") != null)
            {
                Destroy(GameObject.Find(typeOfElement[1] + "Object"));
                Debug.Log("ButtonForChargingAtack || destroyChargeBar || BarPointer destroyed!");
            }
        }
    }

    public void displayChargeBar()
    {
        chargeBar.SetActive(true);
        chargeBarPointer.SetActive(true);
        Debug.Log("ButtonForChargingAtack || displayChargeBar || ChargeBar displayed!");
    }

    public void hideChargeBar()
    {
        Debug.Log("ButtonForChargingAtack || destroyChargeBar || chargeMultiplier " + (int)(chargeMultiplier*100/3.999998));
        bool initialized = false;
        initialized = dealDamageToMarkedEnemy();

        //If choosen enemy is not last in the queue
        if (dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().Count != idOfChoosenEnemy)
        {
            StartCoroutine(WaitForDamageDealInitialization(initialized));
            chargeMultiplier = 0;
        }
        else if (dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().Count != 0)
        {
            objectSelector.initializeHighlightOnFirstEnemy();
        }
        else
        {
            GameObject.Find("EnemyHighlightMaskObject").SetActive(false);
            dungeonsGenerator.loadAnotherLevel(fightMode.getCurrentCorridorId(), 2);
        }

        //Destroying empty party
        if (dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().Count == 0)
        {
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().RemoveAt(idOfEnemyParty);
            Debug.Log("ButtonForChargingAtack || dealDamageToMarkedEnemy || Party destroyed!");
        }

        chargeBar.SetActive(false);
        chargeBarPointer.SetActive(false);
        Debug.Log("ButtonForChargingAtack || hideChargeBar || ChargeBar hidden!");
    }

    public bool dealDamageToMarkedEnemy()
    {
        idOfCorridor = objectSelector.getIdOfCorridor();
        idOfEnemyParty = objectSelector.getIdOfEnemyParty();
        idOfChoosenEnemy = objectSelector.getIdOfChoosenEnemy();

        Debug.Log("ButtonForChargingAtack || dealDamageToMarkedEnemy || HP BEFORE: " + dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray()[idOfChoosenEnemy]);

        dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray()[idOfChoosenEnemy] -= (int)(chargeMultiplier * 100 / 3.999998);
        if(dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray()[idOfChoosenEnemy]<0)
        {
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray()[idOfChoosenEnemy] = 0;
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].destroyEnemyObject(idOfCorridor, idOfEnemyParty, idOfChoosenEnemy);
            objectSelector.initializeHighlightOnFirstEnemy();
        }
        return true;
    }

    IEnumerator WaitForDamageDealInitialization(bool loadingHasFinished)
    {
        yield return new WaitUntil(() => loadingHasFinished == true);
        GameObject tempObject = GameObject.Find("EnemyHealthBar_" + idOfEnemyParty + "." + idOfChoosenEnemy);

        tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x,
            (500 * ((float)dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray()[idOfChoosenEnemy] /
            (float)dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyMaxHealthArray()[idOfChoosenEnemy]))
            , tempObject.transform.localScale.z);
    }

    public void actualizeChargeBarPossition()
    {
        chargeBar.transform.position = new Vector3(Camera.main.transform.position.x, chargeBar.transform.position.y, chargeBar.transform.position.z);
        chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBar.transform.position.y - chargeBarHeight/2, chargeBarPointer.transform.position.z);
    }

    public void loadChargeBarSize() {
        chargeBarHeight = (float)chargeBar.GetComponent<SpriteRenderer>().bounds.size.y;
        chargeBarWidth = (float)chargeBar.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void chargeItNow()
    {
        if (buttonIsHeld == true)
        {
            if (chargeReachedPeak == false && chargeBarPointer.transform.position.y < (Camera.main.transform.position.y + chargeBarHeight / 2))
            {
                chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBarPointer.transform.position.y + speedOfCharging, chargeBarPointer.transform.position.z);
                chargeMultiplier += speedOfCharging;
            } else if (chargeReachedPeak == false && chargeBarPointer.transform.position.y >= (Camera.main.transform.position.y + chargeBarHeight / 2))
            {
                chargeReachedPeak = true;
            } else if(chargeReachedPeak == true && chargeBarPointer.transform.position.y > (Camera.main.transform.position.y - chargeBarHeight / 2))
            {
                chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBarPointer.transform.position.y - speedOfCharging, chargeBarPointer.transform.position.z);
                chargeMultiplier -= speedOfCharging;
            }else if (chargeReachedPeak == true && chargeBarPointer.transform.position.y <= (Camera.main.transform.position.y - chargeBarHeight / 2))
            {
                chargeReachedPeak = false;
                buttonIsHeld = false;
                chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBar.transform.position.y - chargeBarHeight / 2, chargeBarPointer.transform.position.z);
            }
        }
    }
}
