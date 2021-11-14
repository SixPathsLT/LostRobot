using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {

    public override void Init(GameObject gameObject) {
        base.Init(gameObject);

    }

    public override void Process() {
        AIManager.player.transform.position = new Vector3(-30, 1, 21);
        AIManager.player.GetComponent<PlayerController>().data.SetConciousness(0);
        aiManager.SetBehaviour(aiManager.investigateBehaviour);
    }

    public override void End() {
    }

}
