using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
   public static NetworkPlayer Local {get; set;}
   [SerializeField] Camera RemoteCamera;

   public Camera localCamera;
   Camera MainMenuCamera;
    int num = 0;

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex ==1 )
        {
            if(num == 0){
                localCamera.gameObject.SetActive(false);
                Debug.Log("Disabling Camera");
                num++;
            }
            
        }
        else {
            if(num == 1){
            localCamera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Reabling Camera");
            LocalInputAuthority();
            num++;
            }
            else return;
        }
    }

    public override void Spawned()
    {

        LocalInputAuthority();
        
    }


    public void PlayerLeft(PlayerRef player)
    {
        if(player ==Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

    public void LocalInputAuthority()
    {
        if(Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local Player");

        }
        else 
        {
            
            RemoteCamera.enabled = false;
            Debug.Log("Spawned remote Player");
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
        }
    }
}
