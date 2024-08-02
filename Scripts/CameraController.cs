using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- This script manages limiting the camera movement and user input.
    /// </summary>
    [SerializeField] private float speed = 10;
    [SerializeField] private float zoomSpeed = 5;
    [SerializeField] private GameObject hexes;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    // C.Love - this method is called once per frame and applies user input to camera movement
    void Update()
    {
        float horz = Input.GetAxis("Horizontal")*Time.deltaTime*speed;
        float vert = Input.GetAxis("Vertical")*Time.deltaTime*speed;
        float zoom = Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime*zoomSpeed;
        transform.Translate(horz, vert, zoom);
    }
    // C.Love - this method is called every frame but after Update() in order to avoid lag, it limits camera position so the hexes are within frame
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;

        // Clamp each component of the position individually
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Apply the clamped position to the transform
        transform.position = newPosition;
    }
}
