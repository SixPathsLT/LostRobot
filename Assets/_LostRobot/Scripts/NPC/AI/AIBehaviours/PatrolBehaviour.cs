using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/PatrolBehaviour")]
public class PatrolBehaviour : AIBehaviour {

    public float maxVisionDistance = 15f;
    Rigidbody rbd;
    int currentNode = 0;
    List<Vector3> path;
    float maxForce = 10;
    float maxSpeed = 30;
    int dir = 1;
    public override void Init(GameObject gameObject) {
        base.Init(gameObject);

        Reset();

        rbd = gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < aiManager.nodes.Count; i++)
            path.Add(aiManager.nodes[i].transform.position);
        
    }

    public override void Process() {
        if (Utils.CanSeeTransform(gameObject.transform, AIManager.player.transform, 45, maxVisionDistance) && !AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility())
        {
            aiManager.routeTiles = null;
            aiManager.SetBehaviour(aiManager.chaseBehaviour);
            return;
        }


        float distance = Vector3.Distance(gameObject.transform.position, path[currentNode]);
        if (distance <= 3)
            currentNode++;
        currentNode = Random.Range(0, path.Count);
        if (currentNode >= path.Count || currentNode < 0)
        {
            //dir *= -1;
            //currentNode += dir;
            currentNode = 0;
        }

        Vector3 desiredPos = path[currentNode];

        if (Vector3.Distance(gameObject.transform.position, desiredPos) < 3)
            return; 
       
        if (aiManager.routeTiles == null)
            aiManager.pathfinding.FindPath(gameObject, desiredPos);




        /*desiredPos = Vector3.ClampMagnitude(desiredPos, maxForce);
        desiredPos = desiredPos / rbd.mass;
        rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPos, maxSpeed);
        gameObject.transform.position = gameObject.transform.position + rbd.velocity;*/
    }

    public override void End() {
        Reset();
    }

    private void Reset() {
        currentNode = 0;
        path.Clear();
        dir = 1;
    }

}
