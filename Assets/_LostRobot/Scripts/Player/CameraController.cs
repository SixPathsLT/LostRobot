using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform focus;
    public float yOffset;
    public float zOffset = 8f;
    public float mouseSensitivity = 3f;
    public Camera cam;

    private RaycastHit hit;
    float camDist;
    public float scrollSensitivity = 2f;
    public float scrollDamp = 6f;
    public Vector2 zoomMinMax = new Vector2(2f, 15f);
    float zoomDist;
    public float collisionSensitivity = 2.5f;
    void Start()
    {
        camDist = cam.transform.localPosition.z;
        zoomDist = zOffset;
        camDist = zoomDist;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Movement();
        Zoom();
        CameraCollision();
    }

    private void Movement()
    {
        focus.transform.position = new Vector3(player.transform.position.x, 
                                               player.transform.position.y + yOffset, player.transform.position.z);
        var rotation = focus.transform.rotation;
        float mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity / 2;
        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotation = Quaternion.Euler(rotation.eulerAngles.x - mouseX,
                                    rotation.eulerAngles.y + mouseY, rotation.eulerAngles.z);
        focus.transform.rotation = rotation;
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            scrollAmount *= zoomDist * 0.3f;
            zoomDist -= scrollAmount;
            zoomDist = Mathf.Clamp(zoomDist, zoomMinMax.x, zoomMinMax.y);
        }
        if (camDist != zoomDist * -1f)
        {
            camDist = Mathf.Lerp(camDist, -zoomDist, Time.deltaTime * scrollDamp);
        }
    }

    private void CameraCollision()
    {
        cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, camDist);
        if (Physics.Linecast(focus.transform.position, cam.transform.position, out hit))
        {
            cam.transform.position = hit.point;
            var desiredCamPos = cam.transform.localPosition;
            desiredCamPos = new Vector3(desiredCamPos.x, desiredCamPos.y, desiredCamPos.z + collisionSensitivity);
            cam.transform.localPosition = desiredCamPos;
        }
        if (cam.transform.localPosition.z > -1f)
        {
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
        }
    }
}
