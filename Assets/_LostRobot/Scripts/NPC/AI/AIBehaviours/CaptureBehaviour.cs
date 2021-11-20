using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {

    public override void Init(GameObject gameObject) {
        base.Init(gameObject);
    }

    public override void Process() {
        AIManager.player.GetComponent<PlayerController>().HandleCapture();
        aiManager.SetBehaviour(aiManager.patrolBehaviour);
    }

    public override void End() {
    }

}
