
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/InvestigateBehaviour")]
public class InvestigateBehaviour : AIBehaviour {

    GameObject investigatingObject;
    float timeElapsed = 0f;

   List<GameObject> investigatedObjects = new List<GameObject>();

    public override void Init(AIManager aiManager) {

        Reset();
    }

    public override void Process(AIManager aiManager) {

        if (AIManager.player.GetComponent<AbilitiesManager>().UsingCloakingAbility()) {
            aiManager.SetBehaviour(aiManager.patrolBehaviour);
            return;
        }

        if (investigatingObject == null) {      
            investigatingObject = GetUnCheckedObject(aiManager);
            ChangeColor(aiManager, Color.yellow);
        } else {
            Rigidbody rigidbody = aiManager.gameObject.GetComponent<Rigidbody>();

            float distance = Vector3.Distance(aiManager.gameObject.transform.position, investigatingObject.transform.position);
            bool isPlayer = investigatingObject.CompareTag("Player");

            if (rigidbody != null && distance > 2) {
                Vector3 direction = investigatingObject.transform.position - aiManager.gameObject.transform.position;

                aiManager.gameObject.transform.rotation = Quaternion.Slerp(aiManager.gameObject.transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
                aiManager.gameObject.transform.position = Vector3.MoveTowards(aiManager.gameObject.transform.position, investigatingObject.transform.position, 2f * Time.deltaTime);
            } else {
                ChangeColor(aiManager, Color.cyan);

                if (isPlayer)
                    ChangeColor(aiManager, Color.red);
                
                if (timeElapsed > Random.Range(1, 3)) {
                    if (investigatingObject.CompareTag("Player"))
                        aiManager.SetBehaviour(aiManager.captureBehaviour);
                    else
                        ChangeColor(aiManager, Color.green);

                    investigatingObject = null;
                    timeElapsed = 0;
                }

                timeElapsed += Time.deltaTime;
            }
        }
    }

    void ChangeColor(AIManager aiManager, Color color)
    {
        aiManager.gameObject.GetComponent<MeshRenderer>().material.color = color;
        aiManager.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
    }

    GameObject GetUnCheckedObject(AIManager aiManager) {
        Collider[] colliders = Physics.OverlapSphere(aiManager.gameObject.transform.position, 15f);

        GameObject obj;
        foreach (var collider in colliders) {


            obj = collider.gameObject;
            if (obj.transform.root != null && obj.tag != "Player")
                obj = obj.transform.root.gameObject;

            if (obj.GetComponent<Collider>() == null || obj.GetComponent<MeshCollider>() != null || obj.GetComponent<Collider>().isTrigger == true)
                continue;
           
            if (obj == aiManager.gameObject)
                continue;

            if (!investigatedObjects.Contains(obj)) {
                investigatedObjects.Add(obj);
                return obj;
            }
        }

        investigatedObjects.Clear();

        return null;
    }

    public override void End(AIManager aiManager) {
        Reset();
    }

    void Reset()
    {
        investigatedObjects.Clear();
        investigatingObject = null;
        timeElapsed = 0f;
    }

}
