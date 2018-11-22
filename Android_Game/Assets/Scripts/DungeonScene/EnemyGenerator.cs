using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GameObject someObjectBoi= new GameObject();
        //someObjectBoi.layer = 0;
        //GameObject another = Instantiate(someObjectBoi);

		
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

    private float initialPositionX;
    private int lengthOfCorridor;
    private int idOfCorridor;
    private const int chunkWidth = 7;
    //This is needed to randomize next number based on the last one (will prevent generating same thing)
    private static int milisecondsForRand = 0;

    private static List<string> characterType = new List<string>();
    private const int minimalSizeOfParty = 1, maximumSizeOfParty = 4;

    public EnemyParty(int lengthOfCorridor, int idOfCorridor)
    {
        enemyObjectArray = new List<GameObject>();
        randomNumber = new System.Random(DateTime.Now.Millisecond + milisecondsForRand);
        //It is length in number of chunks
        this.lengthOfCorridor = lengthOfCorridor;
        this.idOfCorridor = idOfCorridor;

        sizeOfParty = randomNumber.Next(minimalSizeOfParty, maximumSizeOfParty);

        GameObject factoryObject = new GameObject();
        factoryObject.layer = 0;
        factoryObject.AddComponent<SpriteRenderer>();
        //GameObject.Find("HeroObject1");
        //GameObject tempObject = MonoBehaviour.Instantiate(GameObject.Find("HeroObject1"));//It is needed here to call MonoBehaviour


        //TODO - level info about its length, id and putting it
        initialPositionX = randomNumber.Next(0, (int)(lengthOfCorridor * chunkWidth - ((sizeOfParty - 1) * 2.081015))*1000000)/1000000;
        //initialPositionX = (float)(lengthOfCorridor * chunkWidth - ((sizeOfParty-1)*2.081015));

        SpriteRenderer spriteRender;
        string filePath = "Enemies/";
        string[] typeOfDungeonTexture = { "skeleton", "skeleton_defender", "skeleton_spear", "skeleton_militia" };
        Debug.Log("Creating spooky scarry skeletons!");
        Debug.Log("Size of spookyness: " + sizeOfParty);

        for (int i=0;i<sizeOfParty;i++)
        {
            GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);//It is needed here to call MonoBehaviour
            tempObject.SetActive(false);
            tempObject.name = "EnemyObject" + i;
            enemyObjectArray.Add(tempObject);
            enemyObjectArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);
            spriteRender = enemyObjectArray[i].GetComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
            spriteRender.sortingOrder = 1;
            spriteRender.flipY = true;
            spriteRender.transform.rotation = (Quaternion.Euler(0, 0, 270));
            //Debug.Log("-------------------> SPOOK: " + enemyObjectArray[i].transform.position.x);
            enemyObjectArray[i].transform.localPosition = new Vector3(i*130, -100, 0);//TODO SET POSS
            //Debug.Log("-------------------> POSTSPOOK: " + enemyObjectArray[i].transform.position.x);
            enemyObjectArray[i].transform.localScale = new Vector3(35, 35, 1);
            enemyObjectArray[i].SetActive(true);
            enemyObjectArray[i].transform.parent = null;
            enemyObjectArray[i].transform.localPosition = new Vector3(enemyObjectArray[i].transform.position.x + initialPositionX, enemyObjectArray[i].transform.position.y, 0);//TODO SET POSS
        }
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
}