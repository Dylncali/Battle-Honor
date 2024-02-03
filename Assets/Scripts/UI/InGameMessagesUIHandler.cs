using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameMessagesUIHandler : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProUGUIs;

    Queue messageQueue = new Queue();
    void Start()
    {
        
    }

    


    public void OngameMessgaeRecieved(string message)
    {
        Debug.Log($"InGameMessgaesUIHandler {message}");

        messageQueue.Enqueue(message);
        if(messageQueue.Count > 4)
            {
                messageQueue.Dequeue();
            }
        int queueIndex = 0;
        foreach (string messageInQueue in messageQueue)
        {
            textMeshProUGUIs[queueIndex].text = messageInQueue;
            queueIndex++;
        }
    }
}
