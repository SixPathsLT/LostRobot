
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {
    
    public override void Init(AIManager aiManager) {
    }

    public override void Process(AIManager aiManager) {
        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform)
            && !AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility()) {
            AIManager.player.GetComponent<PlayerController>().HandleCapture();
            aiManager.transform.position = aiManager.nodes[Random.Range(0, aiManager.nodes.Count)].transform.position;
        }

        aiManager.SetBehaviour(aiManager.patrolBehaviour);
    }

    public override void End(AIManager aiManager) {
    }

}