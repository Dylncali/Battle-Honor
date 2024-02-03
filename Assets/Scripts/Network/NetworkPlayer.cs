using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;


public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
   public static NetworkPlayer Local {get; set;}
   [SerializeField] Camera RemoteCamera;

   public TextMeshProUGUI playerNickName;

   [Networked(OnChanged = nameof(OnNickNameChanged))]
   public NetworkString<_16> nickName {get; set; }

   bool isPublicJoinMessageSent = false;

   NetworkInGameMessages networkInGameMessages;

   public Camera localCamera;
   Camera MainMenuCamera;
    int num = 0;

    private void Awake() {
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
    }

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
            transform.position = Utils.GetSpawnPoint();
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
        if(Object.HasStateAuthority)
        {
            networkInGameMessages.SendInGameRPCMessage(nickName.ToString(), "left");
        }

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
            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));

        }
        else 
        {
            
            RemoteCamera.enabled = false;
            Debug.Log("Spawned remote Player");
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed){
        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nickname changed from {gameObject.name} to {nickName}");

        playerNickName.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;

        if(!isPublicJoinMessageSent)
        {
            networkInGameMessages.SendInGameRPCMessage(nickName, "joined");
            isPublicJoinMessageSent = true;
        }
    }
}
