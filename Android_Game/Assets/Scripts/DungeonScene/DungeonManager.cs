using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    private ConnectionMap conMap;

    //[SerializeField]
    private int numberOfDungeonLevels;
    //TODO: Eliminating LevelChunks
    private static List<DungeonLevel> levelsArray;//Array of levels
    private static List<DungeonLevelChunk> levelChunks;
    private DungeonLevelChunk tempChunkObject;

    // Use this for initialization
    void Start () {
        //Array of levels
        levelsArray = new List<DungeonLevel>();

        tempChunkObject = new DungeonLevelChunk();
        tempChunkObject.setWasCreated(false);
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

    public List<DungeonLevel> getLevelsArray()
    {
        return levelsArray;
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

public class DungeonLevel
{
    private int idOfLevel;
    private int numberOfChunks;
    private List<LevelChunk> chunksArray;
    private List<EnemyParty> enemyParties;

    public DungeonLevel(int idOfLevel, int numberOfChunks)
    {
        this.idOfLevel = idOfLevel;
        this.numberOfChunks = numberOfChunks;
        chunksArray = new List<LevelChunk>();
        enemyParties = new List<EnemyParty>();

        for(int i=0; i < numberOfChunks; i++)
        {
            //By default we're setting texture as 0
            chunksArray.Add(new LevelChunk(0));
        }
    }

    public void setIdOfLevel(int idOfLevel)
    {
        this.idOfLevel = idOfLevel;
    }

    public void setNumberOfChunks(int numberOfChunks)
    {
        this.numberOfChunks = numberOfChunks;
    }

    public void setChunkArrayElementTexture(int indexOfChunk, int textureId)
    {
        chunksArray[indexOfChunk].setIdOfAppliedTexture(textureId);
    }

    public void addToEnemyParties(EnemyParty partyToAdd)
    {
        enemyParties.Add(partyToAdd);
    }

    public int getIdOfLevel()
    {
        return idOfLevel;
    }

    public int getNumberOfChunks()
    {
        return numberOfChunks;
    }

    public int getChunkArrayElementTexture(int indexOfChunk)
    {
        return chunksArray[indexOfChunk].getIdOfAppliedTexture();
    }

    public List<EnemyParty> getEnemyParties()
    {
        return enemyParties;
    }

    public EnemyParty getSpecificEnemyParty(int id)
    {
        return enemyParties[id];
    }
}

public class LevelChunk
{
    private int idOfAppliedTexture;

    public LevelChunk(int idOfAppliedTexture)
    {
        this.idOfAppliedTexture = idOfAppliedTexture;
    }

    public void setIdOfAppliedTexture(int idOfAppliedTexture)
    {
        this.idOfAppliedTexture = idOfAppliedTexture;
    }

    public int getIdOfAppliedTexture()
    {
        return idOfAppliedTexture;
    }
}