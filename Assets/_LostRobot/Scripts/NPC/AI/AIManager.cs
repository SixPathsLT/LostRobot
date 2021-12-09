using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [HideInInspector]
    public bool isStunned;

    public float speed = 4.5f;

    [Header("Vision")]
    [SerializeField]
    float maxDegrees = 45f;
    [SerializeField]
    float maxDistance = 10f;

    public List<GameObject> nodes;
    [HideInInspector]
    public int currentNode = 0;

    public Animator _anim;
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
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetBehaviour(AIBehaviour aiBehaviour) {
        if (currentBehaviour != null)
            currentBehaviour.End(this);

        currentBehaviour = aiBehaviour;

        if (currentBehaviour != null)
            currentBehaviour.Init(this);

        routeTiles = null;
        nextTile = null;
    }
    static GameObject OBJECT_REQUIRED_REPATH = null;

    void FixedUpdate() {
        if (!GameManager.GetInstance().InPlayingState() && !Utils.CanSeeTransform(transform, player.transform, 360))
            return;

        if (currentBehaviour != null && !isStunned)
            currentBehaviour.Process(this);
        else _anim.SetTrigger("stun");

        if (routeTiles == null) {
            nextTile = null;
            //example :)
           // pathfinding.FindPath(gameObject, AIManager.player.transform.position);
        }
        Vector3 toPosition = nextTile == null ? Vector3.zero : nextTile.position;
        if (toPosition.y != transform.position.y)
            toPosition = new Vector3(toPosition.x, transform.position.y, toPosition.z);

        if (routeTiles != null && (nextTile == null || Vector3.Distance(transform.position, toPosition) < 0.3f)) {
            if (routeTiles.Count < 1)
                routeTiles = null;
            else
                nextTile = routeTiles.Pop();
        } else if (nextTile != null) {
            _anim.SetBool("Walking", true);
            float aiSpeed = speed;
            float tileMultiplier = (Pathfinding.TILE_SIZE + Pathfinding.OFFSET);
            Vector3 aheadPos = (toPosition - transform.position).normalized * tileMultiplier;
            WorldTile tile = pathfinding.GetTile(transform.position + aheadPos);

            if (tile != null)
                tile.canWalk = false;

            //if (OBJECT_REQUIRED_REPATH != null || OBJECT_REQUIRED_REPATH != gameObject) {
                if (Utils.HasEntity(transform.position + (aheadPos.normalized * (tileMultiplier * 3))))
                    aiSpeed /= 1.5f;
                else if (Utils.HasEntity(transform.position + (aheadPos.normalized * (tileMultiplier * 2))))
                    aiSpeed /= 2f;
           // }
            
            if (!Utils.HasEntity(transform.position + aheadPos) || (OBJECT_REQUIRED_REPATH != null && OBJECT_REQUIRED_REPATH == gameObject)) {
                transform.position = Vector3.MoveTowards(transform.position, toPosition, aiSpeed * Time.deltaTime);

                Quaternion rotation = transform.rotation;
                Vector3 lookDirection = (toPosition - transform.position).normalized;
                if (lookDirection != Vector3.zero)
                    rotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            } else if (OBJECT_REQUIRED_REPATH == null && RePath(false))
                StartCoroutine(ResetRePath());
        }
        else _anim.SetBool("Walking", false);
    }

    bool RePath(bool reduceNodes) {
        /*WorldTile lastTile = null;
        while (routeTiles != null && routeTiles.Count > 0)
            lastTile = routeTiles.Pop();

        routeTiles = null;
        nextTile = null;
        if (lastTile != null) {
            pathfinding.FindPath(gameObject, lastTile.position, reduceNodes);
            return true;
        }*/

        float tileMultiplier = (Pathfinding.TILE_SIZE + Pathfinding.OFFSET);
        Vector3 aheadPos = transform.position + ((nextTile.position - transform.position).normalized * tileMultiplier);
        WorldTile tile = pathfinding.GetTile(aheadPos);
        WorldTile[] tiles = pathfinding.GetNeighbours(tile);
        if (tile != null && tiles != null) {
            foreach (var t in tiles) {
                if (t == null || !t.canWalk || Utils.HasEntity(t.position))
                    continue;

                if (!Physics.Raycast(transform.position, (t.position - transform.position), tileMultiplier)) {
                    if (Vector3.Distance(nextTile.position, t.position) < 3)
                        nextTile = t;
                    else {
                        routeTiles.Push(nextTile);
                        nextTile = t;
                    }
                    return true;
                }
            }
        }

        routeTiles = null;
        nextTile = null;
        currentNode = Random.Range(0, nodes.Count);
        return false;
    }

    public IEnumerator ResetRePath() {
        OBJECT_REQUIRED_REPATH = gameObject;
        yield return new WaitForSeconds(2);
        OBJECT_REQUIRED_REPATH = null;
        //yield return new WaitForSeconds(1);
        //RePath(true);
    }

    public IEnumerator Stun(float duration) {
        nextTile = null;
        routeTiles = null;
        isStunned = true;
        _anim.SetTrigger("stun");
        yield return new WaitForSeconds(duration);
        isStunned = false;
        _anim.SetBool("S", false);
    }

}
