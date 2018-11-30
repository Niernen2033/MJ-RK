using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class DungeonsGenerator : MonoBehaviour {

    [SerializeField]
    private int idOfCorridor;

    [SerializeField]
    private int minimumNumberOfChunks, maximumNumberOfChunks;

    private GameObject factoryObject;
    private static List<GameChunk> producedChunks;
    private int objectsToGenerate;
    private int chunkWidth;
    private int leftBoundPossition, rightBoundPossition, rightBoundPossitionForGenerator;
    private Corridor corridorToGenerate;
    private static DungeonManager dungeonManager;
    private static GameObject dungeonCanvas;
    private static List<DungeonLevelChunk> dungeonLevelChunks;
    private static List<Corridor> corridorList;
    private static ButtonForCameraMovement movementButton;
    private static EnemyGenerator enemyGen;

    private System.Random randomNumber;

    public void Reset()
    {
        //Getting dungeonManager to acces his variables
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();
        dungeonLevelChunks = dungeonManager.GetLevelChunks();//Not sure now if needed
        //objectsToGenerate = dungeonLevelChunks[idOfCorridor].
        //objectsToGenerate = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getNumberOfChunks();//Problem if it doesn't exist
        movementButton = FindObjectOfType<ButtonForCameraMovement>();//Needs investigtion
        generationDecission();
    }

    public void Start() {
        randomNumber = new System.Random();
        minimumNumberOfChunks = 6;
        maximumNumberOfChunks = 12;

        //Static list of corridors
        corridorList = new List<Corridor>();

        //Temporary const
        chunkWidth = 7;

        //It's template object for creating another chunks
        factoryObject = new GameObject();
        //Defines layer to display backgound on
        factoryObject.layer = 0;

        objectsToGenerate = randomNumber.Next(minimumNumberOfChunks, maximumNumberOfChunks);
        //Saving length data to object for later reload
        corridorToGenerate = new Corridor(objectsToGenerate);
        leftBoundPossition = chunkWidth;
        rightBoundPossition = chunkWidth * (objectsToGenerate-1);
        rightBoundPossitionForGenerator = chunkWidth * objectsToGenerate;
        producedChunks = new List<GameChunk>();

        //Getting dungeonManager to acces his variables
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();//YET NEEDED
        dungeonLevelChunks = dungeonManager.GetLevelChunks();//YET NEEDED
        movementButton = FindObjectOfType<ButtonForCameraMovement>();//Needs investigtion
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();

        generationDecission();
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("-----------------------CorridorList straight length: " + getCorridorList().Count);
    }

    public void clearingPreviousSpriteObjects(int previousIdOfCorridor)
    {
        for(int i=0;i< objectsToGenerate; i++)
        {
            //Destroying previously generated corridor objects
            Destroy(GameObject.Find("DungeonChunk_" + i));
        }

        //Destroys enemies objects iterating by party(i) and members of it(j)
       for(int i=0;i< dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == previousIdOfCorridor).getEnemyParties().Count; i++)
        {
            for (int j = 0; j < dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == previousIdOfCorridor).getEnemyParties()[i].getEnemyObjectArray().Count; j++)
            {
                Destroy(GameObject.Find("EnemyObject_" + i + "." + j));
                Debug.Log("DESTROY THE CHILD, CORRUPT THEM ALL: " + "EnemyObject_" + i + "." + j);
            }
        }
        producedChunks.Clear();

        //Destroying entrance and exit neighbours outside of corridor
        Destroy(GameObject.Find("DungeonChunkEntrance"));
        Destroy(GameObject.Find("DungeonChunkExit"));
        //Destroy(GameObject.Find("New Game Object"));//It's needed when reloading another corridor
    }

    public void loadAnotherLevel(int Id)
    {
        int previousIdOfCorridor = idOfCorridor;
        idOfCorridor = Id;
        Debug.Log("-----------------------ID: " + Id);
        clearingPreviousSpriteObjects(previousIdOfCorridor);
        Reset();
        movementButton.getThemToTheEntrance();
        Debug.Log("-----------------------Debug4 in dungeonGenerator length: " + getCorridorList().Count);
        //Here we have to initialize level again -> reload every object
    }

    public int getLeftBoundPossition()
    {
        return leftBoundPossition;
    }

    public int getRightBoundPossition()
    {
        return rightBoundPossition;
    }

    public void generationDecission()
    {
        int i;
        string pickedOne;
        int pickedOneInt;

        Debug.Log("DungeonGenerator || generationDecission || Id of corridor before: " + idOfCorridor);
        if (dungeonManager.getLevelsArray().Exists(x => x.getIdOfLevel() == idOfCorridor))
        {
            Debug.Log("DungeonGenerator || generationDecission || Scene " + idOfCorridor + " was already created!");
            objectsToGenerate = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getNumberOfChunks();
            rightBoundPossition = chunkWidth * (objectsToGenerate - 1);
            rightBoundPossitionForGenerator = chunkWidth * objectsToGenerate;
            Debug.Log("DungeonGenerator || generationDecission || Objects to generate: " + objectsToGenerate);
            Debug.Log("DungeonGenerator || generationDecission || idOfCorridor: " + idOfCorridor);
            string debug = "";
            for (i = 0; i < objectsToGenerate; i++)
            {
                pickedOneInt = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getChunkArrayElementTexture(i);
                generateMap(pickedOneInt, i, true);
                debug += " " + pickedOneInt;
            }
            Debug.Log("DungeonGenerator || generationDecission || pickedOneInt(generated levels) " + debug);

            //Making it to read from already ceated variables of this level
            loadEnemyParties();

        }
        else
        {
            objectsToGenerate = randomNumber.Next(minimumNumberOfChunks, maximumNumberOfChunks);
            rightBoundPossition = chunkWidth * (objectsToGenerate - 1);
            rightBoundPossitionForGenerator = chunkWidth * objectsToGenerate;


            Debug.Log("Scene " + idOfCorridor + " wasn't created!");

            Debug.Log("DungeonGenerator || generationDecission || Length of dungeonManager.getLevelsArray().Count before " + dungeonManager.getLevelsArray().Count + " | objectsToGenerate | " + objectsToGenerate);
            dungeonManager.getLevelsArray().Add(new DungeonLevel(dungeonManager.getLevelsArray().Count, objectsToGenerate));//tu zryte przypisywanie
            Debug.Log("DungeonGenerator || generationDecission || Length of dungeonManager.getLevelsArray().Count after " + dungeonManager.getLevelsArray().Count + " | objectsToGenerate | " + objectsToGenerate);
            Debug.Log("DungeonGenerator || generationDecission || IdOfLevel from last element: " + dungeonManager.getLevelsArray()[dungeonManager.getLevelsArray().Count-1].getIdOfLevel() + " | objectsToGenerate | " + dungeonManager.getLevelsArray()[dungeonManager.getLevelsArray().Count - 1].getNumberOfChunks());


            for (i = 0; i < objectsToGenerate; i++)
            {
                pickedOneInt = randomNumber.Next(1, 7);
                //HERE IS LOCAL CORRIDOR - TO BE REMOVED
                {
                    corridorToGenerate.setTextureArray(pickedOneInt);//Adding texture number to Corridor object
                                                                     //Adding generated corridor object to List
                    corridorList.Add(corridorToGenerate);
                }

                //Setting and saving texture in array inside DungeonManager
                dungeonManager.getLevelsArray()[dungeonManager.getLevelsArray().Count - 1].setChunkArrayElementTexture(i, pickedOneInt);//zle id przydziela chyba
                dungeonManager.getLevelsArray()[dungeonManager.getLevelsArray().Count - 1].setIdOfLevel(idOfCorridor);//generuje to samo
                dungeonManager.getLevelsArray()[dungeonManager.getLevelsArray().Count - 1].setNumberOfChunks(objectsToGenerate);

                pickedOne = pickedOneInt.ToString();
                generateMap(pickedOneInt, i, false);
                //dungeonLevelChunks[idOfCorridor].setWasCreated(true);//We will need to eliminate theese
            }

            //Temporary generating enemy possition
            generateEnemyParties();
        }

        //EnemyParty enemyParty = new EnemyParty(chunkWidth * (objectsToGenerate - 1), idOfCorridor);
    }

    public void generateMap(int pickedOneInt, int i, bool wasAlreadyGenerated)
    {
        GameObject tempObject;
        GameChunk tempChunk;
        SpriteRenderer spriteRender;
        string filePath = "WarrensTextures/";
        string[] typeOfDungeonTexture = { "warrens." };
        string[] specificTextureType = { "corridor_wall.", "corridor_door.basic", "endhall.01" };

        tempObject = Instantiate(factoryObject);
        tempChunk = new GameChunk(tempObject, pickedOneInt);
        tempObject.SetActive(false);
        producedChunks.Add(tempChunk);

        //Adding SpriteRenderer to component (later we can change it to add it into cloned object)
        spriteRender = producedChunks[i].getProducedObject().AddComponent<SpriteRenderer>();//jest jeszcze GetComponent

        //Every first and last chunk will be generated as entrance and exit
        if (i == 0 || i == objectsToGenerate - 1)
        {
            //Generating entrance and exit from the corridor
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[1]);
        }
        else
        {
            if (wasAlreadyGenerated == false)
            {
                //This checks if it generated 2 identical textures in a row
                if (producedChunks[i - 1].getRandomizedNumber() == producedChunks[i].getRandomizedNumber())
                {
                    //And tries to reroll it until it gets different one
                    while (producedChunks[i - 1].getRandomizedNumber() == producedChunks[i].getRandomizedNumber())
                    {
                        //Applies new value
                        producedChunks[i].setRandomizedNumber(randomNumber.Next(1, 7));
                    }
                }
                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[0] + producedChunks[i].getRandomizedNumber().ToString());//I might need to remove toString
            }
            else
            {
                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[0] + pickedOneInt);
            }
        }

        //Setting possition, naming new object and making it active in inspector
        producedChunks[i].getProducedObject().transform.Translate(Vector2.right * chunkWidth * i);
        producedChunks[i].getProducedObject().name = "DungeonChunk_" + i.ToString();
        producedChunks[i].getProducedObject().SetActive(true);

        //After generating last chunk we are generating border chunks
        if (i == objectsToGenerate-1)
        {
            generateEdgesOfMap(tempObject, tempChunk, spriteRender, filePath, typeOfDungeonTexture, specificTextureType);
        }
    }

    public void generateEdgesOfMap(GameObject tempObject, GameChunk tempChunk, SpriteRenderer spriteRender, string filePath, string[] typeOfDungeonTexture, string[] specificTextureType)
    {
        //Debug.Log("Dlugość chunkow past: " + producedChunks.Count);
        int i;
        for (i = 0; i < 2; i++)
        {
            //Generating temporary Object
            tempObject = Instantiate(factoryObject);
            tempChunk = new GameChunk(tempObject, -1);
            tempObject.SetActive(false);
            producedChunks.Add(tempChunk);

            //Debug.Log("Number of object: " + (int)(objectsToGenerate + i));
            //Debug.Log("Debug iterator!" + i);
            //Debug.Log("Debug OTG!" + objectsToGenerate);
            //Debug.Log("Chunk array length: " + producedChunks.Count);
            spriteRender = producedChunks[(int)(objectsToGenerate + i)].getProducedObject().AddComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[2]);

            //Entrance and exit generation
            if (i == 0)
            {
                producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(Vector2.left * leftBoundPossition);
                producedChunks[objectsToGenerate + i].getProducedObject().name = "DungeonChunkEntrance";
            }
            else
            {
                //Exit texture must be flipped
                spriteRender.flipX = true;
                producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(Vector2.right * rightBoundPossitionForGenerator);
                producedChunks[objectsToGenerate + i].getProducedObject().name = "DungeonChunkExit";
            }
            producedChunks[objectsToGenerate + i].getProducedObject().SetActive(true);
        }
    }

    public void generateEnemyParties()
    {
        bool thereIsEnoughSpace = false;
        int howManyEnemiesToGenerate = randomNumber.Next(0, 5);
        //It's getting random number of parties with limit up to 4 parties per corridor
        while (!thereIsEnoughSpace)
        {
            thereIsEnoughSpace = makeSureThereWillBeEnoughSpaceForThem(howManyEnemiesToGenerate);
            howManyEnemiesToGenerate--;
        }


        Debug.Log("DungeonsGenerator || generateEnemyParties || How many enemy parties to generate: " + howManyEnemiesToGenerate);
        string debugString = "";
        //EnemyParty generatedEnemyParty;
        Debug.Log("DungeonsGenerator || generateEnemyParties || How many levels are generated already: " + dungeonManager.getLevelsArray().Count);
        Debug.Log("DungeonsGenerator || generateEnemyParties || Current corridor: " + idOfCorridor);

        for (int i = 0; i < howManyEnemiesToGenerate; i++)
        {
            for(int Ii=0;Ii< dungeonManager.getLevelsArray().Count; Ii++)
            {
                debugString += dungeonManager.getLevelsArray()[Ii].getIdOfLevel() + " ";
            }
            Debug.Log("DungeonsGenerator || generateEnemyParties || SavedId's(already generated levels): " + debugString);
            Debug.Log("DungeonsGenerator || generateEnemyParties || How many enemies parties are there: " + dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count);
            //Creates new party with current number of objects, and IdOfCorridor
            //Checks whatever there are already created parties of enemies
            EnemyParty generatedEnemyParty = new EnemyParty(idOfCorridor,
                dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count, i);
            //Adds newly created EnemyParty to DungeonManager || DungeonLevel || enemyParties
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).addToEnemyParties(generatedEnemyParty);
            Debug.Log("DungeonsGenerator || generateEnemyParties || Generating party number " + i);
            debugString = "";
        }
        for (int i = 0; i < dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count; i++){
            debugString += dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[i].getInitialPositionX() + " ";
        }
        Debug.Log("DungeonsGenerator || generateEnemyParties || Possitions of " + 
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count + " enemy parties on this levels: " + debugString);

        //pickedOneInt = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getChunkArrayElementTexture(i);
        //generateMap(pickedOneInt, i, true);
    }

    public void loadEnemyParties()
    {
        //Reading already written number of parties
        int howManyToLoad = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count;
        Debug.Log("DungeonsGenerator || loadEnemyParties || How many enemy parties to load: " + howManyToLoad);
        string debugString = "";
        //EnemyParty generatedEnemyParty;
        for (int i = 0; i < howManyToLoad; i++)
        {
            Debug.Log("DungeonsGenerator || loadEnemyParties || How many levels are generated already: " + dungeonManager.getLevelsArray().Count);
            Debug.Log("DungeonsGenerator || loadEnemyParties || Current corridor: " + idOfCorridor);

            for (int Ii = 0; Ii < dungeonManager.getLevelsArray().Count; Ii++)
            {
                debugString += dungeonManager.getLevelsArray()[Ii].getIdOfLevel() + " ";
            }
            Debug.Log("DungeonsGenerator || loadEnemyParties || SavedId's(already generated levels): " + debugString);


            Debug.Log("DungeonsGenerator || loadEnemyParties || How many enemies parties are there: " + dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count);
            //Creates new party with current number of objects, and IdOfCorridor
            //Checks whatever there are already created parties of enemies
            EnemyParty generatedEnemyParty = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getSpecificEnemyParty(i);
            //Adds newly created EnemyParty to DungeonManager || DungeonLevel || enemyParties
            generatedEnemyParty.loadAlreadyExistingSettingOfEnemies(idOfCorridor, i);
            Debug.Log("DungeonsGenerator || loadEnemyParties || Generating party number " + i);
            debugString = "";
        }
        for (int i = 0; i < dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count; i++)
        {
            debugString += dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties()[i].getInitialPositionX() + " ";
        }
        Debug.Log("DungeonsGenerator || loadEnemyParties || Possitions of " +
            dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getEnemyParties().Count + " enemy parties on this levels: " + debugString);

        //pickedOneInt = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getChunkArrayElementTexture(i);
        //generateMap(pickedOneInt, i, true);
    }

    //It will check if there is enough space in corridor to fit everyone in worst deployment scenario
    public bool makeSureThereWillBeEnoughSpaceForThem(int howManyToGenerate)
    {
        int lengthOfCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == idOfCorridor).getNumberOfChunks();
        double widthOfUnit = (0.01600781 * 130);
        double furthestPointOfCorridor = lengthOfCorridor * chunkWidth;
        double closestPointOfCorridor = 4 * widthOfUnit;
        //Worst scenario (width of enemy party + gap of the same size + another party and so on...)
        double howMuchWeNeedInWorstCase = 2 * 4 * widthOfUnit * howManyToGenerate;
        //Checking if there is enough space
        if (howMuchWeNeedInWorstCase <= furthestPointOfCorridor - closestPointOfCorridor)
        {
            Debug.Log("DungeonsGenerator || generateEnemyParties || makeSureThereWillBeEnoughSpaceForThem || There is enough space for" + howManyToGenerate + "!");
            return true;
        }
        else
        {
            Debug.Log("DungeonsGenerator || generateEnemyParties || makeSureThereWillBeEnoughSpaceForThem || There is not enough space for" + howManyToGenerate + "!");
            return false;
        }
    }

    public Corridor getCorridorFromList(int whichOne)
    {
        return corridorList[whichOne];
    }

    public List<Corridor> getCorridorList()
    {
        return corridorList;
    }

    public int getIdOfCorridor()
    {
        return idOfCorridor;
    }
}

