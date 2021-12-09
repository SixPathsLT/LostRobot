using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    static readonly bool DEBUGGING = false;
    static readonly int GRID_SIZE_X = 200;
    static readonly int GRID_SIZE_Y = 200;
    static readonly int TOTAL_GRID_SIZE = GRID_SIZE_X * GRID_SIZE_Y;
    static readonly int STRAIGHT_COST = 10;
    static readonly int DIAGONAL_COST = 14;
    public static readonly float TILE_SIZE = 1.4f;
    public static readonly float OFFSET = 0.5f;

    WorldTile[,] tiles = new WorldTile[GRID_SIZE_X, GRID_SIZE_Y];
    HashSet<GameObject> requests = new HashSet<GameObject>();

    public Stack<WorldTile> routeTiles;

    //visual debugging purposes
    List<WorldTile> neighbourTiles = new List<WorldTile>();
    HashSet<WorldTile> checkedTiles;

    WorldTile currentTile;

    public class Node {
        public WorldTile tile;
        public Node previousNode;

        public int gCost, hCost, fCost;

        public Node(WorldTile tile) {
            this.tile = tile;
        }
    }

    void Start() {
        for (int x = 0; x < GRID_SIZE_X; x++) {
            for (int z = 0; z < GRID_SIZE_Y; z++) {
                Vector3 position = Vector3.right * (x * TILE_SIZE + OFFSET) + Vector3.forward * (z * TILE_SIZE + OFFSET);
                position.y = 1f;

                bool hasGround = Physics.Raycast(position + ( Vector3.up * 1), Vector3.down);
                if (!hasGround)
                    continue;
                
                bool canWalk = !Physics.CheckSphere(position, 0.5f, 1, QueryTriggerInteraction.Ignore);
                WorldTile tile = new WorldTile(position, canWalk);
   
                tiles[x, z] = tile;
            }
        }
    }

    void FixedUpdate() {
        foreach (var tile in tiles) {
            if (tile == null)
                continue;
            tile.canWalk = !Physics.CheckSphere(tile.position, 0.5f, 1, QueryTriggerInteraction.Ignore);
        }
    }

    public void FindPath(GameObject gameObject, Vector3 endPosition, bool reduceNodes = false) {
        if (requests.Contains(gameObject)) {
            Debug.Log(this + " " + gameObject.name + " already requested for a route.");
            return;
        }

        requests.Add(gameObject);
        StartCoroutine(ProcessPath(gameObject, endPosition, reduceNodes));
    }

    public IEnumerator ProcessPath(GameObject gameObject, Vector3 endPosition, bool reduceNodes = false) {
        WorldTile startTile = GetTile(gameObject.transform.position);
        WorldTile endTile = GetTile(endPosition, true);
        //if (startTile != null && !startTile.canWalk)
        //    startTile = GetNeighbours(startTile)[0];
        if (endTile != null && !endTile.canWalk)
           endTile = GetNeighbours(endTile)[0];
                
        if (startTile == null || endTile == null) {
            Debug.Log(this + " Failed to find start/end tile. ");
            requests.Remove(gameObject);
            yield break;
        }

        currentTile = startTile;
        Node startNode = new Node(startTile);
        Node endNode = new Node(endTile);
        Node foundNode = null;
        List<Node> unCheckedNodes = new List<Node>() { startNode };
        checkedTiles = new HashSet<WorldTile>();
        
        Vector3 rayStartPos = gameObject.transform.position;
        bool quickFind = false;
        RaycastHit hit;

        Vector3 targetDirection = (endPosition - rayStartPos);
        rayStartPos.y = 0.5f;
        targetDirection.y = 0.5f;
        if (Physics.Raycast(rayStartPos, targetDirection.normalized * Vector3.Distance(rayStartPos, endPosition), out hit, Vector3.Distance(rayStartPos, endPosition))) {
            if (hit.collider.CompareTag("Player")) {
                foundNode = endNode;
                //foundNode.previousNode = startNode;
                quickFind = true;
            }
        } else {
            foundNode = endNode;
            //foundNode.previousNode = startNode;
            quickFind = true;
        }

        while (foundNode == null && unCheckedNodes.Count > 0) {
            Node current = GetBestNode(unCheckedNodes);
            if (current == null)
                continue;
            
            currentTile = current.tile;

            unCheckedNodes.Remove(current);
            checkedTiles.Add(current.tile);
            neighbourTiles.Clear();

            if (current.tile == endTile) {
                foundNode = current;
                break;
            }

            foreach (var tile in GetNeighbours(current.tile)) {
                if (tile == null || checkedTiles.Contains(tile))
                    continue;

                neighbourTiles.Add(tile);

                Node node = new Node(tile);
                node.hCost = GetDistanceCost(node, endNode);
                node.gCost = current.gCost + GetDistanceCost(current, node);
                node.fCost = node.hCost + node.gCost;
                node.previousNode = current;

                unCheckedNodes.Add(node);
            }
        }

        if (foundNode != null) {
            if (quickFind)
                reduceNodes = false;

            gameObject.GetComponent<AIManager>().routeTiles = TracePath(startNode, foundNode, gameObject, reduceNodes);
            gameObject.GetComponent<AIManager>().nextTile = null;
        } else
            Debug.Log(this + " couldn't find route.");

        requests.Remove(gameObject);
    }

    public Node GetBestNode(List<Node> nodes) {
        Node bestNode = null;
        int bestFCost = int.MaxValue;

        for (int i = nodes.Count - 1; i >= 0; i--) {
            Node node = nodes[i];
            if (checkedTiles.Contains(node.tile)) {
                nodes.Remove(node);
                continue;
            }

            if (node.fCost < bestFCost) {
                bestFCost = node.fCost;
                bestNode = node;
            }
        }
        return bestNode;
    }

    private bool SkippableNode(Node prevNode, Node nextNode) {
        Vector3 prevPosition = prevNode.tile.position;
        Vector3 nextPosition = nextNode.tile.position;

        float dist = Vector3.Distance(prevPosition, nextPosition);

        prevPosition.y = 0.7f;
        nextPosition.y = 0.7f;

        bool canWalk = !Physics.CheckSphere(nextPosition, 0.9f, 0, QueryTriggerInteraction.Ignore);
        Vector3 targetDirection = (nextPosition - prevPosition);

        RaycastHit hit;
        if (Physics.Raycast(prevPosition, targetDirection, out hit, dist, 1, QueryTriggerInteraction.Ignore)) {
           if (hit.collider.CompareTag("Player"))
             return true;
        } else if(canWalk)
                return true;
        
        return false;
    }

    public Stack<WorldTile> TracePath(Node startNode, Node endNode, GameObject aiObject, bool reduceNodes) {
        Stack<WorldTile> routeTiles = new Stack<WorldTile>();
        Node current = endNode;
        int totalNodes = -1;

        AddTileToRoute(routeTiles, endNode.tile);

        while (current != null) {
            Node runningNode = current;
            
            if (reduceNodes) {
                while (runningNode.previousNode != null && runningNode.previousNode.previousNode != null) {
                    if (!SkippableNode(runningNode.previousNode.previousNode, current)) {
                        AddTileToRoute(routeTiles, runningNode.tile);
                        AddTileToRoute(routeTiles, runningNode.previousNode.tile);
                        break;
                    } else
                        totalNodes += 2;

                    runningNode = runningNode.previousNode;
                }
            } else
                AddTileToRoute(routeTiles, runningNode.tile);
            
            totalNodes++;
            current = runningNode.previousNode;
        }

        //Debug.Log(this + "Total nodes: " + totalNodes+ ", skipped " + (totalNodes - routeTiles.Count) + " unnecessary nodes, returned: " + routeTiles.Count+" waypoints.");
        if (DEBUGGING)  {
            this.routeTiles = new Stack<WorldTile>(new Stack<WorldTile>(routeTiles));
            AddTileToRoute(this.routeTiles, GetTile(aiObject.transform.position));
        }
        return routeTiles;
    }

    void AddTileToRoute(Stack<WorldTile> routeTiles, WorldTile tile) {
        if(!routeTiles.Contains(tile))
            routeTiles.Push(tile);
    }

    int GetDistanceCost(Node a, Node b) {
        int distance = (int) Vector3.Distance(a.tile.position, b.tile.position);
        return distance * (Utils.IsDiagonal(a.tile, b.tile) ? DIAGONAL_COST : STRAIGHT_COST);
    }

   readonly int rows = 3;
   readonly int columns = 3;
   public WorldTile[] GetNeighbours(WorldTile tile) {
        WorldTile[] foundTiles = new WorldTile[rows * columns];
        Vector3 startPosition = tile.position - new Vector3(TILE_SIZE, tile.position.y, TILE_SIZE);

        int index = 0;
        for (int x = 0; x < rows; x++) {
            for (int z = 0; z < columns; z++) {
                Vector3 nextPosition = new Vector3(startPosition.x + x * TILE_SIZE, tile.position.y, startPosition.z + z * TILE_SIZE);

                WorldTile neighbourTile = GetTile(nextPosition);
                if (neighbourTile == null || neighbourTile == tile || !neighbourTile.canWalk)
                    continue;

                foundTiles[index] = neighbourTile;
                index++;
            }
        }
        return foundTiles;
    }

    private bool IsOutOfBounds(int x, int z) {
        if (x < 0 || z < 0 || x >= tiles.GetLength(0) || z >= tiles.GetLength(1))
            return true;
        return false;
    }

    private Vector3 GetTilePosition(Vector3 position) {
        return (position - (Vector3.zero * TOTAL_GRID_SIZE)) / TILE_SIZE;
    }

    public WorldTile GetTile(Vector3 position, bool retry = false) {

        Vector3 tilePosition = GetTilePosition(position);
        int x = (int) tilePosition.x;
        int z = (int) tilePosition.z;

        if (IsOutOfBounds(x, z))
            return null;

        WorldTile tile = tiles[x, z];

        if ((tile == null || (tile != null && !tile.canWalk && retry)) && retry) {
            float offset = TILE_SIZE / 2f;
            
            float bestDistance = float.MaxValue;
            WorldTile bestTile = null;
            for (int row = 0; row < 3; row++) {
                for (int column = 0; column < 3; column++)  {
                    position = new Vector3(position.x + (row * offset), position.y, position.z + (column * offset));
                    bool hasGround = Physics.Raycast(position + (Vector3.up * 100f), Vector3.down);

                    tilePosition = GetTilePosition(position);
                    x = (int) tilePosition.x;
                    z = (int) tilePosition.z;

                    if (IsOutOfBounds(x, z) || !hasGround)
                        continue;

                    tile = tiles[x, z];
                    float distance = Vector3.Distance(position, tilePosition);

                    if (bestTile == null)
                        bestTile = tile;
                    if (tile != null && tile.canWalk && distance < bestDistance) {
                        bestDistance = distance;
                        bestTile = tile;
                        break;
                    }
                }
            }

            if (bestTile != null && !bestTile.canWalk)
                bestTile = GetNeighbours(bestTile)[0];

            tile = bestTile;
        }
        return tile;
    }

    private void OnDrawGizmos() {
        if (!DEBUGGING)
            return;

        Vector3 size = new Vector3(GRID_SIZE_X, 1, GRID_SIZE_Y);
        Gizmos.DrawWireCube(size / 2, size);

        if (tiles != null) {
            foreach (var tile in tiles) {
                if (tile == null)
                    continue;
                
                Gizmos.color = tile.canWalk ? Color.green : Color.red;

                Vector3 position = tile.position;
                position.y = 0f;

                if (routeTiles != null && routeTiles.Contains(tile))
                    Gizmos.color = Color.cyan;
                else if (checkedTiles != null && checkedTiles.Contains(tile))
                    Gizmos.color = Color.yellow;

                if (currentTile != null && currentTile == tile)
                    Gizmos.color = Color.blue;

                if (neighbourTiles != null && neighbourTiles.Contains(tile))
                    Gizmos.color = Color.magenta;

                Gizmos.DrawCube(position, new Vector3(TILE_SIZE, 0.01f, TILE_SIZE));

            }
        }

        WorldTile nextTile = null;

        if (routeTiles != null) {
            foreach (var tile in routeTiles) {
                Gizmos.color = Color.magenta;
                if (nextTile == null)
                    nextTile = tile;
                else
                    Gizmos.DrawLine(nextTile.position, tile.position);
                
                nextTile = tile;
            }
        }

    }

}
