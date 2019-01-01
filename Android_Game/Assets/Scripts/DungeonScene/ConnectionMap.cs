using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMap : MonoBehaviour {

    [SerializeField]
    private int numberOfCorridors;

    private static List<CorridorDependency> corridorDependenciesList;
    private System.Random randomNumber;

    // Use this for initialization
    void Start () {
        randomNumber = new System.Random();
        corridorDependenciesList = new List<CorridorDependency>();
        int tempNumb;

        for (int i = 0; i < numberOfCorridors; i++)
        {
            corridorDependenciesList.Add(new CorridorDependency());
            corridorDependenciesList[i].setIsLinkedWithInitialCorridor(false);
        }
        for (int i = 0; i < numberOfCorridors; i++)
        {
            if (i == 0)
            {
                //First one must be defined as initial
                corridorDependenciesList[i].setIsLinkedWithInitialCorridor(true);
            }
            else
            {
                do
                {
                    tempNumb = randomNumber.Next(0, numberOfCorridors);
                } while (tempNumb == i || corridorDependenciesList[i].getNeighbourCorridor().Contains(tempNumb) || corridorDependenciesList[tempNumb].getIsLinkedWithInitialCorridor()==false);
                //Preventing adding corridor itself, duplicating already added ones and making sure it's connected with initial one

                //Checking if there is no more corridors than 4 (eighter in initial corridor and in the destinated one)
                if (corridorDependenciesList[i].getNeighbourCorridor().Count < 4 && corridorDependenciesList[tempNumb].getNeighbourCorridor().Count < 4)
                {
                    corridorDependenciesList[i].addToNeighbourCorridor(tempNumb);
                    corridorDependenciesList[tempNumb].addToNeighbourCorridor(i);
                    if (corridorDependenciesList[tempNumb].getIsLinkedWithInitialCorridor() == true)
                    {
                        corridorDependenciesList[i].setIsLinkedWithInitialCorridor(true);
                    }
                }
            }
        }

        //Making additional connections
        for(int i = 0; i < corridorDependenciesList.Count; i++)
        {
            //If certain corridor have less than for connections
            if(corridorDependenciesList[i].getNeighbourCorridor().Count < 4)
            {
                //Checking if we should add corridor or not
                int tempNumb2 = randomNumber.Next(0, 2);
                if (tempNumb2 == 1)
                {
                    //Looking for possible match
                    do
                    {
                        tempNumb = randomNumber.Next(0, numberOfCorridors);
                    } while (tempNumb == i || corridorDependenciesList[i].getNeighbourCorridor().Contains(tempNumb) || corridorDependenciesList[tempNumb].getNeighbourCorridor().Count >= 4);

                    corridorDependenciesList[i].addToNeighbourCorridor(tempNumb);
                    corridorDependenciesList[tempNumb].addToNeighbourCorridor(i);
                }
            }
        }
        

        for(int i = 0; i < corridorDependenciesList.Count; i++)
        {
            corridorDependenciesList[i].toString(i);
        }
        
    }

    public int getNumberOfCorridors()
    {
        return numberOfCorridors;
    }

    public List<CorridorDependency> getCorridorDependenciesList()
    {
        return corridorDependenciesList;
    }

    // Update is called once per frame
    void Update () {
	}

    void generateDependencies()
    {

    }
}

public class CorridorDependency
{
    private List<int> neighbourCorridor;
    private bool isLinkedWithInitialCorridor;

    public CorridorDependency()
    {
        neighbourCorridor = new List<int>();
        isLinkedWithInitialCorridor = false;
    }

    public void addToNeighbourCorridor(int neighbour)
    {
        if (!neighbourCorridor.Contains(neighbour))
        {
            neighbourCorridor.Add(neighbour);
        }
    }

    public void setNeighbourCorridor(List<int> list)
    {
        neighbourCorridor = list;
    }

    public void setIsLinkedWithInitialCorridor(bool isIt)
    {
        isLinkedWithInitialCorridor = isIt;
    }

    public List<int> getNeighbourCorridor()
    {
        return neighbourCorridor;
    }

    public int getSpecificNeighbourCorridor(int whichOne)
    {
        return neighbourCorridor[whichOne];
    }

    public bool getIsLinkedWithInitialCorridor()
    {
        return isLinkedWithInitialCorridor;
    }

    public void toString(int Id)
    {
        string temp = "";
        for (int i=0; i<neighbourCorridor.Count;i++)
        {
            temp += neighbourCorridor[i] + " ";
        }

        Debug.Log("ConnectionMap || Corridor " + Id + " is linked with corridors: " + temp);
    }
}