public class GameChunk
{
    //Obiekt właściwego chunka
    private GameObject producedObject;
    //Wylosowana liczba, żeby wiedzieć, czy tekstura się powtarza
    private int randomizedNumber;

    public GameChunk(GameObject obj, int number)
    {
        this.producedObject = obj;
        this.randomizedNumber = number;
    }

    public void setProducedObject(GameObject obj)
    {
        this.producedObject = obj;
    }

    public void setRandomizedNumber(int number)
    {
        this.randomizedNumber = number;
    }

    public GameObject getProducedObject()
    {
        return producedObject;
    }

    public int getRandomizedNumber()
    {
        return randomizedNumber;
    }
}

public class Corridor
{
    private int corridorLength;
    private List<int> textureArray;

    public Corridor(int corridorLength)
    {
        this.corridorLength = corridorLength;
        textureArray = new List<int>();
    }

    public void setTextureArray(List<int> textureId)
    {
        textureArray = textureId;
    }

    public void setTextureArray(int textureId)
    {
        textureArray.Add(textureId);
    }

    public List<int> getTextureArray()
    {
        return textureArray;
    }

    public int getTextureArrayElement(int iterator)
    {
        return textureArray[iterator];
    }

    public void setCorridorLength(int corridorLength)
    {
        this.corridorLength = corridorLength;
    }

    public int getCorridorLength()
    {
        return corridorLength;
    }
}
