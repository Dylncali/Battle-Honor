using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 10f;
    public float distanceFromPlayer = 5f;
    public float cameraHeight = 2f;
    public float zoomSpeed = 2f; // Speed of zooming in and out
    public float minZoom = 2f; // Minimum zoom distance
    public float maxZoom = 10f; // Maximum zoom distance

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Get the scroll wheel input and modify the distance from the player
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distanceFromPlayer = Mathf.Clamp(distanceFromPlayer - scroll * zoomSpeed, minZoom, maxZoom);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);

        Vector3 offset = new Vector3(0, cameraHeight, -distanceFromPlayer);
        transform.position = player.position + player.TransformDirection(offset);
    }
}

