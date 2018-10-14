using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class buttonForCameraMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed;
    private float speedTimer;
    //timeLimit is for making sure camera is moved every few frames
    private double timeLimit=0.125;

    [SerializeField]
    private short cameraMovementDirection;
    //There are two movement directions -1 (left direction) and 1 (right direction)

    [SerializeField]
    private UnityEvent buttonHold;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        speedTimer = 0;
    }

    public void Update()
    {
        
        if(isButtonPressed)
        {
            speedTimer += Time.deltaTime;
            if (speedTimer > timeLimit)
            {
                if (cameraMovementDirection == 1)
                {
                    Camera.main.transform.Translate(Vector2.right);
                }
                if (cameraMovementDirection == -1)
                {
                    Camera.main.transform.Translate(Vector2.left);
                }
                speedTimer = 0;
            }
        }
    }
}
