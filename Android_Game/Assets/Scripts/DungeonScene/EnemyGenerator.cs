using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//public class 

public class EnemyParty{
    private System.Random randomNumber;
    private int sizeOfParty;
    private List<GameObject> enemyObjectArray;
    private List<GameObject> healthBarObjectsArray;

    //HealthStats dev
    private List<int> enemyHealthArray;
    private List<int> enemyMaxHealthArray;

    //It serves as a template
    private static GameObject factoryObject;

    private int idOfParty;
    private float initialPositionX;
    private const double overallEnemyWidth = (0.01600781 * 130);
    private int lengthOfCorridor;
    private int idOfCorridor;
    private const int chunkWidth = 7;
    //This is needed to randomize next number based on the last one (will prevent generating same thing)
    private static int milisecondsForRand = 0;

    private static List<string> characterType = new List<string>();
    private const int minimalSizeOfParty = 1, maximalSizeOfParty = 5;//maximumSizeOfParty = 5;

    private string filePath;
    private string guiFilePath = "UI_Elements/";
    private string[] typeOfDungeonTexture = { "Defender", "Soldier", "Pikeman", "Archer" };
    private string[] typeOfBarTexture = { "HealthBarVertical" };

    private static DungeonManager dungeonManager;
    private static FightMode fightMode;
    private static ObjectSelector objectSelector;

public EnemyParty(int idOfCorridor, int howManyPartiesAlreadyExistsOnThisLevel, int idOfParty)
    {
        enemyObjectArray = new List<GameObject>();
        healthBarObjectsArray = new List<GameObject>();
        enemyHealthArray = new List<int>();
        enemyMaxHealthArray = new List<int>();

        randomNumber = new System.Random(DateTime.Now.Millisecond + milisecondsForRand);

        //It is length in number of chunks
        this.idOfCorridor = idOfCorridor;
        this.idOfParty = idOfParty;
        dungeonManager = GameObject.Find("Dungeon").GetComponent<DungeonManager>();
        this.lengthOfCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getNumberOfChunks();
        fightMode = GameObject.Find("Dungeon").GetComponent<FightMode>();
        objectSelector = GameObject.Find("Dungeon").GetComponent<ObjectSelector>();

        sizeOfParty = randomNumber.Next(minimalSizeOfParty, maximalSizeOfParty);

        if (sizeOfParty != 0)
        {
            if (GameObject.Find("FactoryObject") == null)
            {
                factoryObject = new GameObject();
                factoryObject.name="FactoryObject";
                factoryObject.layer = 0;
                factoryObject.AddComponent<SpriteRenderer>();
                Debug.Log("EnemyGenerator || EnemyParty || Created new FactoryObject");
            }
            else
            {
                //Should't get in here
                Debug.Log("EnemyGenerator || EnemyParty || Found FactoryObject");
                factoryObject = GameObject.Find("FactoryObject");
            }

            //LENGTH OF CORRIDOR IS WRONG ~ fixed i think
            Debug.Log("EnemyGenerator || EnemyParty || SizeOfParty " + (sizeOfParty - 1) + " furthest posible point to place: " + (lengthOfCorridor * chunkWidth - ((sizeOfParty - 1) * 2.081015)));

            //Scaler provider double number from 0 - 1. This allows me to generate any number from this as a scaler.

            Debug.Log("EnemyGenerator || EnemyParty ||  Current initialX: " + initialPositionX);
            
            bool shouldGenerateAnotherOne = true;
            do
            {
                double tempScaler = randomNumber.NextDouble();
                double furthestPointOfCorridor = lengthOfCorridor * chunkWidth - ((sizeOfParty) * 2.081015);
                double closestPointOfCorridor = 4 * 2.081015;

                //Generating party possition between start of corridor (adding max hero party width) and end of it (keeping width of enemy party in mind)
                initialPositionX = (float)(((furthestPointOfCorridor - closestPointOfCorridor) * tempScaler) + closestPointOfCorridor);

                bool noCollisionDetected = true;
                //Iterating through already existing parties on this level to check their possitions
                for (int i = 0; i < howManyPartiesAlreadyExistsOnThisLevel; i++)
                {
                    //Getting initial possition of party to check
                    float initX = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[i].getInitialPositionX();
                    float sizeOfParty = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[i].getEnemyObjectArray().Count;

                    Debug.Log("EnemyGenerator || EnemyParty || !!!!!!!!!!!!!!! || " + initialPositionX + ">" + initX + " && " + initialPositionX + "<" + initX + "+ (" + sizeOfParty + "*" + overallEnemyWidth + ")");
                    if ((initialPositionX > initX) && (initialPositionX <= initX + (sizeOfParty * overallEnemyWidth)))
                    {
                        Debug.Log("EnemyGenerator || EnemyParty || SPAWNING ON OTHER PARTY! - rerolling");
                        //At this point we can ommit next iterations as well
                        noCollisionDetected = false;
                    }
                }

                //If there were no collisions
                if (noCollisionDetected == true)
                {
                    Debug.Log("EnemyGenerator || EnemyParty || SPAWNED WITHOUT ANY PROBLEMS!");
                    shouldGenerateAnotherOne = false;
                }
            } while (shouldGenerateAnotherOne);
            
            

            Debug.Log("EnemyGenerator || EnemyParty || --------------> " + initialPositionX + " " + (sizeOfParty - 1) + " " + (lengthOfCorridor * chunkWidth - ((sizeOfParty - 1) * 2.081015)));

            SpriteRenderer spriteRender;
            filePath = "Enemies/";
            Debug.Log("EnemyGenerator || EnemyParty || Creating spooky scarry skeletons!");
            Debug.Log("EnemyGenerator || EnemyParty || Size of spookyness: " + sizeOfParty);


            for (int i = 0; i < sizeOfParty; i++)
            {
                enemyHealthArray.Add(100);
                enemyMaxHealthArray.Add(100);

                GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);//It is needed here to call MonoBehaviour
                tempObject.SetActive(false);
                tempObject.name = "EnemyObject_" + howManyPartiesAlreadyExistsOnThisLevel + "." + i;
                Debug.Log("EnemyGenerator || EnemyParty || Generated: " + "EnemyObject_" + howManyPartiesAlreadyExistsOnThisLevel + "." + i);
                factoryObject.name = "Factory EnemyGeneratorObject";
                enemyObjectArray.Add(tempObject);
                enemyObjectArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);
                spriteRender = enemyObjectArray[i].GetComponent<SpriteRenderer>();
                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
                spriteRender.sortingOrder = 1;
                spriteRender.flipY = true;
                spriteRender.transform.rotation = (Quaternion.Euler(0, 0, 270));
                //Debug.Log("-------------------> SPOOK: " + enemyObjectArray[i].transform.position.x);
                enemyObjectArray[i].transform.localPosition = new Vector3(i * 130, -100, 0);//TODO SET POSS
                                                                                            //Debug.Log("-------------------> POSTSPOOK: " + enemyObjectArray[i].transform.position.x);
                enemyObjectArray[i].transform.localScale = new Vector3(35, 35, 1);
                enemyObjectArray[i].SetActive(true);
                enemyObjectArray[i].transform.parent = null;
                enemyObjectArray[i].transform.localPosition = new Vector3(i * (float)overallEnemyWidth + initialPositionX, enemyObjectArray[i].transform.position.y, 0);//TODO SET POSS
                Debug.Log("EnemyGenerator || EnemyParty || After: Initial X: " + initialPositionX);
                Debug.Log("EnemyGenerator || EnemyParty || After: overallEnemyWidth: " + overallEnemyWidth);
                Debug.Log("EnemyGenerator || EnemyParty || After: Position X: " + (enemyObjectArray[i].transform.position.x + (-initialPositionX)));
                Debug.Log("EnemyGenerator || EnemyParty || After: Position X + Initial X: " + enemyObjectArray[i].transform.position.x);
            }
            MonoBehaviour.Destroy(factoryObject);
        }
    }

    public void loadAlreadyExistingSettingOfEnemies(int idOfCorridor, int indexOfParty)
    {
        sizeOfParty = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[indexOfParty].getEnemyObjectArray().Count;
        initialPositionX = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[indexOfParty].getInitialPositionX();
        SpriteRenderer spriteRender;
        enemyObjectArray.Clear();

        factoryObject = new GameObject();
        factoryObject.layer = 0;
        factoryObject.AddComponent<SpriteRenderer>();

        for (int i = 0; i < sizeOfParty; i++)
        {
            GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);//It is needed here to call MonoBehaviour
            tempObject.SetActive(false);
            tempObject.name = "EnemyObject_" + indexOfParty + "." + i;
            enemyObjectArray.Add(tempObject);
            enemyObjectArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);
            spriteRender = enemyObjectArray[i].GetComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
            spriteRender.sortingOrder = 1;
            spriteRender.flipY = true;
            spriteRender.transform.rotation = (Quaternion.Euler(0, 0, 270));
            enemyObjectArray[i].transform.localPosition = new Vector3(i * 130, -100, 0);//TODO SET POSS
            enemyObjectArray[i].transform.localScale = new Vector3(35, 35, 1);
            enemyObjectArray[i].SetActive(true);
            enemyObjectArray[i].transform.parent = null;
            enemyObjectArray[i].transform.localPosition = new Vector3(enemyObjectArray[i].transform.position.x + initialPositionX - Camera.main.transform.position.x, enemyObjectArray[i].transform.position.y, 0);
        }
        MonoBehaviour.Destroy(factoryObject);
    }

    public void loadAlreadyExistingSettingOfEnemies(int idOfCorridor, int indexOfParty, bool isItFightMode)
    {
        if (isItFightMode)
        {
            sizeOfParty = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[indexOfParty].getEnemyObjectArray().Count;

            SpriteRenderer spriteRender;
            enemyObjectArray.Clear();

            if (GameObject.Find("FactoryObject") == null)
            {
                factoryObject = new GameObject();
                factoryObject.name = "FactoryObject";
                factoryObject.layer = 0;
                factoryObject.AddComponent<SpriteRenderer>();
                Debug.Log("EnemyGenerator || EnemyParty || Created new FactoryObject");
            }
            else
            {
                //Should't get in here
                Debug.Log("EnemyGenerator || EnemyParty || Found FactoryObject");
                factoryObject = GameObject.Find("FactoryObject");
            }

            for (int i = 0; i < sizeOfParty; i++)
            {
                GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);//It is needed here to call MonoBehaviour

                float scaledSpaceBetweenEnemies = 90 * 5 / 7;
                float halfOfCanvasWidth = (float)(GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width * 0.5);

                tempObject.SetActive(false);
                tempObject.name = "EnemyObject_" + indexOfParty + "." + i;
                enemyObjectArray.Add(tempObject);
                enemyObjectArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);
                spriteRender = enemyObjectArray[i].GetComponent<SpriteRenderer>();
                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
                spriteRender.sortingOrder = 1;
                spriteRender.flipY = true;
                spriteRender.transform.rotation = (Quaternion.Euler(0, 0, 270));
                //The idea here is as follows: canvas is from -400 to 400 we're getting half of it to reach right end of it
                //next step it to draw last enemy from right borded +1 is to use one space from border
                fightMode.setEnemyPossitionBeforeFight(i, enemyObjectArray[i].transform.localPosition.x);
                enemyObjectArray[i].transform.localPosition = new Vector3(halfOfCanvasWidth - ((3-i + 1) * scaledSpaceBetweenEnemies), -100, 0);
                enemyObjectArray[i].transform.localScale = new Vector3(25, 25, 1);
                enemyObjectArray[i].SetActive(true);
            }
        }
    }

    public void displayHealthBarsOnEnemies(int idOfCorridor, int indexOfParty, bool isItFightMode)
    {
        if (isItFightMode == true)
        {
            int numberOfBarsToDisplay = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[indexOfParty].getEnemyObjectArray().Count;
            SpriteRenderer spriteRender;
            healthBarObjectsArray.Clear();

            if (GameObject.Find("FactoryObject") == null)
            {
                factoryObject = new GameObject();
                factoryObject.name = "FactoryObject";
                factoryObject.layer = 0;
                factoryObject.AddComponent<SpriteRenderer>();
                Debug.Log("EnemyGenerator || EnemyParty || Created new FactoryObject");
            }
            else
            {
                //Should't get in here
                Debug.Log("EnemyGenerator || EnemyParty || Found FactoryObject");
                factoryObject = GameObject.Find("FactoryObject");
            }

            for (int i = 0; i < numberOfBarsToDisplay; i++)
            {
                GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);//It is needed here to call MonoBehaviour

                float scaledSpaceBetweenEnemies = 90 * 5 / 7;
                float halfOfCanvasWidth = (float)(GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width * 0.5);

                tempObject.SetActive(false);
                tempObject.name = "EnemyHealthBar_" + indexOfParty + "." + i;
                healthBarObjectsArray.Add(tempObject);
                healthBarObjectsArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);

                spriteRender = healthBarObjectsArray[i].GetComponent<SpriteRenderer>();
                //We're getting healthBar texture from array
                spriteRender.sprite = Resources.Load<Sprite>(guiFilePath + typeOfBarTexture[0]);
                spriteRender.sortingOrder = 1;
                //The idea here is as follows: canvas is from -400 to 400 we're getting half of it to reach right end of it
                //next step it to draw last enemy from right borded +1 is to use one space from border
                healthBarObjectsArray[i].transform.localPosition = new Vector3(halfOfCanvasWidth - ((3 - i + 1) * scaledSpaceBetweenEnemies), -100, 0);
                healthBarObjectsArray[i].transform.localScale = new Vector3(25, 25, 1);
                healthBarObjectsArray[i].SetActive(true);
                healthBarObjectsArray[i].transform.localPosition = new Vector3(enemyObjectArray[i].transform.localPosition.x, enemyObjectArray[i].transform.localPosition.y + enemyObjectArray[i].GetComponent<SpriteRenderer>().sprite.texture.height/2, 0);
                healthBarObjectsArray[i].transform.localScale = new Vector3(500, 500, 1);
            }
        }
    }

    public void destroyEnemyObject(int idOfCorridor, int idOfEnemyParty, int idOfChoosenEnemy)
    {
        dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().RemoveAt(idOfChoosenEnemy);
        dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyMaxHealthArray().RemoveAt(idOfChoosenEnemy);
        dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyHealthArray().RemoveAt(idOfChoosenEnemy);
        //I will let health destroy itsellf after fight
        Debug.Log("EnemyGenerator || destroyEnemyObject || Destroying enemyObject no. " + idOfEnemyParty + "." + idOfChoosenEnemy);
        UnityEngine.Object.Destroy(GameObject.Find("EnemyObject_" + idOfEnemyParty + "." + idOfChoosenEnemy));
        //Previous health bar is taking over deleted one so we can get rid of only last one
        UnityEngine.Object.Destroy(GameObject.Find("EnemyHealthBar_" + idOfEnemyParty + "." + dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().Count));

        float scaledSpaceBetweenEnemies = 90 * 5 / 7;

        for (int i=0;i< dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[idOfEnemyParty].getEnemyObjectArray().Count+1; i++)
        {
            if (i != idOfChoosenEnemy && i > idOfChoosenEnemy)
            {
                GameObject tempObject = GameObject.Find("EnemyObject_" + idOfEnemyParty + "." + i);
                tempObject.transform.localPosition = new Vector3(tempObject.transform.localPosition.x - scaledSpaceBetweenEnemies, tempObject.transform.localPosition.y, tempObject.transform.localPosition.z);
                //Filling the name gap
                tempObject.name = ("EnemyObject_" + idOfEnemyParty + "." + (i-1));
            }
        }
        objectSelector.loadEnemyPossition(idOfCorridor, idOfEnemyParty);
    }

    public void setLengthOfCorridor(int lengthToSet)
    {
        lengthOfCorridor = lengthToSet;
    }

    public void setIdOfCorridor(int idToSet)
    {
        idOfCorridor = idToSet;
    }

    public void setInitialPossitionX(float positionToSet)
    {
        initialPositionX = positionToSet;
    }

    public void setIdOfParty(int idToSet)
    {
        idOfParty = idToSet;
    }

    public List<GameObject> getEnemyObjectArray()
    {
        return enemyObjectArray;
    }

    public int getLengthOfCorridor()
    {
        return lengthOfCorridor;
    }

    public int getIdOfCorridor()
    {
        return idOfCorridor;
    }

    public float getInitialPositionX()
    {
        return initialPositionX;
    }

    public int getIdOfParty()
    {
        return idOfParty;
    }

    public float getOverallEnemyWidth()
    {
        return (float)overallEnemyWidth;
    }

    public List<int> getEnemyHealthArray()
    {
        return enemyHealthArray;
    }

    public List<int> getEnemyMaxHealthArray()
    {
        return enemyMaxHealthArray;
    }
}