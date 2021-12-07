
using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{
    public KeyCardLogic main;
    public GameObject mesh;


    public void Update()
    {
        if (main.obtainedKey)
            return;

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
