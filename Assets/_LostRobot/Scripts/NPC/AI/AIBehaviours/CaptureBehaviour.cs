using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {

    public override void Init(GameObject gameObject) {
        base.Init(gameObject);
    }

    public override void Process() {
        if (Utils.CanSeeTransform(gameObject.transform, AIManager.player.transform) && !AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility()) {
            AIManager.player.GetComponent<PlayerController>().HandleCapture();
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
        //} else {
           // aiManager.SetBehaviour(aiManager.investigateBehaviour);
        }
    }

    public override void End() {
    }

}
