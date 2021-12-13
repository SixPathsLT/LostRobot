
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public AIBehaviour combat;
    public AIManager aiManager;

    private void Start()
    {
        aiManager = transform.root.GetComponent<AIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && combat == aiManager.currentBehaviour) 
        {
            other.GetComponent<PlayerController>().data.SetHealth(0);
            Debug.Log("Damage player");
        }
    }

}
