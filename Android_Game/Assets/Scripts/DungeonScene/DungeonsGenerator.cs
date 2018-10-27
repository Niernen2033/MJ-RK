using System.Collections.Generic;
using UnityEngine;

public class DungeonsGenerator : MonoBehaviour {

    private GameObject factoryObject;
    private List<GameChunk> producedChunks;
    private int objectsToGenerate;
    private int chunkWidth;
    private int minimumNumberOfChunks;
    private int maximumNumberOfChunks;

    System.Random randomNumber = new System.Random();

    public void Start() {
        minimumNumberOfChunks = 6;
        maximumNumberOfChunks = 12;

        //to narazie na sztywno
        chunkWidth = 7;

        //to je wzór, tego nie wysprite'ujesz
        factoryObject = new GameObject();
        factoryObject.layer = 0;

        objectsToGenerate = randomNumber.Next(minimumNumberOfChunks, maximumNumberOfChunks);
        //string pickedOne;
        int pickedOneInt;
        producedChunks = new List<GameChunk>();
        SpriteRenderer spriteRender;
        string filePath = "WarrensTextures/";
        string[] typeOfDungeonTexture = { "warrens." };
        string[] specificTextureType = { "corridor_wall.", "corridor_door.basic", "endhall.01"};
        GameObject tempObject;
        GameChunk tempChunk;

        int i;

        for (i = 0; i < objectsToGenerate; i++)
        {
            pickedOneInt = randomNumber.Next(1, 7);
            //pickedOne = pickedOneInt.ToString();
            tempObject = Instantiate(factoryObject);
            tempChunk = new GameChunk(tempObject,pickedOneInt);
            tempObject.SetActive(false);
            producedChunks.Add(tempChunk);
        
            spriteRender = producedChunks[i].getProducedObject().AddComponent<SpriteRenderer>();//jest jeszcze GetComponent

            if (i == 0 || i == objectsToGenerate - 1)
            {
                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[1]);
            }
            else
            {
                if(producedChunks[i - 1].getRandomizedNumber() == producedChunks[i].getRandomizedNumber())
                {
                    while(producedChunks[i - 1].getRandomizedNumber() == producedChunks[i].getRandomizedNumber())
                    {
                        producedChunks[i].setRandomizedNumber(randomNumber.Next(1, 7));
                    }
                }

                spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[0] + producedChunks[i].getRandomizedNumber());
            }

            producedChunks[i].getProducedObject().transform.Translate(Vector2.right * chunkWidth * i);
            producedChunks[i].getProducedObject().name = "DungeonChunk_" + i.ToString();
            producedChunks[i].getProducedObject().SetActive(true);
        }

        for (i = 0; i < 2; i++)
        {
            tempObject = Instantiate(factoryObject);
            tempChunk = new GameChunk(tempObject, -1);
            tempObject.SetActive(false);
            producedChunks.Add(tempChunk);
            spriteRender = producedChunks[objectsToGenerate + i].getProducedObject().AddComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[0] + specificTextureType[2]);

            //producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(new Vector2(0,(float)0.4));
            if (i == 0)
            {
                producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(Vector2.left * chunkWidth);
                producedChunks[objectsToGenerate + i].getProducedObject().name = "DungeonChunkEntrance";
            }
            else
            {
                //producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(new Vector2(-3, 0));
                spriteRender.flipX = true;
                producedChunks[objectsToGenerate + i].getProducedObject().transform.Translate(Vector2.right * chunkWidth * objectsToGenerate);
                producedChunks[objectsToGenerate + i].getProducedObject().name = "DungeonChunkExit";
            }
            producedChunks[objectsToGenerate + i].getProducedObject().SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    /*
    void SaveItemData()
    {
        if(!File.Exists(Application.persistentDataPath + "/WarrensTextures/items.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/WarrensTextures/Items.data");
            SaveItems data = new SaveItems();
            data.itemName = SecondSprite.name;
            bf.Serialize(file, data);
            file.Close();
        }
        else if (File.Exists(Application.persistentDataPath + "/WarrensTextures/items.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/WarrensTextures/Items.data");
            SaveItems data = new SaveItems();
            data.itemName = SecondSprite.name;
            bf.Serialize(file, data);
            file.Close();
        }
    }*/
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


[System.Serializable]
public class SaveItems
{
    public string itemName;
}

