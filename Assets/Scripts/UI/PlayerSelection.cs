using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelection : MonoBehaviour
{
   


   public string characterSelection()
   {
    return GetComponent<TextMeshProUGUI>().text;

   }
}
