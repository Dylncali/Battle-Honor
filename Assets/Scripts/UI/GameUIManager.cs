using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;

public class GameUIManager : NetworkBehaviour
{
    [SerializeField] GameObject playerUI; 
    [SerializeField] Camera firstUICamera; 

    [SerializeField] Camera MainCamera; 

    [SerializeField] GameObject playerUIScripts; 




    void Awake()
    {
        MainCamera.gameObject.SetActive(false);
        playerUI.SetActive(false);
        playerUIScripts.SetActive(false);
        var LoadingTask = WaitForplayer();
        
       
    }

    
  private async Task WaitForplayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (player == null){
            
            await Task.Delay(100);
            Debug.Log("Player Has NOT been Found");
            player = GameObject.FindGameObjectWithTag("Player");
            

        }
        Debug.Log("Player Finally Found");
        playerUI.SetActive(true);
        playerUIScripts.SetActive(true);
        MainCamera.gameObject.SetActive(true);
        firstUICamera.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        
    }

}
