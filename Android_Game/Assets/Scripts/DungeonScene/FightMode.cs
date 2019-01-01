using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMode : MonoBehaviour {
    public static GameObject dungeonCanvas;
    public static ButtonForCameraMovement buttonForCameraMovement;
    public static ButtonForUsage buttonForUsage;
    public static DungeonManager dungeonManager;
    public static DungeonsGenerator dungeonsGenerator;
    public static EnemyGenerator enemyGenerator;
    public static EnemyParty enemyParty;
    public static ObjectSelector objectSelector;
    public static DisplayParty displayParty;
    public static int currentCorridorId;
    public static bool partyIsInFightMode;

    //Values to remember from corridor stage to resume it
    public static int previousCorridorId;
    public static int arrayIdOfPartyWeAreFightingWith;
    public static float[] enemyPossitionsBeforeFight;
    public static int colidedWithPartyNumber;
    //public static 


	// Use this for initialization
	void Start () {
        dungeonCanvas = GameObject.Find("Dungeon");
        dungeonManager = dungeonCanvas.GetComponent<DungeonManager>();
        dungeonsGenerator = dungeonCanvas.GetComponent<DungeonsGenerator>();
        enemyGenerator = dungeonCanvas.GetComponent<EnemyGenerator>();
        buttonForCameraMovement = GameObject.Find("MoveRightButton").GetComponent<ButtonForCameraMovement>();
        buttonForUsage = GameObject.Find("UseButton").GetComponent<ButtonForUsage>();
        objectSelector = dungeonCanvas.GetComponent<ObjectSelector>();
        displayParty = dungeonCanvas.GetComponent<DisplayParty>();
        partyIsInFightMode = false;
        enemyPossitionsBeforeFight = new float[4];
        colidedWithPartyNumber = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (partyIsInFightMode == false)
        {
            currentCorridorId = dungeonsGenerator.getIdOfCorridor();
            checkIfThereShouldBeAFight(currentCorridorId);
        }
	}

    public void checkIfThereShouldBeAFight(int currentCorridorId)
    {
        int howManyEnemyPartiesAreThere = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId).getEnemyParties().Count;

        //I should mind that it is not needed if there are no enemy parties at all at this corridor
        //So first we need to check if there is atleast one party and if so -> get needed unitWidth from it
        if (howManyEnemyPartiesAreThere != 0)
        {
            //There is atleast one party -> so we are going to get value from that one (0)
            float enemyPartyWidth = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId).getEnemyParties()[0].getOverallEnemyWidth();
            int howManyHeroesAreThere = buttonForCameraMovement.getSizeOfParty();
            //Check how many heroes are there
            //iterate through objects in unity gui and check who is furthest in booth sides
            //that way you dont need ButtonForCameraMovement
            //Had to initialize it due to uncertain initialization conditions(i made sure they are certain tho)
            float furthestPosition = 0;
            float nearestPosition = 0;

            //Checking what point of party is the furthest
            for (int i = 0; i < howManyHeroesAreThere; i++)
            {
                if (displayParty.getHeroIsAlive()[i] == true)
                {
                    if (i == 0)
                    {
                        furthestPosition = GameObject.Find("HeroObject" + (i + 1)).transform.position.x;
                        nearestPosition = furthestPosition;
                    }
                    else
                    {
                        if (GameObject.Find("HeroObject" + (i + 1)).transform.position.x > furthestPosition)
                        {
                            furthestPosition = GameObject.Find("HeroObject" + (i + 1)).transform.position.x;
                        }
                        if (GameObject.Find("HeroObject" + (i + 1)).transform.position.x < furthestPosition)
                        {
                            nearestPosition = GameObject.Find("HeroObject" + (i + 1)).transform.position.x;
                        }
                    }
                }
            }
            //To aquire furthest point we have to add unit legth to the value because now its rendered fom thi point
            furthestPosition += enemyPartyWidth;
            bool noCollisionDetected = true;

            //Debug.Log("FightMode || checkIfThereShouldBeAFight || Nearest position: " + nearestPosition + " FurthestPossition: " + furthestPosition);


            //Now it's time to find enemies like that
            //For now only checking it with furthest point -> encounters only from going right
            for (int i = 0; i < howManyEnemyPartiesAreThere; i++)
            {
                //Getting initial possition of party to check
                float initX = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId).getEnemyParties()[i].getInitialPositionX();
                float sizeOfParty = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId).getEnemyParties()[i].getEnemyObjectArray().Count;

                if ((furthestPosition > initX) && (furthestPosition <= initX + (sizeOfParty * enemyPartyWidth)))
                {
                    noCollisionDetected = false;
                    colidedWithPartyNumber = i;
                    i = howManyEnemyPartiesAreThere;
                    //Debug.Log("FightMode || checkIfThereShouldBeAFight || Nearest position: " + nearestPosition + " FurthestPossition: " + furthestPosition);
                }

                //If there were collisions
                if (noCollisionDetected == false)
                {
                    Debug.Log("FightMode || checkIfThereShouldBeAFight || Encountered enemy troops!");
                    partyIsInFightMode = true;
                    loadFightMode(colidedWithPartyNumber);
                    Debug.Log("FightMode || checkIfThereShouldBeAFight || Encountered enemy troops!" + colidedWithPartyNumber);
                    //If there will be a problem actualize current corridor id here
                    previousCorridorId = buttonForUsage.getPreviousCorridorId();
                    arrayIdOfPartyWeAreFightingWith = i;
                    break;
                }
            }


            //Get heroes possitions
            //Get Enemy possitions
        }
    }

    public void loadFightMode(int idOfEnemyParty)
    {
        objectSelector.loadEnemyPossition(currentCorridorId, idOfEnemyParty);

        //Type of 2 means fightMode
        dungeonsGenerator.loadFightLevel(currentCorridorId, idOfEnemyParty);

        //We are calling displayParty class to create health bars for heroes in fight scene
        displayParty.displayHealthBarsOnHeroes(true);
        objectSelector.initializeHighlightOnFirstEnemy();

        //previousCorridorId = currentCorridorId;
        //currentCorridorId = choosenCorridorId;
        //Debug.Log("ButtonForUsage || doorTransition || doTransitionPreparation || currentCorridorNumber: " + currentCorridorId);
        //Debug.Log("ButtonForUsage || doorTransition || doTransitionPreparation || Length of corridorList from dungeonGenerator: " + dungeonGenerator.getCorridorList().Count);
        //currentCorridor = dungeonManager.getLevelsArray().Find(x => x.getIdOfLevel() == currentCorridorId);
    }

    public void setPartyIsInFightMode(bool isItInFightMode)
    {
        partyIsInFightMode = isItInFightMode;
    }

    public void setCurrentCorridorId(int idToSet)
    {
        currentCorridorId = idToSet;
    }

    public void setPreviousCorridorId(int idToSet)
    {
        previousCorridorId = idToSet;
    }

    public void setEnemyPossitionBeforeFight(int idOfEnemy,float enemyPossitionToSet)
    {
        enemyPossitionsBeforeFight[idOfEnemy] = enemyPossitionToSet;
    }

    public void setArrayIdOfPartyWeAreFightingWith(int idToSet)
    {
        arrayIdOfPartyWeAreFightingWith = idToSet;
    }

    public void setColidedWithPartyNumber(int idToSet)
    {
        colidedWithPartyNumber = idToSet;
    }

    public bool getPartyIsInFightMode()
    {
        return partyIsInFightMode;
    }

    public int getCurrentCorridorId()
    {
        return currentCorridorId;
    }

    public int getPreviousCorridorId()
    {
        return previousCorridorId;
    }

    public float getEnemyPossitionBeforeFight(int idOfEnemy)
    {
        return enemyPossitionsBeforeFight[idOfEnemy];
    }

    public int getArrayIdOfPartyWeAreFightingWith()
    {
        return arrayIdOfPartyWeAreFightingWith;
    }

    public int getColidedWithPartyNumber()
    {
        return colidedWithPartyNumber;
    }
}
