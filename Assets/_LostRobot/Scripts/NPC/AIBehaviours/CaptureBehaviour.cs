
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CaptureBehaviour")]
public class CaptureBehaviour : AIBehaviour {

    AbilitiesManager abilitiesManager;
    public override void Init(AIManager aiManager) {
        if (abilitiesManager == null)
            abilitiesManager = AIManager.player.GetComponent<AbilitiesManager>();
    }

    public override void Process(AIManager aiManager) {
        if (Utils.CanSeeTransform(aiManager.gameObject.transform, AIManager.player.transform)
            && !abilitiesManager.UsingCloakingAbility()) {
            aiManager._anim.SetTrigger("Capture");
            FindObjectOfType<CutsceneManager>().PlayCutscene(CutsceneManager.CAPTURED_CUTSCENE);
            
            Vector3 lookDirection = (aiManager.transform.position - AIManager.player.transform.position).normalized;
            if (lookDirection != Vector3.zero)
                AIManager.player.transform.rotation = Quaternion.LookRotation(lookDirection);
           

            //AIManager.player.GetComponent<PlayerController>().HandleCapture();
            //aiManager.transform.position = aiManager.nodes[Random.Range(0, aiManager.nodes.Count)].transform.position;
            return;
        }

        aiManager.SetBehaviour(aiManager.patrolBehaviour);
    }

    public override void End(AIManager aiManager) {
        aiManager.transform.position = aiManager.nodes[Random.Range(0, aiManager.nodes.Count)].transform.position;
    }

}
