using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
public class PlayerMovementHandler : NetworkBehaviour
{
   PlayerMovement playerMovement;

   AnimationStateController animationStateContoller;

   HPHandler hPHandler;
   

   public Camera localCamera;
   float cameraRoationY = 0f;

   

   private void Awake()
    {
            playerMovement = GetComponent<PlayerMovement>();
            animationStateContoller = GetComponent<AnimationStateController>(); 
            hPHandler = GetComponent<HPHandler>();
            
    }

     


    public override void FixedUpdateNetwork()
    {
            if (SceneManager.GetActiveScene().buildIndex== 1)
                return;
        
        
            if(GetInput(out NetworkInputData networkInputData))
            {
                //rotateing camera view
            playerMovement.Rotate(networkInputData.aimForwardVector.x);
            cameraRoationY -= networkInputData.aimForwardVector.y*playerMovement.rotationSpeedY* Runner.DeltaTime;
            cameraRoationY= Mathf.Clamp(cameraRoationY, -45f, 45f);
            localCamera.transform.localRotation = Quaternion.Euler(cameraRoationY,0,0);
            

        
            

                //handle player movement
                Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right*networkInputData.movementInput.x;
                moveDirection.Normalize();
                playerMovement.Move(moveDirection);
                

                //jump
                // if(networkInputData.isRollPressed)
                // {
                //     playerMovement.Jump();
                    
                // }
                animationStateContoller.movement(moveDirection);
                // animationStateContoller.backwordmovement(moveDirection);
                animationStateContoller.rollAnimation(networkInputData.isRollPressed);
                
                // animationStateContoller.activateAtk(networkInputData.isAtkingPressed);
                


                CheckDeathRespawn();
        
        }
    }

    void CheckDeathRespawn()
    {
        if(transform.position.y < -12)
        {
            transform.position = Utils.GetSpawnPoint();
            
        }
    }

    
}
