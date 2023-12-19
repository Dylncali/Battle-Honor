using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    public Vector2 cameraInputVector = Vector2.zero;
    LocalCameraHandler localCameraHandler;

    bool isJumpButtonPressed = false;
    bool isAtking = false;
    PlayerMovementHandler playerMovementHandler;

    private void Awake()
    {
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
        playerMovementHandler = GetComponent<PlayerMovementHandler>();
    }



    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            return;
        if(!playerMovementHandler.Object.HasInputAuthority)
            return;
        //move Input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        //camera Input
        cameraInputVector.x= Input.GetAxis("Mouse X");
        cameraInputVector.y = Input.GetAxis("Mouse Y");

        if(Input.GetMouseButtonDown(0))
        isAtking = true;

        if(Input.GetButtonDown("Jump"))
        isJumpButtonPressed = true;

    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Handles camera rotation
        networkInputData.aimForwardVector = cameraInputVector;
        

        //Move data
        networkInputData.movementInput = moveInputVector;

        //Is player Jumping?
        networkInputData.isJumpPressed = isJumpButtonPressed;

        //reset jump button
        isJumpButtonPressed = false;

        networkInputData.isAtkingPressed = isAtking;

        isAtking = false;

        return networkInputData;
    }
}
