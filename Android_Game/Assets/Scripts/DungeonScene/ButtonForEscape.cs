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
    private static DisplayParty displayParty;

    // Use this for initialization
    void Start () {
        escapeButton = GameObject.Find("ButtonForEscape");
        dungeon = GameObject.Find("Dungeon");
        displayParty = dungeon.GetComponent<DisplayParty>();
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
        //There is 30% of chance of escape and 70% of failure
        if(randomNumber.Next(0, 101)<=30)
        {
            Debug.Log("Escape succesfull!");
            Debug.Log("ButtonForEscape || escapeFromFight || Loading level after fight scene! Scene: " + fightMode.getCurrentCorridorId());
            dungeonsGenerator.loadAnotherLevel(fightMode.getCurrentCorridorId(), 2);
            GameObject.Find("EnemyHighlightMaskObject").SetActive(false);
        }
        else
        {
            Debug.Log("Escape unsuccesfull!");
            displayParty.dealDamageToHero(randomNumber.Next(0, displayParty.getNumberOfHeroesAlive()), randomNumber.Next(9, 30));
        }
    }
}
