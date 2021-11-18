using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    static readonly int GRID_SIZE_X = 200;
    static readonly int GRID_SIZE_Y = 200;
    static readonly int TOTAL_GRID_SIZE = GRID_SIZE_X * GRID_SIZE_Y;
    static readonly int STRAIGHT_COST = 10;
    static readonly int DIAGONAL_COST = 14;
    static readonly int TILE_SIZE = 1;
    static readonly float OFFSET = 0.5f;

    WorldTile[,] tiles = new WorldTile[GRID_SIZE_X, GRID_SIZE_Y];
    HashSet<GameObject> requests = new HashSet<GameObject>();

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
                Vector3 position = new Vector3(x + OFFSET, 1f, z + OFFSET) * TILE_SIZE;

                bool hasGround = Physics.Raycast(position, Vector3.down);
                if (!hasGround)
                    continue;

                bool canWalk = !Physics.CheckSphere(position, 0.5f, 1, QueryTriggerInteraction.Ignore);
                WorldTile tile = new WorldTile(position, canWalk);

                tiles[x, z] = tile;
            }
        }
    }

    public void FindPath(GameObject gameObject, Vector3 endPosition) {
        StartCoroutine(ProcessPath(gameObject, endPosition));
    }

    public IEnumerator ProcessPath(GameObject gameObject, Vector3 endPosition) {
        if (requests.Contains(gameObject)) {
            Debug.Log(this +  " " +gameObject.name + " already requested for a route.");
            yield break;
        }

        requests.Add(gameObject);

        WorldTile startTile = GetTile(gameObject.transform.position);
        WorldTile endTile = GetTile(endPosition);

        if (startTile == null || endTile == null) {
            Debug.Log(this + " Failed to find start/end tile. ");
            yield break;
        }

        Node startNode = new Node(startTile);
        Node endNode = new Node(endTile);
        Node foundNode = null;
        List<Node> unCheckedNodes = new List<Node>() { startNode };
        HashSet <WorldTile> checkedTiles = new HashSet<WorldTile>();
        
        while (unCheckedNodes.Count > 0) {
            Node current = GetBestNode(unCheckedNodes);

            if (current.tile == endTile) {
                foundNode = current;
                break;
            }

            unCheckedNodes.Remove(current);
            checkedTiles.Add(current.tile);

            foreach (var tile in GetNeighbours(current.tile)) {
                if (!tile.canWalk || checkedTiles.Contains(tile))
                    continue;

                Node node = new Node(tile);
                node.hCost = GetDistanceCost(node, endNode); 
                node.gCost = current.gCost + GetDistanceCost(current, node);
                node.fCost = node.hCost + node.gCost;
                node.previousNode = current;
                unCheckedNodes.Add(node);
            }
        }

        if (foundNode != null) {
            gameObject.GetComponent<AIManager>().routeTiles = TracePath(startNode, foundNode);
            requests.Remove(gameObject);
        } else 
            Debug.Log(this + " couldn't find route.");
    }

    public Node GetBestNode(List<Node> nodes) {
        Node bestNode = null;
        int bestFCost = int.MaxValue;

        foreach (var node in nodes) {
            if (node.fCost < bestFCost) {
                bestFCost = node.fCost;
                bestNode = node;
            }
        }
        return bestNode;
    }

    public Stack<WorldTile> TracePath(Node startNode, Node endNode) {
        Stack<WorldTile> tiles = new Stack<WorldTile>();
        Node current = endNode;

        while (current.previousNode != null) {
            tiles.Push(current.tile);
            current = current.previousNode;
        }
        return tiles;
    }

    int GetDistanceCost(Node a, Node b) {
        int distance = (int) Vector3.Distance(a.tile.position, b.tile.position);
        bool isDiagonal = a.tile.position.x != b.tile.position.x && a.tile.position.z != b.tile.position.z;
       
        return distance * (isDiagonal ? DIAGONAL_COST : STRAIGHT_COST);
    }

    List<WorldTile> GetNeighbours(WorldTile tile) {
        List<WorldTile> foundTiles = new List<WorldTile>();
        Vector3 startPosition = tile.position - new Vector3(TILE_SIZE, tile.position.y, TILE_SIZE);

        for (int x = 0; x < 3; x++) {
            for (int z = 0; z < 3; z++) {
                Vector3 nextPosition = new Vector3(startPosition.x + x, tile.position.y, startPosition.z + z);

                WorldTile neighbourTile = GetTile(nextPosition);
                if (neighbourTile == null || neighbourTile == tile)
                    continue;

                foundTiles.Add(neighbourTile);
            }
        }
        return foundTiles;
    }


    private WorldTile GetTile(Vector3 position) {
        Vector3 tilePosition = position - (Vector3.zero * TOTAL_GRID_SIZE);
        int x = (int) tilePosition.x;
        int z = (int) tilePosition.z;

        if (x < 0 || z < 0 || x >= tiles.GetLength(0) || z >= tiles.GetLength(1))
            return null;

        return tiles[x, z];
    }
    
}
