using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMotion : MonoBehaviour {

    public Button leftButton,rightButton;
    //public Camera camera;

	// Use this for initialization
	void Start () {
        Button leftButtonActive = leftButton.GetComponent<Button>();
        Button rightButtonActive = rightButton.GetComponent<Button>();

        leftButtonActive.onClick.AddListener(cameraMoveLeft);
        //btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void cameraMoveLeft()
    {
        //Camera tempCamera = camera.GetComponent<Camera>();
        Camera.main.transform.Translate(Vector2.left);
    }
}
