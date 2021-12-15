using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [HideInInspector]
    public bool isStunned;

    public float speed = 4.5f;

    public Transform[] nodes;
    [HideInInspector]
    public int currentNode = 0;

    public Animator _anim;
    public static GameObject player;
    public AIBehaviour investigateBehaviour, patrolBehaviour, chaseBehaviour, combatBehaviour, captureBehaviour;
   // [HideInInspector]
    public AIBehaviour currentBehaviour;

    public int invIndex;

    [HideInInspector]
    public Stack<WorldTile> routeTiles;
    [HideInInspector]
    public WorldTile nextTile;

    [HideInInspector]
    public Pathfinding pathfinding;

    readonly float tileMultiplier = (Pathfinding.TILE_SIZE + Pathfinding.OFFSET);
    [HideInInspector]
    public float lastPunch, health;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        SetBehaviour(patrolBehaviour);
        pathfinding = FindObjectOfType<Pathfinding>();
        _anim = GetComponentInChildren<Animator>();
        health = 100f;
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

    internal void TakeDamage()
    {
        if (isStunned)
            return;

        health -= 30f;//instant 
        if (health < 0)
            StartCoroutine(Stun(20));
    }

    void FixedUpdate() {
        if (!GameManager.GetInstance().InPlayingState() && !Utils.CanSeeTransform(transform, player.transform, 360) || GameManager.GetInstance().InCutsceneState() || GameManager.GetInstance().InCapturedState())
            return;
        if (lastPunch > 0f)
            lastPunch -= Time.deltaTime;

        if (currentBehaviour != null && !isStunned)
            currentBehaviour.Process(this);
        
        if (routeTiles == null) {
            nextTile = null;
            //example :)
           // pathfinding.FindPath(gameObject, AIManager.player.transform.position);
        }
        Vector3 toPosition = nextTile == null ? Vector3.zero : nextTile.position;
        if (toPosition.y != transform.position.y)
            toPosition = new Vector3(toPosition.x, transform.position.y, toPosition.z);

        _anim.SetBool("Walking", false);

        if (routeTiles != null && (nextTile == null || Vector3.Distance(transform.position, toPosition) < 0.3f)) {
            if (routeTiles.Count < 1)
                routeTiles = null;
            else {
                nextTile = routeTiles.Pop();
               
                WorldTile correctedTile = PathCorrection(nextTile.position);
                if (correctedTile != null)
                    nextTile = correctedTile;
            }
        } else if (nextTile != null) {  
            float aiSpeed = speed;
            if (_anim.GetBool("Run"))
                aiSpeed *= 2.5f;

            Vector3 aheadPos = (toPosition - transform.position).normalized * tileMultiplier;
            WorldTile tile = Pathfinding.GetTile(transform.position + aheadPos);

            if (tile != null)
                tile.canWalk = false;

            //if (OBJECT_REQUIRED_REPATH != null || OBJECT_REQUIRED_REPATH != gameObject) {
                if (Utils.HasEntity(transform.position + (aheadPos.normalized * (tileMultiplier * 3)), false))
                    aiSpeed /= 1.5f;
                else if (Utils.HasEntity(transform.position + (aheadPos.normalized * (tileMultiplier * 2)), false))
                    aiSpeed /= 2f;
            // }

            if (!Utils.HasEntity(transform.position + aheadPos) || (OBJECT_REQUIRED_REPATH != null && OBJECT_REQUIRED_REPATH == gameObject)) {
                _anim.SetBool("Walking", true);
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


    WorldTile PathCorrection(Vector3 nextPosition) {
        RaycastHit hit;
        WorldTile correctedTile = null;
        Vector3 toPos = nextPosition;
        if (Physics.Raycast(toPos, Vector3.forward + Vector3.right, out hit, 2))
            toPos += (toPos - hit.point).normalized * 1.6f;
        if (Physics.Raycast(toPos, Vector3.back + Vector3.right, out hit, 2))
            toPos += (toPos - hit.point).normalized * 1.6f;
        if (Physics.Raycast(toPos, Vector3.forward + Vector3.left, out hit, 2))
            toPos += (toPos - hit.point).normalized * 1.6f;
        if (Physics.Raycast(toPos, Vector3.back + Vector3.left, out hit, 2))
            toPos += (toPos - hit.point).normalized * 1.6f;
    
        correctedTile = Pathfinding.GetTile(toPos);
        if (correctedTile != null && correctedTile.canWalk)
            nextTile = correctedTile;

        return correctedTile;
    }

    bool RePath(bool reduceNodes) {
        float tileMultiplier = (Pathfinding.TILE_SIZE + Pathfinding.OFFSET);
        Vector3 rightPos = transform.position + (transform.right * tileMultiplier);
        Vector3 leftPos = transform.position + (-transform.right * tileMultiplier);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, rightPos - transform.position, out hit, Vector3.Distance(rightPos, transform.position)))
            rightPos = hit.point - (transform.right / 2) + (transform.forward * tileMultiplier);
        
        if (Physics.Raycast(transform.position, leftPos - transform.position, out hit, Vector3.Distance(leftPos, transform.position)))
            leftPos = hit.point + (transform.right / 2) + (transform.forward * tileMultiplier);
        

        WorldTile rightTile = Pathfinding.GetTile(rightPos);
        WorldTile leftTile = Pathfinding.GetTile(leftPos);

        if (leftTile != null && rightTile != null) {
            WorldTile toTile = Vector3.Distance(rightTile.position, transform.position) > Vector3.Distance(leftTile.position, transform.position) ? rightTile : leftTile;

            float dist = Vector3.Distance(toTile.position, transform.position);
            if (!Physics.Raycast(transform.position, toTile.position - transform.position, out hit, dist)) {
                routeTiles.Push(nextTile);
                nextTile = toTile;
                return true;
            }
        }

        routeTiles = null;
        nextTile = null;
        currentNode = Random.Range(0, nodes.Length);
        return false;
    }

    public IEnumerator ResetRePath() {
        OBJECT_REQUIRED_REPATH = gameObject;
        yield return new WaitForSeconds(2);
        OBJECT_REQUIRED_REPATH = null;
    }

    public IEnumerator Stun(float duration) {
        nextTile = null;
        routeTiles = null;
        isStunned = true;
        _anim.SetBool("S", true);
        SetBehaviour(patrolBehaviour);
        GetComponentInChildren<EnemyAudioControl>().Stun();
        yield return new WaitForSeconds(duration);        
        _anim.SetBool("S", false);
        yield return new WaitForSeconds(3);
        isStunned = false;
        health = 100f;
    }

}
