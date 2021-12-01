using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/PatrolBehaviour")]
public class PatrolBehaviour : AIBehaviour {

    public float maxVisionDistance = 15f;
    Rigidbody rbd;

    /*List<Vector3> path;
    float maxForce = 10;
    float maxSpeed = 30;
    int dir = 1;*/
    public override void Init(AIManager aiManager) {
        Reset();
        //rbd = gameObject.GetComponent<Rigidbody>();
        //for (int i = 0; i < aiManager.nodes.Count; i++)
         //   path.Add(aiManager.nodes[i].transform.position);
    }

    public override void Process(AIManager aiManager) {
        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform, 45, maxVisionDistance) && !AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility()) {
            aiManager.routeTiles = null;
            aiManager.nextTile = null;
            aiManager.SetBehaviour(aiManager.chaseBehaviour);
            return;
        }

        /*Vector3 desiredPos = aiManager.nodes[aiManager.currentNode].transform.position;
        float distance = Vector3.Distance(aiManager.gameObject.transform.position, desiredPos);
        if (distance <= 3)
            aiManager.currentNode++;
        if (currentNode >= path.Count || currentNode < 0)
        {
            //dir *= -1;
            //currentNode += dir;
            currentNode = 0;
        }
        
        if (Vector3.Distance(aiManager.gameObject.transform.position, desiredPos) < 3)
            return;
        */

        if (aiManager.routeTiles == null) {
            Vector3 desiredPos = aiManager.nodes[aiManager.currentNode].transform.position;
            float distance = Vector3.Distance(aiManager.gameObject.transform.position, desiredPos);
            if (distance <= 3)
                aiManager.currentNode = Random.Range(0, aiManager.nodes.Count);
            else
                aiManager.pathfinding.FindPath(aiManager.gameObject, desiredPos, distance > 20 ? true : false);
        }

        /*desiredPos = Vector3.ClampMagnitude(desiredPos, maxForce);
        desiredPos = desiredPos / rbd.mass;
        rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPos, maxSpeed);
        gameObject.transform.position = gameObject.transform.position + rbd.velocity;*/
    }

    public override void End(AIManager aiManager) {
        Reset();
    }

    private void Reset() {
        //currentNode = 0;
       // path.Clear();
        //dir = 1;
    }

}
