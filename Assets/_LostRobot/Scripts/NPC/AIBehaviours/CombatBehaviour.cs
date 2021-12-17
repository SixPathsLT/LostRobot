using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CombatBehaviour")]
public class CombatBehaviour : AIBehaviour {

    AbilitiesManager abilitiesManager;
    public override void Init(AIManager aiManager) {
        AIManager.player.GetComponent<PlayerMovement>().inCombat = true;
        if (abilitiesManager == null)
            abilitiesManager = AIManager.player.GetComponent<AbilitiesManager>();
    }

    public override void Process(AIManager aiManager) {
        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform) && abilitiesManager.UsingCloakingAbility()) {
            aiManager.routeTiles = null;
            aiManager.nextTile = null;
            aiManager.SetBehaviour(aiManager.investigateBehaviour);
            return;
        }
        float distance = Vector3.Distance(AIManager.player.transform.position, aiManager.gameObject.transform.position);
        if (distance > 3) {
            aiManager.SetBehaviour(aiManager.chaseBehaviour);
            return;
        }
        if (GameManager.GetInstance().InPuzzleState() || GameManager.GetInstance().InEmailState()) {
            aiManager.SetBehaviour(aiManager.captureBehaviour);
            return;
        }

        if (GameManager.GetInstance().data.GetHealth() < 1) {
            aiManager.transform.position = aiManager.nodes[Random.Range(0, aiManager.nodes.Length)].position;
            AIManager.player.GetComponent<PlayerController>().HandleDeath();
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
            return;
        }

        Quaternion rotation = aiManager.gameObject.transform.rotation;
        Vector3 lookDirection = (AIManager.player.transform.position - aiManager.gameObject.transform.position).normalized;
        if (lookDirection != Vector3.zero)
            rotation = Quaternion.LookRotation(lookDirection);

        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform, 360f))
            aiManager.gameObject.transform.rotation = Quaternion.Slerp(aiManager.gameObject.transform.rotation, rotation, 4f * Time.deltaTime);

        if (aiManager.lastPunch > 0)
            return;

        aiManager.lastPunch = 1f;
        aiManager._anim.SetTrigger("AttackTrigger");
        aiManager._anim.SetInteger("Attack_Index", Random.Range(0, aiManager._anim.GetInteger("Attack_Max_Index")));

    }
   
  
    public override void End(AIManager aiManager) {

    }



}
