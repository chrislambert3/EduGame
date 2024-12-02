using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Header("Follow Settings")]
    [SerializeField] private float FollowSpeed = 2f;

    [Header("Camera Boundaries")]
    [SerializeField] private float minX = -7.12f; // Left boundary
    [SerializeField] private float maxX = 7.12f; // Right boundary

    void Update()
    {
        // Clamp the target's x position to keep the camera within the defined boundaries
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);

        // New position for the camera
        Vector3 newPos = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Smoothly move the camera
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
