using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovementHandler1 : NetworkBehaviour
{
   PlayerMovement playerMovement;
    


   [SerializeField] Camera localCamera;

   private void Awake()
    {
            playerMovement = GetComponent<PlayerMovement>();
            
    }


    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkMovement networkMovement))
        {
            //rotateing camera view
           playerMovement.Rotate(networkMovement.lookDirection.x); 

           //Cancel out player rotation so camera does not tilt
           

            //handle player movement
            Vector3 moveDirection = transform.forward * networkMovement.direction.y + transform.right*networkMovement.direction.x;
            moveDirection.Normalize();
            playerMovement.Move(moveDirection);

            //jump
            if(networkMovement.isJumping)
            {
                playerMovement.Jump();
            }

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
