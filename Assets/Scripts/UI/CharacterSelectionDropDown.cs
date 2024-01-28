using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;
public class CharacterSelectionDropDown : NetworkBehaviour
{
    [SerializeField] TMP_Dropdown characterDropdownSelection; 
    //  public NetworkRunner networkRunnerPrefab;

    public TMP_InputField inputField;
    
    
    [HideInInspector]
    bool readyUp = false;

    void Start(){
        Debug.Log("Loading Game Manager");
        
        if(PlayerPrefs.HasKey("PlayerNickname"))
        {
            inputField.text = PlayerPrefs.GetString("PlayerNickname");
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
            
            PlayerPrefs.SetString("PlayerNickname", inputField.text);
            PlayerPrefs.Save();
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
