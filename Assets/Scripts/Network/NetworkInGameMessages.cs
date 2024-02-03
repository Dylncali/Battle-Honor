using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkInGameMessages : NetworkBehaviour
{
    public InGameMessagesUIHandler inGameMessagesUIHandler;
    void Start()
    {
        
    }

    public void SendInGameRPCMessage(string userNickName, string message)
    {
        RPC_InGameMessage($"<b> {userNickName}</b> {message}"); 
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
     void RPC_InGameMessage(string messgae, RpcInfo info = default)
    {


        if(inGameMessagesUIHandler != null){
            inGameMessagesUIHandler.OngameMessgaeRecieved(messgae);
        }
    }

    
}
