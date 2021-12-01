using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {

    public override void Init(AIManager aiManager) {
    }

    public override void Process(AIManager aiManager) {
        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform)
            && !AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility())
                AIManager.player.GetComponent<PlayerController>().HandleCapture();
        

        aiManager.SetBehaviour(aiManager.patrolBehaviour);
    }

    public override void End(AIManager aiManager) {
    }

}
