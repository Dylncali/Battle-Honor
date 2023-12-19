using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler1 : MonoBehaviour
{
    Vector2 moveInputVector;
    public Vector2 cameraInputVector;
    LocalCameraHandler localCameraHandler;

    bool isJumpButtonPressed = false;

    private void Awake()
    {
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //move Input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        //camera Input
        cameraInputVector.x= Input.GetAxis("Mouse X");
        cameraInputVector.y = Input.GetAxis("Mouse Y") *-1;


        if(Input.GetButtonDown("Jump"))
        isJumpButtonPressed = true;

    }

    public NetworkMovement GetNetworkInput()
    {
        NetworkMovement networkMovement = new NetworkMovement();

        //Handles camera rotation
        networkMovement.lookDirection = cameraInputVector;
        

        //Move data
        networkMovement.direction = moveInputVector;

        //Is player Jumping?
        networkMovement.isJumping = isJumpButtonPressed;

        //reset jump button
        isJumpButtonPressed = false;

        return networkMovement;
    }
}
