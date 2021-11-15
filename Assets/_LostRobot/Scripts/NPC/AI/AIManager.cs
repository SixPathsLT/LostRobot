using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public bool isStunned;

    public static GameObject player;
    public AIBehaviour investigateBehaviour, patrolBehaviour, chaseBehaviour, combatBehaviour, captureBehaviour;

    AIBehaviour currentBehaviour;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SetBehaviour(investigateBehaviour);
    }

    public void SetBehaviour(AIBehaviour aiBehaviour) {
        if (currentBehaviour != null)
            currentBehaviour.End();

        currentBehaviour = aiBehaviour;

        if (currentBehaviour != null)
            currentBehaviour.Init(gameObject);
    }

    void Update() {
        if (currentBehaviour != null && !isStunned)
            currentBehaviour.Process();

    }

    public IEnumerator Stun(float duration) {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

}
