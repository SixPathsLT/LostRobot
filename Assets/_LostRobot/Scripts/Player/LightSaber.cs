
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    AIManager aiManager;
    CapsuleCollider weapon;

    bool inCombat;
    private void Start() {
        aiManager = transform.root.GetComponent<AIManager>();
        weapon = GetComponent<CapsuleCollider>();
    }

    void Update() {
        if (aiManager == null)
            return;

        inCombat = !aiManager.isStunned && (aiManager.combatBehaviour == aiManager.currentBehaviour || aiManager.chaseBehaviour == aiManager.currentBehaviour);
    }

    private void OnTriggerStay(Collider other) {
        if (inCombat && other.CompareTag("Player"))
            other.GetComponent<PlayerController>().data.SetHealth(other.GetComponent<PlayerController>().data.GetHealth() - 2);

    }


}
