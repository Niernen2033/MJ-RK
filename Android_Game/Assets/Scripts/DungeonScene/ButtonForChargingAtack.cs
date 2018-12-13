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
    private static GameObject factoryObject;
    private static bool buttonIsHeld;
    private static GameObject chargeBar;
    private static GameObject chargeBarPointer;
    private static float chargeBarWidth, chargeBarHeight;
    private static bool chargeReachedPeak;
    private static float speedTimer;
    //timeLimit is for actualization every few frames
    private const double timeLimit = 0.025;
    private const float speedOfCharging = 0.1f;

    // Use this for initialization
    void Start()
    {
        dungeonCanvas = GameObject.Find("Dungeon");
        buttonForAtackCharging = GameObject.Find("ChargeAtackButton");
        buttonForAtackCharging.SetActive(false);
        buttonIsHeld = false;
        chargeReachedPeak = false;
        speedTimer = 0;

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
            //Should't get in here
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

    /*
    IEnumerator waitForInitialization()
    {
        yield return new WaitUntil(() => booleanVariableForCoroutine == true);
        chargeBar = GameObject.Find(typeOfElement[0] + "Object");
        chargeBarPointer = GameObject.Find(typeOfElement[1] + "Object");
        Debug.Log("ButtonForChargingAtack || initializeChargeBar || Creating hooks");
        chargeBar.SetActive(false);
        chargeBarPointer.SetActive(false);
        booleanVariableForCoroutine = false;
    }*/

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
        chargeBar.SetActive(false);
        chargeBarPointer.SetActive(false);
        Debug.Log("ButtonForChargingAtack || hideChargeBar || ChargeBar hidden!");
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
            } else if (chargeReachedPeak == false && chargeBarPointer.transform.position.y >= (Camera.main.transform.position.y + chargeBarHeight / 2))
            {
                chargeReachedPeak = true;
            } else if(chargeReachedPeak == true && chargeBarPointer.transform.position.y > (Camera.main.transform.position.y - chargeBarHeight / 2))
            {
                chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBarPointer.transform.position.y - speedOfCharging, chargeBarPointer.transform.position.z);
            }else if (chargeReachedPeak == true && chargeBarPointer.transform.position.y <= (Camera.main.transform.position.y - chargeBarHeight / 2))
            {
                chargeReachedPeak = false;
                buttonIsHeld = false;
                chargeBarPointer.transform.position = new Vector3(Camera.main.transform.position.x, chargeBar.transform.position.y - chargeBarHeight / 2, chargeBarPointer.transform.position.z);
            }
        }
    }
}
