using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float speed = 0.2f;

    private void LateUpdate()
    {
        Vector3 position = player.position + offset;
        Vector3 finalPosition = Vector3.Lerp(transform.position, position, speed);
        transform.position = finalPosition;
        transform.LookAt(player);
    }
}
