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
        //camDist = cam.transform.localPosition.z;
        zoomDist = zOffset;
        camDist = zoomDist;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        if (CutsceneManager.GetActiveCutscene() == null)
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
      
        float xRot = rotation.eulerAngles.x - mouseX;
        xRot = Mathf.Clamp(xRot, 2, 40);
        rotation = Quaternion.Euler(xRot,
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

    void ResetZoom() {
        float maxDist = Mathf.Abs(camDist);

        Vector3 checkVector = (cam.transform.position - focus.transform.position).normalized * maxDist;
        checkVector.y = checkVector.y - 1;

        if (Physics.Raycast(cam.transform.position, checkVector, out hit, maxDist)) {
            float z = focus.transform.InverseTransformPoint(hit.point).z / 1.3f;
            z = Mathf.Clamp(z, camDist, -1f);
            Vector3 resetToVector = new Vector3(cam.transform.localPosition.x, 1.2f, z);

            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, resetToVector, 10f * Time.deltaTime);
        } else {
            checkVector = (cam.transform.position - focus.transform.position).normalized * maxDist;

            Vector3 resetToVector = new Vector3(cam.transform.localPosition.x, 1.2f, camDist);
            if (Physics.Raycast(cam.transform.position, checkVector, out hit, maxDist)) {
                float z = focus.transform.InverseTransformPoint(hit.point).z / 1.3f;
                z = Mathf.Clamp(z, camDist, -1f);
                resetToVector.z = z;
            }
            
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, resetToVector, 3f * Time.deltaTime);
        }
    }
    
    private void CameraCollision() {

       if (cam.transform.localPosition.y < 1f)
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, 1f, cam.transform.localPosition.z), 50f * Time.deltaTime);

        // cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, 1f, camDist), 5f * Time.deltaTime);

        if (cam.transform.localPosition.z != -1 && Physics.Linecast(focus.transform.position, cam.transform.position, out hit)) {
            if (hit.collider.gameObject.CompareTag("Player"))
                return;

            Vector3 hitVector = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            //cam.transform.position = hitVector;
           // cam.transform.position = Vector3.MoveTowards(cam.transform.position, hitVector, 10f * Time.deltaTime);

            var desiredCamPos = cam.transform.localPosition;
          //  desiredCamPos = new Vector3(desiredCamPos.x, desiredCamPos.y, desiredCamPos.z + collisionSensitivity);
            float z = focus.transform.InverseTransformPoint(hit.point).z / 1.3f;
            z = Mathf.Clamp(z, camDist, -1f);
            desiredCamPos = new Vector3(desiredCamPos.x, desiredCamPos.y, z);

            //cam.transform.localPosition = desiredCamPos;

            float dist = Vector3.Distance(hit.point, cam.transform.position) * 7f;
            if (hit.collider.name.Contains("Wall"))
                dist *= dist * 10f;

           cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, desiredCamPos, dist * Time.deltaTime);
        } else if (!Physics.CheckSphere(cam.transform.position, 0.5f))
            ResetZoom();
        
        if (cam.transform.localPosition.z > -1f)
           // cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x, 1f, -1f), 10f * Time.deltaTime);


    }
}
