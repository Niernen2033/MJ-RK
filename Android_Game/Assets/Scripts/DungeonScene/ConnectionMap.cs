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
        }
        for (int i = 0; i < numberOfCorridors; i++)
        {
            do
            {
                tempNumb = randomNumber.Next(0, numberOfCorridors);
            } while (tempNumb == i || corridorDependenciesList[i].getNeighbourCorridor().Contains(tempNumb));
            //Preventing adding corridor itself or duplicating already added ones

            //Checking if there is no more corridors than 4 (eighter in initial corridor and in the destinated one)
            if (corridorDependenciesList[i].getNeighbourCorridor().Count < 4 && corridorDependenciesList[tempNumb].getNeighbourCorridor().Count < 4)
            {
                corridorDependenciesList[i].addToNeighbourCorridor(tempNumb);
                corridorDependenciesList[tempNumb].addToNeighbourCorridor(i);
            }
        }

        for(int i = 0; i < corridorDependenciesList.Count; i++)
        {
            corridorDependenciesList[i].toString(i);
        }
    }
    //*************** TO DO ***************
    //I have to make sure that every corridor has atleast one connection ( to prevent that we will propably make
    //unique pairs of corridors and then linkining theese. Next step would be making extra connections
    //it would be also be nice to do not make to much of them

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

    public CorridorDependency()
    {
        neighbourCorridor = new List<int>();
    }

    public void setNeighbourCorridor(List<int> list)
    {
        neighbourCorridor = list;
    }

    public void addToNeighbourCorridor(int neighbour)
    {
        if (!neighbourCorridor.Contains(neighbour))
        {
            neighbourCorridor.Add(neighbour);
        }
    }

    public List<int> getNeighbourCorridor()
    {
        return neighbourCorridor;
    }

    public int getSpecificNeighbourCorridor(int whichOne)
    {
        return neighbourCorridor[whichOne];
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
