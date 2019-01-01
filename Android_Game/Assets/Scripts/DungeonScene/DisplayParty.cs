using NPC;
using SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayParty : MonoBehaviour
{

    [SerializeField]
    private GameObject[] heroObject = new GameObject[4];

    [SerializeField]
    private List<string> characterType = new List<string>();

    private static List<Champion> heroStatsObject;

    //For dev purposes
    private static List<double> heroMaxHealth;
    private static int[] heroPositionMap = { 1, 2, 0, 3 };

    private static int numberOfHeroesAlive;
    private static bool[] heroIsAlive = { true, true, true, true };
    private static int numberOfCreatedHealthbars;
    //Array of health bars of heroes
    private List<GameObject> healthBarObjectsArray;
    private static GameObject factoryObject;
    private static ButtonForCameraMovement movementButton;


    void Start()
    {

        numberOfHeroesAlive = 4;
        SpriteRenderer spriteRender;
        string filePath = "HeroesModels/";
        string[] typeOfDungeonTexture = { "Knight", "Rogue", "Lord", "Priest" };
        healthBarObjectsArray = new List<GameObject>();
        movementButton = FindObjectOfType<ButtonForCameraMovement>();

        for (int i = 0; i < numberOfHeroesAlive; i++)
        {
            spriteRender = heroObject[i].GetComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
            heroStatsObject = new List<Champion>();
        }

        //For development purposes
        heroStatsObject.Add(new Champion(ChampionClass.Warrior, ChampionType.Normal, "Knight", 0, 1, new Statistics(100), new Statistics(10), new Statistics(10), new Statistics(10), new Statistics(7), new Statistics(6), new Statistics(10), false, new Items.Equipment(), new List<Items.Item>()));
        heroStatsObject.Add(new Champion(ChampionClass.Rogue, ChampionType.Normal, "Rogue", 0, 1, new Statistics(80), new Statistics(10), new Statistics(10), new Statistics(10), new Statistics(10), new Statistics(5), new Statistics(8), false, new Items.Equipment(), new List<Items.Item>()));
        heroStatsObject.Add(new Champion(ChampionClass.Warrior, ChampionType.Normal, "Lord", 0, 1, new Statistics(90), new Statistics(10), new Statistics(10), new Statistics(10), new Statistics(6), new Statistics(7), new Statistics(9), false, new Items.Equipment(), new List<Items.Item>()));
        heroStatsObject.Add(new Champion(ChampionClass.Priest, ChampionType.Normal, "Priest", 0, 1, new Statistics(70), new Statistics(10), new Statistics(10), new Statistics(10), new Statistics(5), new Statistics(10), new Statistics(5), false, new Items.Equipment(), new List<Items.Item>()));

        heroMaxHealth = new List<double>();
        for (int i = 0; i < 4; i++)
        {
            heroMaxHealth.Add(heroStatsObject[i].Vitality.Acctual);
        }
    }

    void Update()
    {
    }

    public void displayHealthBarsOnHeroes(bool isItFightMode)
    {
        string guiFilePath = "UI_Elements/";
        string[] typeOfBarTexture = { "HealthBarVertical" };

        if (isItFightMode == true)
        {
            //Could simplyfy it to one variable but I'm gonna use it in that form later
            int numberOfBarsToDisplay = numberOfHeroesAlive;
            numberOfCreatedHealthbars = numberOfBarsToDisplay;

            SpriteRenderer spriteRender;
            healthBarObjectsArray.Clear();

            if (GameObject.Find("FactoryObject") == null)
            {
                factoryObject = new GameObject();
                factoryObject.name = "FactoryObject";
                factoryObject.layer = 0;
                factoryObject.AddComponent<SpriteRenderer>();
                Debug.Log("DisplaypParty || displayHealthBarsOnHeroes || Created new FactoryObject");
            }
            else
            {
                Debug.Log("DisplaypParty || displayHealthBarsOnHeroes || Found FactoryObject");
                factoryObject = GameObject.Find("FactoryObject");
            }

            for (int i = 0; i < numberOfBarsToDisplay; i++)
            {
                GameObject tempObject = MonoBehaviour.Instantiate(factoryObject);

                float scaledSpaceBetweenHeroes = 90 * 5 / 7;
                float halfOfCanvasWidth = (float)(GameObject.Find("Dungeon").GetComponent<RectTransform>().rect.width * 0.5);

                tempObject.SetActive(false);
                tempObject.name = "HeroHealthBar_" + i;
                healthBarObjectsArray.Add(tempObject);
                healthBarObjectsArray[i].transform.SetParent(GameObject.Find("Dungeon").transform, false);
                spriteRender = healthBarObjectsArray[i].GetComponent<SpriteRenderer>();

                //We're getting healthBar texture from an array
                spriteRender.sprite = Resources.Load<Sprite>(guiFilePath + typeOfBarTexture[0]);
                spriteRender.sortingOrder = 1;

                //The idea here is as follows: canvas is from -400 to 400 we're getting half of it to reach left end of it
                //next step it to draw heroes away 1 step from left border
                healthBarObjectsArray[i].transform.localPosition = new Vector3(-halfOfCanvasWidth + ((3 - i + 1) * scaledSpaceBetweenHeroes), -100, 0);
                healthBarObjectsArray[i].transform.localScale = new Vector3(25, 25, 1);
                healthBarObjectsArray[i].SetActive(true);
                //Hero height's are uneven so i decided to check height on enemies -> it's 180 so it will be also applied here for aestetics
                healthBarObjectsArray[i].transform.localPosition = new Vector3(heroObject[i].transform.localPosition.x, heroObject[i].transform.localPosition.y + 180, 0);
                healthBarObjectsArray[i].transform.localScale = new Vector3(500, 500, 1);
            }
        }
        actualizeHealthBars();
    }

    //Call it to 
    public void actualizeHealthBars()
    {
        for (int i = 0; i < numberOfHeroesAlive; i++)
        {
            GameObject tempObject = GameObject.Find("HeroHealthBar_" + i);

            tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x,
            (500 * ((float)heroStatsObject[i].Vitality.Acctual / (float)heroMaxHealth[i]))
            , tempObject.transform.localScale.z);
        }
    }

    public void dealDamageToHero(int idOfHero, int amountOfDamage)
    {
        Debug.Log("DisplayParty || dealDamageToHero || IdOfHero: " + idOfHero + " amountOfDamage: " + amountOfDamage);
        double valueAfterInjure = heroStatsObject[idOfHero].Vitality.Acctual - amountOfDamage;
        if(valueAfterInjure <= 0)
        {
            GameSave.Instance.SceneIndex = GameGlobals.SceneIndex.CityScene;
            GameSave.Instance.Update();
            SceneManager.LoadScene((int)GameSave.Instance.SceneIndex);
        }
        heroStatsObject[idOfHero].Vitality.ChangeAcctualValue(valueAfterInjure);
        actualizeHealthBars();
    }

    public void destroyHeroObject(int idOfHero)
    {
        Destroy(GameObject.Find("HeroObject" + (idOfHero + 1)));
    }

    public void setNumberOfHeroesAlive(int numberToSet)
    {
        numberOfHeroesAlive = numberToSet;
    }

    public void setNumberOfCreatedHealthbars(int numberToSet)
    {
        numberOfCreatedHealthbars = numberToSet;
    }

    public int getNumberOfCreatedHealthbars()
    {
        return numberOfCreatedHealthbars;
    }

    public int getNumberOfHeroesAlive()
    {
        return numberOfHeroesAlive;
    }

    public bool[] getHeroIsAlive()
    {
        return heroIsAlive;
    }
}
