using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;



public class AnimationStateController : NetworkBehaviour
{
    Animator animator;


    

    
    void Awake()
    {
            animator = GetComponent<Animator>();
        
    }



    // public void activateAtk(NetworkBool leftClick)
    // {
    //     bool isAtking = animator.GetBool("LightAtk");
        
    //     if(!isAtking && leftClick)
    //     {
    //         animator.SetBool("LightAtk",true);
    //     }
    //     if(isAtking && !leftClick)
    //     {
    //         animator.SetBool("LightAtk",false);
    //     }

    // }

    public void movement(Vector3 direction)
    {
        bool isWalking = animator.GetBool("IsWalking");
        
       
        if (!isWalking && direction.magnitude>0)
        {
            animator.SetBool("IsWalking", true);

        }
        if (isWalking && direction.magnitude==0)
        {
            animator.SetBool("IsWalking", false);
        }

        
    }


    // public void backwordmovement(Vector3 direction)
    // {
    //     bool bkwd = animator.GetBool("movbkw");
        
       
    //     if (!bkwd && direction.magnitude<0)
    //     {
    //         animator.Play("MoveBWD_Battle_RM_SwordAndShield 0");

    //     }
    //     if (bkwd && direction.magnitude==0)
    //     {
    //         animator.SetBool("movbkw", false);
    //     }
    // }

    public void rollAnimation(NetworkBool rollPressed)
    {
        if(rollPressed)
        {
            StartCoroutine(rolling());
        }
    }
      IEnumerator rolling()
    {

        animator.Play("Roll_forward");
        yield return new WaitForSeconds(1f);

    }
}