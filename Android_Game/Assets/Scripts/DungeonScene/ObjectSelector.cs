﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour {

    private static GameObject dungeonCanvas;
    private static FightMode fightMode;
    private static DungeonManager dungeonManager;
    private static DungeonsGenerator dungeonsGenerator;
    private static ButtonForCameraMovement buttonForCameraMovement;
    private static List<FightModeObject> enemyObjectsArray;
    private static List<FightModeObject> heroObjectsArray;
    private static GameObject enemyHighlightMask;
    private static GameObject heroHighlightMask;
    private static bool objectPrepared;
    private static bool objectGotParent;
    private const string guiFilePath = "UI_Elements/";
    private static readonly string[] typeOfHighlightTexture = { "Highlight", "EnemyHighlight" };
    private static GameObject tempObject;
    private static int idOfCorridor;
    private static int idOfEnemyParty;

    // Use this for initialization
    void Start () {
        dungeonCanvas = GameObject.Find("Dungeon");
        fightMode = dungeonCanvas.GetComponent<FightMode>();
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();
        dungeonsGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        buttonForCameraMovement = dungeonCanvas.GetComponent<ButtonForCameraMovement>();
        enemyObjectsArray = new List<FightModeObject>();
        heroObjectsArray = new List<FightModeObject>();
        tempObject = new GameObject();
        createMasksForHighlight();
    }
	
	// Update is called once per frame
	void Update () {
        if (fightMode.getPartyIsInFightMode() == true)
        {
            checkIfIsTouched();
        }
	}

    public void loadEnemyPossition(int idOfCorridorTemp, int idOfEnemyPartyTemp)
    {
        idOfCorridor = idOfCorridorTemp;
        idOfEnemyParty = idOfEnemyPartyTemp;
        for(int i = 0; i < dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getSpecificEnemyParty(idOfEnemyParty).getEnemyObjectArray().Count; i++)
        {
            GameObject tempObject = GameObject.Find("EnemyObject_" + idOfEnemyParty + "." + i);
            //makeItRelativeTotheCamera(tempObject);
            SpriteRenderer spriteRendererOfTempObject = tempObject.GetComponent<SpriteRenderer>();
            FightModeObject tempFightModeObject = new FightModeObject(tempObject.transform.localPosition,
                spriteRendererOfTempObject.bounds.size.x * spriteRendererOfTempObject.transform.localScale.x, spriteRendererOfTempObject.bounds.size.y * spriteRendererOfTempObject.transform.localScale.y, "enemy");
            enemyObjectsArray.Add(tempFightModeObject);
        }
    }

    public void loadHeroPossition()
    {
        int[] heroesOrder = { 3, 1, 0, 2 };

        for (int i=0; i<buttonForCameraMovement.getHeroesObjects().Length-1; i++)
        {
            GameObject tempObject = buttonForCameraMovement.getHeroesObjects()[heroesOrder[3 - i]];
            SpriteRenderer spriteRendererOfTempObject = tempObject.GetComponent<SpriteRenderer>();
            FightModeObject tempFightModeObject = new FightModeObject(tempObject.transform.position,
                spriteRendererOfTempObject.bounds.size.x, spriteRendererOfTempObject.bounds.size.y, "hero");
            heroObjectsArray.Add(tempFightModeObject);
        }
    }

    public void makeItRelativeTotheCamera(GameObject objectToAdjust)
    {
        objectToAdjust.transform.position = new Vector3(objectToAdjust.transform.position.x - Camera.main.transform.position.x, objectToAdjust.transform.position.y - Camera.main.transform.position.y, objectToAdjust.transform.position.z - Camera.main.transform.position.z);
    }

    public void compareItToCamera(GameObject objectToAdjust)
    {
        //Does nothing for now
        objectToAdjust.transform.position = new Vector3(objectToAdjust.transform.position.x, objectToAdjust.transform.position.y, objectToAdjust.transform.position.z);
    }

    //Prepare objects for marking choosen objects
    public void createMasksForHighlight()
    {
        objectPrepared = false;
        objectGotParent = false;
        tempObject = new GameObject();

        //I had to create async code fragment
        objectPrepared = prepareHighlightObject();
        objectGotParent = scaleHighlightObject();
        StartCoroutine(useIt());
    }

    public bool prepareHighlightObject()
    {
        tempObject.transform.SetParent(GameObject.Find("Dungeon").transform, false);
        tempObject.AddComponent<SpriteRenderer>();
        return true;
    }

    public bool scaleHighlightObject()
    {
        StartCoroutine(scaleIt());
        return true;
    }

    IEnumerator scaleIt()
    {
        yield return new WaitUntil(() => objectPrepared == true);
        tempObject.transform.localScale = new Vector3(30, 45, 1);
    }

    IEnumerator useIt()
    {
        yield return new WaitUntil(() => objectGotParent == true);
        enemyHighlightMask = Instantiate(tempObject);
        enemyHighlightMask.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(guiFilePath + typeOfHighlightTexture[1]);
        enemyHighlightMask.name = "EnemyHighlightMaskObject";
        enemyHighlightMask.transform.SetParent(GameObject.Find("Dungeon").transform, false);
        enemyHighlightMask.GetComponent<SpriteRenderer>().sortingOrder = 3;
        enemyHighlightMask.SetActive(false);
        heroHighlightMask = Instantiate(tempObject);
        heroHighlightMask.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(guiFilePath + typeOfHighlightTexture[0]);
        heroHighlightMask.name = "HeroHighlightMaskObject";
        heroHighlightMask.transform.SetParent(GameObject.Find("Dungeon").transform, false);
        heroHighlightMask.GetComponent<SpriteRenderer>().sortingOrder = 3;
        heroHighlightMask.SetActive(false);
    }

    public void checkIfIsTouched()
    {
        float halfOfCanvasWidth = (float)(GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width * 0.5 * GameObject.Find("Dungeon").GetComponent<RectTransform>().localScale.x);
        float scaledSpaceBetweenEnemies = 90 * 5 / 7;
        float halfOfSpaceBetweenEnemies = scaledSpaceBetweenEnemies / 2;
        float CanvasWidth = GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width;

        //Debug.Log("Enemy 0 x: " + (halfOfCanvasWidth - ((3 - 0 + 1) * scaledSpaceBetweenEnemies - halfOfSpaceBetweenEnemies)));
        //Debug.Log("Enemy 0 x finishes: " + (halfOfCanvasWidth - ((3 - 0 + 1) * scaledSpaceBetweenEnemies + halfOfSpaceBetweenEnemies)));
        //Debug.Log("Enemy 1 x: " + (halfOfCanvasWidth - ((3 - 1 + 1) * scaledSpaceBetweenEnemies - halfOfSpaceBetweenEnemies)));
        //Debug.Log("Enemy 1 x finishes: " + (halfOfCanvasWidth - ((3 - 1 + 1) * scaledSpaceBetweenEnemies + halfOfSpaceBetweenEnemies)));

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Vector3 touchInWorld = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 touchInWorld = Camera.main.ScreenToViewportPoint(touch.position);
            touchInWorld = new Vector3((touchInWorld.x * (CanvasWidth))- ((CanvasWidth) /2), touchInWorld.y, touchInWorld.z);
            //float halfOfCanvasWidth = (float)(GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width * 0.5 * GameObject.Find("Dungeon").GetComponent<RectTransform>().localScale.x);
            //float scaledSpaceBetweenEnemies = 90 * 5 / 7;
            //float halfOfSpaceBetweenEnemies = scaledSpaceBetweenEnemies / 2;


            for (int i = 0; i < enemyObjectsArray.Count; i++)
            {
                GameObject tempObject = GameObject.Find("EnemyObject_" + idOfEnemyParty + "." + i);
                enemyObjectsArray[i].setPositionOfObject(tempObject.transform.localPosition);
                
                Debug.Log("Touch position x: " + touchInWorld.x);
                Debug.Log("Touch position y: " + touchInWorld.y);
                /*
                Debug.Log("Touch position X1: " + touchInWorld.x + " > " + (halfOfCanvasWidth - ((3 - 0 + 1) * scaledSpaceBetweenEnemies - halfOfSpaceBetweenEnemies)) * (-1));
                Debug.Log("Touch position X1: " + touchInWorld.x + " <= " + (halfOfCanvasWidth - ((3 - 0 + 1) * scaledSpaceBetweenEnemies + halfOfSpaceBetweenEnemies)) * (-1));
                Debug.Log("Touch position X2: " + touchInWorld.x + " > " + (halfOfCanvasWidth - ((3 - 1 + 1) * scaledSpaceBetweenEnemies - halfOfSpaceBetweenEnemies)) * (-1));
                Debug.Log("Touch position X2: " + touchInWorld.x + " <= " + (halfOfCanvasWidth - ((3 - 1 + 1) * scaledSpaceBetweenEnemies + halfOfSpaceBetweenEnemies)) * (-1));
                */

                if ((touchInWorld.x*(-1) > (CanvasWidth / 2 - ((4 - (3 - i)) * scaledSpaceBetweenEnemies - halfOfSpaceBetweenEnemies))*(-1)) && (touchInWorld.x*(-1) <= (CanvasWidth / 2 - ((4 - (3 - i)) * scaledSpaceBetweenEnemies + halfOfSpaceBetweenEnemies)*(-1))))
                {
                    Debug.Log("Touched an enemy number: " + (enemyObjectsArray.Count-i-1));
                    //enemyHighlightMask.transform.localPosition = enemyObjectsArray[enemyObjectsArray.Count - i - 1].getPositionOfObject();
                    enemyHighlightMask.transform.localPosition = new Vector3(CanvasWidth/2 - ((4-(3-i))*scaledSpaceBetweenEnemies), enemyObjectsArray[enemyObjectsArray.Count - i - 1].getPositionOfObject().y, enemyObjectsArray[enemyObjectsArray.Count - i - 1].getPositionOfObject().z);
                    enemyHighlightMask.SetActive(true);
                    //Debug.Log("Position of highlight: " + enemyObjectsArray[enemyObjectsArray.Count - i - 1].getPositionOfObject());
                }
            }
        }
    }

