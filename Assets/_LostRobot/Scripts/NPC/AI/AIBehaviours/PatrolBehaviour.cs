using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/PatrolBehaviour")]
public class PatrolBehaviour : AIBehaviour {

    Rigidbody rbd;
    int currentNode = 0;
    List<Vector3> path;
    float maxForce = 10;
    float maxSpeed = 30;
    int dir = 1;
    public override void Init(GameObject gameObject) {
        base.Init(gameObject);
        rbd = gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < aiManager.nodes.Count; i++)
            path.Add(aiManager.nodes[i].transform.position);
    }

    public override void Process() {
        float distance = Vector3.Distance(gameObject.transform.position, path[currentNode]);
        if (distance <= 3)
            currentNode++;
        if (currentNode >= path.Count || currentNode < 0)
        {
            dir *= -1;
            currentNode += dir;
        }
        Vector3 desiredPos = path[currentNode];
        desiredPos = Vector3.ClampMagnitude(desiredPos, maxForce);
        desiredPos = desiredPos / rbd.mass;
        rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPos, maxSpeed);
        gameObject.transform.position = gameObject.transform.position + rbd.velocity;
    }

    public override void End() {

    }

}
