
using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{
    public KeyCardLogic main;
    public GameObject mesh;

    Color originalColor;
    Material material;
    public void Start()
    { 
        mesh.SetActive(false);
        originalColor = mesh.GetComponent<Renderer>().materials[0].GetColor("_EmissionColor");
        material = mesh.GetComponent<Renderer>().materials[0];
    }

    public void Update()
    {
        if (main.obtainedKey)
            return;

        float distance = Vector3.Distance(mesh.transform.position, AbilitiesManager.player.transform.position);
        distance = Mathf.Clamp(distance, 1f, 100f) * 50f;
        material.SetColor("_EmissionColor", originalColor * distance);
        mesh.SetActive(main.obtainedInfo);

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && !main.obtainedKey && main.obtainedInfo)
        {
            main.gotKey(true);
            GetComponent<AudioSource>().Play();
            FindObjectOfType<Notification>().SendNotification("Access Card Acquired");            
            main.checkKey();

            mesh.SetActive(false);
        }                
    }
}
