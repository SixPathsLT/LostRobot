using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [HideInInspector]
    public bool isStunned;
    public List<GameObject> nodes;

    public static GameObject player;
    public AIBehaviour investigateBehaviour, patrolBehaviour, chaseBehaviour, combatBehaviour, captureBehaviour;
    [HideInInspector]
    public AIBehaviour currentBehaviour;

    [HideInInspector]
    public Stack<WorldTile> routeTiles;
    WorldTile nextTile;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SetBehaviour(investigateBehaviour);
    }

    public void SetBehaviour(AIBehaviour aiBehaviour) {
        if (currentBehaviour != null)
            currentBehaviour.End();

        currentBehaviour = aiBehaviour;

        if (currentBehaviour != null)
            currentBehaviour.Init(gameObject);
    }


    void Update() {
        if (currentBehaviour != null && !isStunned)
            currentBehaviour.Process();

        //example :)
       // if (routeTiles == null)
          //  FindObjectOfType<Pathfinding>().FindPath(gameObject, new Vector3(95.5f, 1, 124.5f));


        if (nextTile != null && nextTile.position.y != transform.position.y)
            nextTile.position = new Vector3(nextTile.position.x, transform.position.y, nextTile.position.z);
        
        if (routeTiles != null && (nextTile == null || Vector3.Distance(transform.position, nextTile.position) < 1)) {
            nextTile = routeTiles.Pop();

            if (routeTiles.Count < 1)
                routeTiles = null;
        } else if (nextTile != null) {
            transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextTile.position - transform.position), 3f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, nextTile.position, 5f * Time.deltaTime);
        }

    }

    public IEnumerator Stun(float duration) {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

}
