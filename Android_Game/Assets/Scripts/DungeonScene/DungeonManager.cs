using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    private ConnectionMap conMap;

    //[SerializeField]
    private int numberOfDungeonLevels;

    private static List<DungeonLevelChunk> levelChunks;//list of chunks must be shared
    private DungeonLevelChunk tempChunkObject;

    // Use this for initialization
    void Start () {
        tempChunkObject = new DungeonLevelChunk();
        conMap = GameObject.Find("Dungeon").GetComponent<ConnectionMap>();
        numberOfDungeonLevels = conMap.getNumberOfCorridors();

        levelChunks = new List<DungeonLevelChunk>();

        for(int i=0;i<numberOfDungeonLevels;i++)
        {
            tempChunkObject.setIdOfChunk(i);
            levelChunks.Add(tempChunkObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<DungeonLevelChunk> GetLevelChunks()
    {
        return levelChunks;
    }
}

public class DungeonLevelChunk{
    private int idOfChunk;
    private bool wasCreated;

    public DungeonLevelChunk()
    {
        wasCreated = false;
    }

    public void setIdOfChunk(int idOfChunk)
    {
        this.idOfChunk = idOfChunk;
    }

    public int getIdOfChunk()
    {
        return this.idOfChunk;
    }

    public void setWasCreated(bool wasCreated)
    {
        this.wasCreated = wasCreated;
    }

    public bool getWasCreated()
    {
        return this.wasCreated;
    }
}