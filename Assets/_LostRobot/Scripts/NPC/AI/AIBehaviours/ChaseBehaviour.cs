using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/ChaseBehaviour")]
public class ChaseBehaviour : AIBehaviour {

    float maxForce = 10;
    float maxSpeed = 50;
    float frames = 5;
    Rigidbody rbd;
    public override void Init(GameObject gameObject) {
        base.Init(gameObject);
        rbd = gameObject.GetComponent<Rigidbody>();
    }

    public override void Process() {
        Transform player = AIManager.player.transform;
        float range = Vector3.Distance(player.position, gameObject.transform.position);
        if (range > 50)
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
        else if (range <= 1)
            aiManager.SetBehaviour(aiManager.captureBehaviour);
        else
        {
            frames = range / maxSpeed;
            Vector3 desiredPosition = player.position + player.GetComponent<Rigidbody>().velocity * frames;
            desiredPosition = Vector3.ClampMagnitude(desiredPosition, maxForce);
            desiredPosition = desiredPosition / rbd.mass;
            rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPosition, maxSpeed);
            gameObject.transform.position = gameObject.transform.position + rbd.velocity;
        }
    }

    public override void End() {
       
    }

}
