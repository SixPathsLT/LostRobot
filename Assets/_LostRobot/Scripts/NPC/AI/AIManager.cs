using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [HideInInspector]
    public bool isStunned;

    [SerializeField]
    float speed = 4.5f;

    [Header("Vision")]
    [SerializeField]
    float maxDegrees = 45f;
    [SerializeField]
    float maxDistance = 10f;

    public List<GameObject> nodes;

    public static GameObject player;
    public AIBehaviour investigateBehaviour, patrolBehaviour, chaseBehaviour, combatBehaviour, captureBehaviour;
   // [HideInInspector]
    public AIBehaviour currentBehaviour;

    [HideInInspector]
    public Stack<WorldTile> routeTiles;
    [HideInInspector]
    public WorldTile nextTile;

    [HideInInspector]
    public Pathfinding pathfinding;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SetBehaviour(patrolBehaviour);
        pathfinding = FindObjectOfType<Pathfinding>();
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

        if (routeTiles == null) {
            nextTile = null;
            //example :)
           // pathfinding.FindPath(gameObject, AIManager.player.transform.position/*new Vector3(95.5f, 1, 124.5f)*/);
        }

        if (nextTile != null && nextTile.position.y != transform.position.y)
            nextTile.position = new Vector3(nextTile.position.x, transform.position.y, nextTile.position.z);
        
        if (routeTiles != null && (nextTile == null || Vector3.Distance(transform.position, nextTile.position) < 0.1f)) {
            if (routeTiles.Count < 1)
                routeTiles = null;
            else
                nextTile = routeTiles.Pop();
        } else if (nextTile != null) {
            Quaternion rotation = transform.rotation;
            Vector3 lookDirection = (nextTile.position - transform.position).normalized;
            if (lookDirection != Vector3.zero)
                rotation = Quaternion.LookRotation(lookDirection);

            transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, nextTile.position, speed * Time.deltaTime);
        }
    }

    public IEnumerator Stun(float duration) {
        nextTile = null;
        routeTiles = null;
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

}
