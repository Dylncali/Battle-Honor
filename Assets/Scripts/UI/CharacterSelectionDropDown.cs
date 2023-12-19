using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
public class CharacterSelectionDropDown : NetworkBehaviour
{
    [SerializeField] TMP_Dropdown characterDropdownSelection; 
    //  public NetworkRunner networkRunnerPrefab;
    
    
    [HideInInspector]
    bool readyUp = false;

    void Start(){
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject playerObject in playerObjects)
            {
                Camera camera = playerObject.GetComponentInChildren<Camera>();
                Debug.Log("Camera found");
                camera.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
    }


    
    public void CharacterSelection()
    {
        Debug.Log(characterDropdownSelection.options[characterDropdownSelection.value].text);
    }

    public void Readyup()
    {
        if(!readyUp){

            GameObject[] gameObjectsToTransfer = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject gameObjectTotransfer in gameObjectsToTransfer)
            {
                    DontDestroyOnLoad(gameObjectTotransfer);
            }
            Runner.SetActiveScene(2);
            readyUp = true;
            
            Debug.Log(readyUp);
        }
        else{
            readyUp = false;
            Debug.Log(readyUp);
        }
        
    }

 
}
