using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;

public class ConnectToServer : MonoBehaviour
{   
    public NetworkRunner networkRunnerPrefab;
    NetworkRunner networkRunner;

     private void Awake() 
    {
        NetworkRunner networkRunnerIsInScene = FindAnyObjectByType<NetworkRunner>();

        if(networkRunnerIsInScene != null)
        {
            networkRunner = networkRunnerIsInScene;
        }
    }
    
    void Start()
    {
        
       if(networkRunner == null)
       {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner";
        if(SceneManager.GetActiveScene().buildIndex != 1){
            var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),1,null);
            
        }
        Debug.Log("Server Network Started");
       }
    
    }

    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();


        if(sceneManager == null)
        {
            //Handles networked objects in scene that already exist
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = GetSceneManager(runner);

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs{
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            // SessionName = "TestRoom",
            Initialized = initialized,
            SceneManager = sceneManager

        });
    }

    


    public void isReadyupClicked()
    {
       
        
        Debug.Log("Loading next level");
        
    }

}