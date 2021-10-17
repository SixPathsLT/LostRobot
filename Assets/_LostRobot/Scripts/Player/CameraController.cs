using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float moveSpeed = 0.2f;
    public Transform focus;

    public float mouseSensitivity = 3f;
    public float zoomLevel = 1f;

    //collision
    private RaycastHit hit;
    float camDistance;
    Vector3 cameraDirection;
    public Vector2 camDistMinMax = new Vector2(0.5f, 5f);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraDirection = focus.transform.localPosition.normalized;
        camDistance = camDistMinMax.y;
    }

    private void Update()
    {
        SmoothMovement();
        Rotation();
        CameraCollision();
    }

    private void SmoothMovement()
    {
        Vector3 position = player.position + offset;
        Vector3 finalPosition = Vector3.Lerp(focus.transform.position, position, moveSpeed);
        focus.transform.position = finalPosition;
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity/2;
        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity;
        var rotation = Quaternion.Euler(focus.transform.rotation.eulerAngles.x - mouseX, 
                                        focus.transform.rotation.eulerAngles.y + mouseY, focus.transform.rotation.eulerAngles.z);
        focus.transform.rotation = rotation;
        transform.LookAt(player);
    }

    void CameraCollision()
    {
        Vector3 desiredCameraPosition = transform.TransformPoint(cameraDirection * camDistMinMax.y);
        if (Physics.Linecast(focus.transform.position, desiredCameraPosition, out hit))
        {
            camDistance = Mathf.Clamp(hit.distance, camDistMinMax.x, camDistMinMax.y);
        }
        else
        {
            camDistance = camDistMinMax.y;
        }
        focus.localPosition = cameraDirection * camDistance;
    }

}