/*
public void checkIfIsTouched()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        //Debug.Log("Touch position x: " + touch.position.x);
        //Debug.Log("Touch position y: " + touch.position.y);
        float halfOfCanvasWidth = (float)(dungeonCanvas.GetComponent<RectTransform>().rect.width * (-0.5));
        float canvasWidthInWorldScale = dungeonCanvas.GetComponent<RectTransform>().rect.width * dungeonCanvas.GetComponent<RectTransform>().localScale.x;
        float canvasHeightInWorldScale = dungeonCanvas.GetComponent<RectTransform>().rect.height * dungeonCanvas.GetComponent<RectTransform>().localScale.y;
        //Vector3 touchInWorld = Camera.main.ScreenToWorldPoint(touch.position);
        Vector3 touchInWorld = Camera.main.ScreenToWorldPoint(touch.position);
        Vector3 canvasVector = dungeonCanvas.GetComponent<RectTransform>().position;


        Debug.Log("Touch position x: " + touchInWorld.x);
        Debug.Log("Touch position y: " + touchInWorld.y);
        Debug.Log("Canvas caled width: " + Camera.main.transform.TransformPoint(new Vector3 (Camera.main.pixelWidth,1,1)).x);
        Debug.Log("Canvas width: " + canvasWidthInWorldScale);
        Debug.Log("Canvas height: " + canvasHeightInWorldScale);
        Debug.Log("Canvas width vector: " + canvasVector.x);
        //Debug.Log("Enemy pos x: " + (halfOfCanvasWidth - (4 * scaledSpaceBetweenEnemies)) + " to " + (halfOfCanvasWidth - ((3) * scaledSpaceBetweenEnemies)));
        //Debug.Log("Enemy pos2 x: " + Camera.main.WorldToViewportPoint(enemyObjectsArray[1].getPositionOfObject()).x + " to " + (Camera.main.WorldToViewportPoint(enemyObjectsArray[1].getPositionOfObject()).x));
        //Debug.Log("Enemy pos4 x: " + Camera.main.WorldToViewportPoint(enemyObjectsArray[3].getPositionOfObject()).x + " to " + (Camera.main.WorldToViewportPoint(enemyObjectsArray[3].getPositionOfObject()).x));


        //90 is original space and we're scaling it by 5/7
        //Now every point is countes in percentages (from toch input)
        //We're gonna convert coordinates of the enemies also
        //Percentages are measued in camera sca
        //enemyObjectArray[i].transform.localPosition = new Vector3(halfOfCanvasWidth - ((3 - i + 1) * scaledSpaceBetweenEnemies), -100, 0);
        Debug.Log("Enemy pos1: " + enemyObjectsArray[0].getPositionOfObject().x);
        Debug.Log("EnemyWidth pos1: " + enemyObjectsArray[0].getObjectWidth()/2);
        Debug.Log("Canvas width1: " + canvasWidthInWorldScale);
        Debug.Log("Canvas width vector: " + canvasVector.x);

        for (int i = 0; i < enemyObjectsArray.Count; i++)
        {
            //float initialPositionX = Camera.main.transform.InverseTransformPoint(enemyObjectsArray[i].getPositionOfObject()).x - Camera.main.transform.InverseTransformPoint(new Vector3(enemyObjectsArray[i].getObjectWidth() / 2,1,1)).x;
            float initialPositionX = (float)(enemyObjectsArray[i].getPositionOfObject().x - enemyObjectsArray[i].getObjectWidth());

            float rightBoundPositionX = 0;
            if (i == enemyObjectsArray.Count - 1)
            {
                //rightBoundPositionX = enemyObjectsArray[i].getPositionOfObject().x + enemyObjectsArray[i].getObjectWidth() / 2;
                rightBoundPositionX = enemyObjectsArray[i].getPositionOfObject().x + enemyObjectsArray[i].getObjectWidth();
            }
            else
            {
                rightBoundPositionX = initialPositionX - (Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).x - Camera.main.transform.TransformPoint(enemyObjectsArray[i+1].getPositionOfObject()).x);
            }
            float initialPositionY = Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).y - enemyObjectsArray[i].getObjectHeight() / 2;
            float rightBoundPositionY = Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).y + enemyObjectsArray[i].getObjectHeight() / 2;
            Debug.Log("Enemy pos x: " + initialPositionX + " " + rightBoundPositionX);
            Debug.Log("Enemy pos y: " + initialPositionY + " " + rightBoundPositionY);

            if ((touchInWorld.x > initialPositionX)&&(touchInWorld.x <= rightBoundPositionX))
            //&&
            //(touchInWorld.y < initialPositionY)&&(touchInWorld.y >= rightBoundPositionY))
            {
                Debug.Log("I choose you enemy pokemon no. " + i);
        }
        }
        //for (int i = 0; i < heroObjectsArray.Count; i++)
        //{

        //}
    }
}
}*/
    /*
        public void checkIfIsTouched()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchInWorld = Camera.main.ScreenToWorldPoint(touch.position);


                Debug.Log("Touch position x: " + touchInWorld.x);
                Debug.Log("Touch position y: " + touchInWorld.y);
                Debug.Log("Enemy position x from object: " + enemyObjectsArray[0].getPositionOfObject().x);
                Debug.Log("EnemyWidth from object: " + enemyObjectsArray[0].getObjectWidth());

                float enemyWidths;

                for (int i = 0; i < enemyObjectsArray.Count; i++)
                {
                    //float initialPositionX = Camera.main.transform.InverseTransformPoint(enemyObjectsArray[i].getPositionOfObject()).x - Camera.main.transform.InverseTransformPoint(new Vector3(enemyObjectsArray[i].getObjectWidth() / 2,1,1)).x;
                    float initialPositionX = (float)(enemyObjectsArray[i].getPositionOfObject().x);

                    float rightBoundPositionX = 0;
                    if (i == enemyObjectsArray.Count - 1)
                    {
                        //rightBoundPositionX = enemyObjectsArray[i].getPositionOfObject().x + enemyObjectsArray[i].getObjectWidth() / 2;
                        rightBoundPositionX = enemyObjectsArray[i].getPositionOfObject().x + enemyObjectsArray[i].getObjectWidth();
                    }
                    else
                    {
                        rightBoundPositionX = initialPositionX - (Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).x - Camera.main.transform.TransformPoint(enemyObjectsArray[i + 1].getPositionOfObject()).x);
                    }
                    float initialPositionY = Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).y - enemyObjectsArray[i].getObjectHeight() / 2;
                    float rightBoundPositionY = Camera.main.transform.TransformPoint(enemyObjectsArray[i].getPositionOfObject()).y + enemyObjectsArray[i].getObjectHeight() / 2;
                    /*Debug.Log("-----------------------");
                    Debug.Log("Enemy pos x: " + initialPositionX + " " + rightBoundPositionX);
                    Debug.Log("Enemy pos y: " + initialPositionY + " " + rightBoundPositionY);
                    Debug.Log("Enemy pos x ob: " + enemyObjectsArray[i].getPositionOfObject().x);
                    Debug.Log("Enemy pos y ob: " + enemyObjectsArray[i].getPositionOfObject().y);
                    Debug.Log("EnemyWidth from object: " + enemyObjectsArray[i].getObjectWidth());
                    Debug.Log("-----------------------");*/

    /*

                if ((touchInWorld.x > initialPositionX - (2)) && (touchInWorld.x <= initialPositionX - (i*2)))
                //&&
                //(touchInWorld.y < initialPositionY)&&(touchInWorld.y >= rightBoundPositionY))
                {
                    Debug.Log("I choose you enemy pokemon no. " + i);
                }
                Debug.Log("Enemy pos x: " + (initialPositionX - 2) + " " + (initialPositionX - i*2));
            }
            //for (int i = 0; i < heroObjectsArray.Count; i++)
            //{

            //}
        }
    }*/
}

public class FightModeObject
{
    private Vector3 positionOfObject;
    private float objectWidth;
    private float objectHeight;
    private string objectType;


    public FightModeObject(Vector3 position, float widthOfObject, float heightOfObject, string typeOfObject)
    {
        positionOfObject = position;
        objectWidth = widthOfObject;
        objectHeight = heightOfObject;
        objectType = typeOfObject;
    }

    public void setPositionOfObject(Vector3 positionToSet)
    {
        positionOfObject = positionToSet;
    }

    public void setObjectWidth(float widthToSet)
    {
        objectWidth = widthToSet;
    }

    public void setObjectHeight(float heightToSet)
    {
        objectHeight = heightToSet;
    }

    public void setObjectType(string typeToSet)
    {
        objectType = typeToSet;
    }

    public Vector3 getPositionOfObject()
    {
        return positionOfObject;
    }

    public float getObjectWidth()
    {
        return objectWidth;
    }

    public float getObjectHeight()
    {
        return objectHeight;
    }

    public string getObjectType()
    {
        return objectType;
    }

}