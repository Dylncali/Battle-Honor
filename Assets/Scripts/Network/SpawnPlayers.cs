using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.Diagnostics;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class SpawnPlayers : MonoBehaviour, INetworkRunnerCallbacks
{

    [SerializeField] NetworkPlayer playerPrefab;
    
 
    PlayerInputHandler playerInputHandler;




 
    public void OnConnectedToServer(NetworkRunner runner) 
    {
        Debug.Log("Connected To Server");
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
       
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("Disconnected from Server");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if(playerInputHandler == null && NetworkPlayer.Local != null)
        {
            playerInputHandler = NetworkPlayer.Local.GetComponent<PlayerInputHandler>();
        }
        if(playerInputHandler != null)
        {
            input.Set(playerInputHandler.GetNetworkInput());
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
       
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
            if(runner.IsServer)
            {
                if(SceneManager.GetActiveScene().buildIndex == 1){
                Debug.Log("This is Server. Spawing Player");
                runner.Spawn(playerPrefab, Utils.GetSpawnPoint(), Quaternion.identity, player).transform.Rotate(0,180,0);
                
                
                }
                else
                {
                 Debug.Log("This is Server. Spawing Player to new scene");
                runner.Spawn(playerPrefab, Utils.GetSpawnPoint(), Quaternion.identity, player);
                
                
                }

                
            }
            

        
        
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        StartCoroutine(waitToLoad());
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       Debug.Log("Server Shutdown");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    IEnumerator waitToLoad()
    {
        Debug.Log("Scene Loading");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Can you see me now?");
    }

    
}
