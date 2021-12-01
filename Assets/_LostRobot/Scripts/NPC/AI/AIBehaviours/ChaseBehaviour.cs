using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/ChaseBehaviour")]
public class ChaseBehaviour : AIBehaviour {

    float maxForce = 10;
    float maxSpeed = 50;
    float frames = 5;
    Rigidbody rbd;
    public override void Init(AIManager aiManager) {
        // rbd = gameObject.GetComponent<Rigidbody>();
    }

    public override void Process(AIManager aiManager) {

        Transform player = AIManager.player.transform;

        if (player.GetComponent<AbilitiesManager>().UsingCloakingAbility()) {
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
            return;
        }

        float range = Vector3.Distance(player.position, aiManager.gameObject.transform.position);

        Quaternion rotation = aiManager.gameObject.transform.rotation;
        Vector3 lookDirection = (player.transform.position - aiManager.gameObject.transform.position).normalized;
        if (lookDirection != Vector3.zero)
            rotation = Quaternion.LookRotation(lookDirection);

        if (Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform, 360f))
            aiManager.gameObject.transform.rotation = Quaternion.Slerp(aiManager.gameObject.transform.rotation, rotation, 4f * Time.deltaTime);

        if (range > 50)
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
        else if (Utils.CanSeeTransform(aiManager.gameObject.transform, player) && range < 3.5f)
            aiManager.SetBehaviour(aiManager.captureBehaviour);
        else {
            if (aiManager.routeTiles == null 
                || (!Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform) && range < 20f && Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform, 360f)))
                //|| (Utils.CanSeeTransform(gameObject.transform, player.transform, 45f, 10) && aiManager.nextTile != null && Vector3.Distance(aiManager.nextTile.position, player.transform.position) > range))
                aiManager.pathfinding.FindPath(aiManager.gameObject, player.transform.position);

            /*frames = range / maxSpeed;
            Vector3 desiredPosition = player.position + player.GetComponent<Rigidbody>().velocity * frames;
            desiredPosition = Vector3.ClampMagnitude(desiredPosition, maxForce);
            desiredPosition = desiredPosition / rbd.mass;
            rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPosition, maxSpeed);
            gameObject.transform.position = gameObject.transform.position + rbd.velocity;*/
        }
    }

    public override void End(AIManager aiManager) {
       
    }

}
