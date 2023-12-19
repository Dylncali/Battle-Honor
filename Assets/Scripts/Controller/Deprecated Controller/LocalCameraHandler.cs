using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    Camera localCamera;
    public Transform cameraAnchorPoint;

    Vector2 cameraInput;

    float cameraRotationx = 0f;
    float cameraRotationy = 0f;

    PlayerMovement playerMovement;

     public void SetCameraInputVector(Vector2 cameraInput)
    {
        this.cameraInput = cameraInput;
    }


    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        localCamera = GetComponent<Camera>();
    }
    void Start()
    {
        if(localCamera.enabled)
        {
            localCamera.transform.parent = null;

    
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraAnchorPoint == null)
        {
            return;
        }
        if(!localCamera.enabled)
        {
            return;
        }

        localCamera.transform.position = cameraAnchorPoint.position;
        cameraRotationx += cameraInput.y * Time.deltaTime * playerMovement.rotationSpeedY;
        cameraRotationx = Mathf.Clamp(cameraRotationx, -90,90);

        cameraRotationy += cameraInput.x * Time.deltaTime * playerMovement.rotationSpeed;

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationx,cameraRotationy,0);


        Vector3 start = transform.position;

        // End point is some distance in the forward direction
        Vector3 end = start + transform.forward * 10f; // Extend 10 units forward

        // Draw the line
        Debug.DrawLine(start, end, Color.red); 
    }

}
