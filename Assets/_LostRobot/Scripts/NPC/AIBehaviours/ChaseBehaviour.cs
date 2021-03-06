using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/ChaseBehaviour")]
public class ChaseBehaviour : AIBehaviour {

    public int chaseDistance = 50;

    /* float maxForce = 10;
     float maxSpeed = 50;
     float frames = 5;
     Rigidbody rbd;*/

    AbilitiesManager abilitiesManager;
    PlayerData data;
    public override void Init(AIManager aiManager) {
        // rbd = gameObject.GetComponent<Rigidbody>();
        aiManager._anim.SetBool("Run", true);
        aiManager.GetComponentInChildren<EnemyAudioControl>().Found();

        if (abilitiesManager == null)
            abilitiesManager = AIManager.player.GetComponent<AbilitiesManager>();

        if (data == null)
            data = AIManager.player.GetComponent<PlayerController>().data;
    }

    public override void Process(AIManager aiManager) {

        Transform player = AIManager.player.transform;

        if (abilitiesManager.UsingCloakingAbility()) {
            aiManager.SetBehaviour(aiManager.investigateBehaviour);
            return;
        }

        float range = Vector3.Distance(player.position, aiManager.gameObject.transform.position);

        Quaternion rotation = aiManager.gameObject.transform.rotation;
        Vector3 lookDirection = (player.transform.position - aiManager.gameObject.transform.position).normalized;
        if (lookDirection != Vector3.zero)
            rotation = Quaternion.LookRotation(lookDirection);

        if (Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform, 360f))
            aiManager.gameObject.transform.rotation = Quaternion.Slerp(aiManager.gameObject.transform.rotation, rotation, 4f * Time.deltaTime);
        bool aggressive = data.GetConcioussness() > 0 && data.GetEmailsCount() >= 12 ? true : false;
        if (range > chaseDistance)
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
        else if (Utils.CanSeeTransform(aiManager.gameObject.transform, player) && range < (aggressive ? 3f : 4f))
            aiManager.SetBehaviour(aggressive ? aiManager.combatBehaviour : aiManager.captureBehaviour);
        else {
            if (aiManager.routeTiles == null )
                //|| (!Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform) && range < 20f && Utils.CanSeeTransform(aiManager.gameObject.transform, player.transform, 360f)))
                //|| (Utils.CanSeeTransform(gameObject.transform, player.transform, 45f, 10) && aiManager.nextTile != null && Vector3.Distance(aiManager.nextTile.position, player.transform.position) > range))
                aiManager.pathfinding.FindPath(aiManager.gameObject, player.transform.position, true);

            /*frames = range / maxSpeed;
            Vector3 desiredPosition = player.position + player.GetComponent<Rigidbody>().velocity * frames;
            desiredPosition = Vector3.ClampMagnitude(desiredPosition, maxForce);
            desiredPosition = desiredPosition / rbd.mass;
            rbd.velocity = Vector3.ClampMagnitude(rbd.velocity + desiredPosition, maxSpeed);
            gameObject.transform.position = gameObject.transform.position + rbd.velocity;*/
        }
    }

    public override void End(AIManager aiManager) {
        aiManager._anim.SetBool("Run", false);
    }

}
