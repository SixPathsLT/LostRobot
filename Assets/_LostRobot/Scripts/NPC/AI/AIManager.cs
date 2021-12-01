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
    [HideInInspector]
    public int currentNode = 0;

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
            currentBehaviour.End(this);

        currentBehaviour = aiBehaviour;

        if (currentBehaviour != null)
            currentBehaviour.Init(this);
    }
    static GameObject OBJECT_REQUIRED_REPATH = null;

    void Update() {
        if (currentBehaviour != null && !isStunned)
            currentBehaviour.Process(this);
        
        if (routeTiles == null) {
            nextTile = null;
            //example :)
           // pathfinding.FindPath(gameObject, AIManager.player.transform.position);
        }
        
        if (routeTiles != null && (nextTile == null || Vector3.Distance(transform.position, nextTile.position) < 0.3f)) {
            if (routeTiles.Count < 1)
                routeTiles = null;
            else
                nextTile = routeTiles.Pop();
        } else if (nextTile != null) {
            float aiSpeed = speed;
            float tileMultiplier = (Pathfinding.TILE_SIZE + Pathfinding.OFFSET);
            Vector3 aheadPos = (nextTile.position - transform.position).normalized * tileMultiplier;
            WorldTile tile = pathfinding.GetTile(transform.position + aheadPos);

            if (tile != null)
                tile.canWalk = false;

            if (OBJECT_REQUIRED_REPATH != null || OBJECT_REQUIRED_REPATH != gameObject) {
                if (Utils.HasAI(transform.position + (aheadPos.normalized * (tileMultiplier * 3))))
                    aiSpeed /= 1.5f;
                else if (Utils.HasAI(transform.position + (aheadPos.normalized * (tileMultiplier * 2))))
                    aiSpeed /= 2f;
            }
            
            if (!Utils.HasAI(transform.position + aheadPos) || (OBJECT_REQUIRED_REPATH != null && OBJECT_REQUIRED_REPATH == gameObject)) {
                Vector3 toPosition = nextTile.position;
                if (toPosition.y != transform.position.y)
                    toPosition = new Vector3(toPosition.x, transform.position.y, toPosition.z);

                transform.position = Vector3.MoveTowards(transform.position, toPosition, aiSpeed * Time.deltaTime);

                Quaternion rotation = transform.rotation;
                Vector3 lookDirection = (toPosition - transform.position).normalized;
                if (lookDirection != Vector3.zero)
                    rotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            } else if (OBJECT_REQUIRED_REPATH == null && RePath(false))
                StartCoroutine(ResetRePath());
        }
    }

    bool RePath(bool reduceNodes) {
        WorldTile lastTile = null;
        while (routeTiles.Count > 0)
            lastTile = routeTiles.Pop();

        routeTiles = null;
        nextTile = null;
        if (lastTile != null) {
            pathfinding.FindPath(gameObject, lastTile.position, reduceNodes);
            return true;
        }
        return false;
    }

    public IEnumerator ResetRePath() {
        OBJECT_REQUIRED_REPATH = gameObject;
        yield return new WaitForSeconds(2);
        OBJECT_REQUIRED_REPATH = null;
        yield return new WaitForSeconds(1);
        RePath(true);
    }

    public IEnumerator Stun(float duration) {
        nextTile = null;
        routeTiles = null;
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

}
