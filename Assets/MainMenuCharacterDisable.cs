using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacterDisable : MonoBehaviour
{
    GameObject playerCharacter;

    Camera playerCamera;

    bool isDisabled = false;
    
  void Start(){
    playerCharacter = GameObject.FindGameObjectWithTag("Player");
    playerCamera = playerCharacter.GetComponentInChildren<Camera>();
  }
    void Update()
    {
        if(playerCharacter != null && !isDisabled)
        {
            CharacterComponentsDisabler();
            isDisabled = true;
        }
        Debug.Log("Player Object still null");
    }

  private void CharacterComponentsDisabler()
  {
    playerCharacter = GameObject.FindGameObjectWithTag("Player");
        if(playerCharacter != null)
        {
            Debug.Log("Player Found... attempting to find camera");
        }
        Debug.Log("Not found");
        playerCharacter.gameObject.transform.Rotate(0,180,0);

        playerCamera = playerCharacter.GetComponentInChildren<Camera>();

        if(playerCamera !=null)
        {
        playerCamera.gameObject.SetActive(false);
        }
        else{
            Debug.Log("Did not find Player Camera");
        }
  }
}
