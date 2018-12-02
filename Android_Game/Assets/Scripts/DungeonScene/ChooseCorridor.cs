using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChooseCorridor : MonoBehaviour
{

    private static GameObject dungeonCanvas;
    private double canvasWidth, canvasHeight;
    private int idOfPopup;
    private List<int> corridorsIdArray;
    private System.Random randomNumber;
    private List<DungeonLevel> listOfAllLevels;
    private int idOfCurrentCorridor;
    private ConnectionMap connectionMap;
    private int choosenCorridorId;
    private static bool isCorridorChoosen;


    Rect popupWindow;
    private static float popupWidth;
    private static float popupHeight;
    private static float popupOptionWidth;
    private static float popupOptionHeight;
    private static float popupBorderSpaceWidth;
    private static float popupBorderSpaceHeight;
    private static float popupSpaceBetweenWidth;
    private static float popupSpaceBetweenHeight;
    private static float popupHeightSpaceForTitle;

    // Use this for initialization
    void Start()
    {
        choosenCorridorId = 0;
        isCorridorChoosen = false;
        idOfPopup = 0;
        popupWidth = (float)(Screen.width * 0.8);
        popupHeight = (float)(Screen.height * 0.6);
        popupOptionWidth = (float)(popupWidth * 0.45);
        popupOptionHeight = (float)(popupHeight * 0.4);

        popupBorderSpaceWidth = (float)(popupWidth * 0.03);
        popupBorderSpaceHeight = (float)(popupHeight * 0.03);
        popupSpaceBetweenWidth = (float)(popupWidth * 0.04);
        popupSpaceBetweenHeight = (float)(popupHeight * 0.04);
        popupHeightSpaceForTitle = (float)(popupHeight * 0.1);

        //Width has 10% of spaces between - 2x3% for border and 4% between
        //Height has 10% of spaces between and 10% for title - 2x3% for border and 4% between
        connectionMap = GameObject.Find("Dungeon").GetComponent<ConnectionMap>();

        //Now we are going to gain some information about near corridors
        listOfAllLevels = GameObject.Find("Dungeon").GetComponent<DungeonManager>().getLevelsArray();

        randomNumber = new System.Random(DateTime.Now.Millisecond);
        int howManyCorridorsToChoose = randomNumber.Next(1, 5);
        corridorsIdArray = new List<int>();

        //Random numbers for dev purposes

        for (int i=0; i<howManyCorridorsToChoose; i++)
        {
            corridorsIdArray.Add(randomNumber.Next(0, 10));
        }

        //Debug.Log("ChooseCorridor || DungeonCanvas width:" + canvasWidth + " || DungeonCanvas height: " + canvasHeight);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.Window(idOfPopup, popupWindow, displayOptionsToChooseFrom, "Where should we go?");
    }

    public void showCorridorDecissionPopup(int corridorId)
    {
        idOfCurrentCorridor = corridorId;
        popupWindow = new Rect(Screen.width / 2 - popupWidth / 2, Screen.height / 2 - popupHeight / 2, popupWidth, popupHeight);
        //return choosenCorridorId;
    }

    void displayOptionsToChooseFrom(int idOfPopup)
    {
        List<int> neighbourCorridorList = connectionMap.getCorridorDependenciesList()[idOfCurrentCorridor].getNeighbourCorridor();
        //conMap.getCorridorDependenciesList()[currentCorridorNumber].getSpecificNeighbourCorridor(randomizedOne);
        for (int i = 0; i < neighbourCorridorList.Count; i++)
        {
            if (i == 0)
            {
                if (GUI.Button(new Rect(popupBorderSpaceWidth, popupHeightSpaceForTitle + popupBorderSpaceHeight, popupOptionWidth, popupOptionHeight), "Corridor" + neighbourCorridorList[i]))
                {
                    choosenCorridorId = neighbourCorridorList[i];
                    popupWindow = new Rect(0, 0, 0, 0);
                    isCorridorChoosen = true;
                }
            }       
            else if(i == 1)
            {
                if (GUI.Button(new Rect(popupBorderSpaceWidth + popupOptionWidth + popupSpaceBetweenWidth, popupHeightSpaceForTitle + popupBorderSpaceHeight, popupOptionWidth, popupOptionHeight), "Corridor" + neighbourCorridorList[i]))
                {
                    choosenCorridorId = neighbourCorridorList[i];
                    popupWindow = new Rect(0, 0, 0, 0);
                    isCorridorChoosen = true;
                }
            }
            else if( i == 2)
            {
                if (GUI.Button(new Rect(popupBorderSpaceWidth, popupHeightSpaceForTitle + popupBorderSpaceHeight + popupOptionHeight + popupSpaceBetweenHeight, popupOptionWidth, popupOptionHeight), "Corridor" + neighbourCorridorList[i]))
                {
                    choosenCorridorId = neighbourCorridorList[i];
                    popupWindow = new Rect(0, 0, 0, 0);
                    isCorridorChoosen = true;
                }
            }
            else
            {
                if (GUI.Button(new Rect(popupBorderSpaceWidth + popupOptionWidth + popupSpaceBetweenWidth, popupHeightSpaceForTitle + popupBorderSpaceHeight + popupOptionHeight + popupSpaceBetweenHeight, popupOptionWidth, popupOptionHeight), "Corridor" + neighbourCorridorList[i]))
                {
                    choosenCorridorId = neighbourCorridorList[i];
                    popupWindow = new Rect(0, 0, 0, 0);
                    isCorridorChoosen = true;
                }
            }
        }
    }

    public void setChoosenCorridorId(int idToSet)
    {
        choosenCorridorId = idToSet;
    }

    public void setIsCorridorChoosen(bool isIt)
    {
        isCorridorChoosen = isIt;
    }

    public int getChoosenCorridorId()
    {
        return choosenCorridorId;
    }

    public bool getIsCorridorChoosen()
    {
        return isCorridorChoosen;
    }

}
