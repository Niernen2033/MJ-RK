using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayParty : MonoBehaviour {

    [SerializeField]
    private GameObject[] heroObject = new GameObject[4];

    [SerializeField]
    private List<string> characterType = new List<string>();

    void Start () {

        SpriteRenderer spriteRender;
        string filePath = "HeroesModels/";
        for (int i = 0; i < 4; i++)
        {
            string[] typeOfDungeonTexture = { "crusader", "jester", "abomination", "vestal"};
            spriteRender = heroObject[i].GetComponent<SpriteRenderer>();
            spriteRender.sprite = Resources.Load<Sprite>(filePath + typeOfDungeonTexture[i]);
        }
    }
	
	void Update () {
		
	}
}
