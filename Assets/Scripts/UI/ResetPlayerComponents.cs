using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ResetPlayerComponents : NetworkBehaviour
{
    
    void Start()
    {
        StartCoroutine(WaitForPlayertoSpawn());
    }

   IEnumerator WaitForPlayertoSpawn()
   {
    GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
    Debug.Log(playerObjects.Length);
     while (playerObjects.Length == 0)
     {
        int num =0;
        Debug.Log(num);
        num ++;
        
        yield return null;
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
     }
     foreach(GameObject playerObject in playerObjects)
     {      
                NetworkPlayer networkPlayer = playerObject.GetComponent<NetworkPlayer>();
                
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
            if (networkPlayer != null)
            {
                Camera camera = networkPlayer.localCamera;
                camera.gameObject.SetActive(true);
                networkPlayer.LocalInputAuthority();
            }
            else Debug.Log("Camera not found");
            
                
     }
    
   }
}
