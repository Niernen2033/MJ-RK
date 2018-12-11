using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForEscape : MonoBehaviour {

    [SerializeField]
    private static GameObject escapeButton;
    private static System.Random randomNumber;

    private static GameObject dungeon;
    private static DungeonsGenerator dungeonsGenerator;
    private static FightMode fightMode;

    // Use this for initialization
    void Start () {
        escapeButton = GameObject.Find("ButtonForEscape");
        dungeon = GameObject.Find("Dungeon");
        escapeButton.GetComponent<Button>().onClick.AddListener(escapeFromFight);
        randomNumber = new System.Random();
        dungeonsGenerator = dungeon.GetComponent<DungeonsGenerator>();
        fightMode = dungeon.GetComponent<FightMode>();
        escapeButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void escapeFromFight()
    {
        //Randomizes chance and checks output
        //There is 70% of chance of escape and 30% of failure
        if(randomNumber.Next(0, 101)<=70)
        {
            //return true;
            Debug.Log("Escape succesfull!");
            Debug.Log("ButtonForEscape || escapeFromFight || Loading level after fight scene! Scene: " + fightMode.getCurrentCorridorId());
            dungeonsGenerator.loadAnotherLevel(fightMode.getCurrentCorridorId(), 2);
        }
        else
        {
            Debug.Log("Escape unsuccesfull!");
        }
    }
}
