
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/InvestigateBehaviour")]
public class InvestigateBehaviour : AIBehaviour {
    
    float timeElapsed = 0f;

    public int chanceOfDetection = 0;

    AbilitiesManager abilitiesManager;
    public override void Init(AIManager aiManager) {
        Reset();
        if (abilitiesManager == null)
            abilitiesManager = AIManager.player.GetComponent<AbilitiesManager>();

    }

    public override void Process(AIManager aiManager) {
        float distance = Vector3.Distance(aiManager.gameObject.transform.position, AIManager.player.transform.position);
        if (distance > 4) {
            if (timeElapsed > 0 && Utils.CanSeeTransform(aiManager.transform, AIManager.player.transform)) {
                aiManager.SetBehaviour(aiManager.captureBehaviour);
                return;
            }
            if (aiManager.routeTiles == null)
                aiManager.pathfinding.FindPath(aiManager.gameObject, AIManager.player.transform.position, Random.Range(0, 3) == 1);

            ChangeColor(aiManager, Color.yellow);
        } else {
            bool isHidden = abilitiesManager.UsingCloakingAbility();

            if (!isHidden && Utils.CanSeeTransform(aiManager.transform, AIManager.player.transform)) {
                aiManager.SetBehaviour(aiManager.chaseBehaviour);
                ChangeColor(aiManager, Color.red);
                timeElapsed = 0;
                return;
            }
            
            if (aiManager.routeTiles != null) { 
                aiManager.routeTiles = null;
                aiManager.nextTile = null;
                aiManager.invIndex = Random.Range(0, aiManager._anim.GetInteger("Inv_Max"));
                aiManager._anim.SetInteger("Inv_Index", aiManager.invIndex);
                aiManager._anim.SetTrigger("Investigate");
            }
            
            float seconds = aiManager.invIndex == 0 ? 6.5f : aiManager.invIndex == 1 ? 4.5f : 8.5f;     //Random.Range(3, 5);
            //aiManager.gameObject.transform.rotation = Quaternion.Slerp(aiManager.gameObject.transform.rotation, Quaternion.LookRotation(aiManager.gameObject.transform.right), (seconds / 2) * Time.deltaTime);
            
            if (timeElapsed > seconds) {
                ChangeColor(aiManager, Color.cyan);
                
                if ((isHidden && Random.Range(0, 101 - chanceOfDetection) == 0))
                    AIManager.player.GetComponent<PlayerController>().HandleCapture();
               
                aiManager.SetBehaviour(aiManager.patrolBehaviour);
                ChangeColor(aiManager, Color.red);
                timeElapsed = 0;
                return;
            }
            timeElapsed += Time.deltaTime;
        }
    }

    void ChangeColor(AIManager aiManager, Color color) {
        aiManager.gameObject.GetComponent<MeshRenderer>().material.color = color;
        aiManager.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
    }

    public override void End(AIManager aiManager) {
        Reset();
    }

    void Reset() {
        timeElapsed = 0f;
    }

}
