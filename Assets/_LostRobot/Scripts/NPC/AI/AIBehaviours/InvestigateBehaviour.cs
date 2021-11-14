
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/InvestigateBehaviour")]
public class InvestigateBehaviour : AIBehaviour {

    GameObject investigatingObject;
    float timeElapsed = 0f;

   List<GameObject> investigatedObjects = new List<GameObject>();

    public override void Init(GameObject gameObject) {
        base.Init(gameObject);

        Reset();
    }

    public override void Process() {
        if (investigatingObject == null) {      
            investigatingObject = GetUnCheckedObject();
            ChangeColor(Color.yellow);
        } else {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            float distance = Vector3.Distance(gameObject.transform.position, investigatingObject.transform.position);

            if (rigidbody != null && distance > 2) {
                Vector3 direction = investigatingObject.transform.position - gameObject.transform.position;

                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, investigatingObject.transform.position, 2f * Time.deltaTime);
            } else {
                ChangeColor(Color.cyan);

                if (investigatingObject.CompareTag("Player"))
                    ChangeColor(Color.red);
                
                if (timeElapsed > Random.Range(5, 10)) {
                    if (investigatingObject.CompareTag("Player"))
                        aiManager.SetBehaviour(aiManager.captureBehaviour);
                    else
                        ChangeColor(Color.green);

                    investigatingObject = null;
                    timeElapsed = 0;
                }

                timeElapsed += Time.deltaTime;
            }
        }
    }

    void ChangeColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
    }

    GameObject GetUnCheckedObject() {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 15f);

        GameObject obj;
        foreach (var collider in colliders) {
            if (collider.gameObject.GetComponent<MeshCollider>() != null || collider.gameObject.GetComponent<DoorController>() != null)
                continue;

            obj = collider.gameObject;
            if (obj.transform.root != null && obj.tag != "Player")
                obj = obj.transform.root.gameObject;

            if (obj == gameObject)
                continue;

            if (!investigatedObjects.Contains(obj)) {
                investigatedObjects.Add(obj);
                return obj;
            }
        }

        investigatedObjects.Clear();

        return null;
    }

    public override void End() {
        Reset();
    }

    void Reset()
    {
        investigatedObjects.Clear();
        investigatingObject = null;
        timeElapsed = 0f;
    }

}
