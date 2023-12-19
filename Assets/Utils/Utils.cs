using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    
    

   public static Vector3 GetSpawnPoint()
   {     
      if(SceneManager.GetActiveScene().buildIndex == 1){
         GameObject spawnPad = GameObject.FindGameObjectWithTag("SpawnPad");
         return spawnPad.transform.position;
      }
      else
        return new Vector3(-7.38f, 1.677f, 2.542f);
   }
   public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
        {
            if (trans.CompareTag("IgnoreLayerChange"))
                continue;

            trans.gameObject.layer = layerNumber;
        }
    }

   

}
