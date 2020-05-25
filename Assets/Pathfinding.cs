using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct PathNode {
    public float f, g, h;
    public Vector3Int parent;
    public PathNode(float a, float b, float c, Vector3Int p) {
        f = a;
        g = b;
        h = c;
        parent = p;
    }
}

public class Pathfinding : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    GameBoard gameBoard;

    public static Vector3Int nullParent = new Vector3Int(-5000, -5000, -5000);

    private void Awake()
    {
        gameBoard = transform.parent.GetComponent<GameBoard>();
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    private void Start() {

    }

    public List<Vector3Int> FindBurger() {
        Dictionary<Vector3Int, PathNode> closed = new Dictionary<Vector3Int, PathNode>();
        Dictionary<Vector3Int, PathNode> open = new Dictionary<Vector3Int, PathNode>();
        List<Vector3Int> path = new List<Vector3Int>();

        float h = CalculateH(gameBoard.witchIsoCoordinates, gameBoard.burgerIsoCoordinates);
        float g = 0;
        PathNode node = new PathNode(g + h, g, h, nullParent);
        open.Add(gameBoard.witchIsoCoordinates, node);
        bool stillSearching = true;
        while (stillSearching) {
            KeyValuePair<Vector3Int, PathNode> nextNode = FindNextNodeToExplore(open);
            open.Remove(nextNode.Key);
            closed.Add(nextNode.Key, nextNode.Value);
            foreach(Vector3Int neighbor in GetNeighbors(nextNode.Key)) {
                if (neighbor == gameBoard.burgerIsoCoordinates) {
                    print("Celebrate good times come on");
                    closed.Add(neighbor, new PathNode(0, 0, 0, nextNode.Key));
                    stillSearching = false;
                    break;
                } else if (gameBoard.obstacleIsoCoordinates.Contains(neighbor) || closed.ContainsKey(neighbor)) {
                    continue;
                } else {
                    PathNode pathNode = GenerateNeighboringNode(nextNode, neighbor);
                    if (!open.ContainsKey(neighbor)) {
                        open.Add(neighbor, pathNode);
                    } else if (pathNode.f < open[neighbor].f) {
                        open[neighbor] = pathNode;
                    }
                }
            }
        }

        Vector3Int prevInPath = gameBoard.burgerIsoCoordinates;
        while (prevInPath != nullParent) {
            path.Add(prevInPath);
            prevInPath = closed[prevInPath].parent;
        }
        path.Reverse();

        return path;
    }

    PathNode GenerateNeighboringNode(KeyValuePair<Vector3Int, PathNode> src, Vector3Int dst) {
        float g = src.Value.g + (src.Key - dst).magnitude;
        float h = (gameBoard.burgerIsoCoordinates - dst).magnitude;
        float f = g + h;
        return new PathNode(f, g, h, src.Key);
    }

    Vector3Int[] GetNeighbors(Vector3Int location) {
        List<Vector3Int> allNeighbors = new List<Vector3Int>();

        Vector3Int upNeighbor = location + Vector3Int.up;
        Vector3Int rightNeighbor = location + Vector3Int.right;
        Vector3Int downNeighbor = location + Vector3Int.down;
        Vector3Int leftNeighbor = location + Vector3Int.left;

        bool upIsBlock = gameBoard.obstacleIsoCoordinates.Contains(upNeighbor);
        bool rightIsBlock = gameBoard.obstacleIsoCoordinates.Contains(rightNeighbor);
        bool downIsBlock = gameBoard.obstacleIsoCoordinates.Contains(downNeighbor);
        bool leftIsBlock = gameBoard.obstacleIsoCoordinates.Contains(leftNeighbor);

        // Add straight neighbors
        if (!upIsBlock) allNeighbors.Add(upNeighbor);
        if (!rightIsBlock) allNeighbors.Add(rightNeighbor);
        if (!downIsBlock) allNeighbors.Add(downNeighbor);
        if (!leftIsBlock) allNeighbors.Add(leftNeighbor);

        // Add diagonal neighbors only if both straight neighbors are empty
        if (!upIsBlock && !rightIsBlock) {
            allNeighbors.Add(location + Vector3Int.up + Vector3Int.right);
        }
        if (!rightIsBlock && !downIsBlock) {
            allNeighbors.Add(location + Vector3Int.right + Vector3Int.down);
        }
        if (!downIsBlock && !leftIsBlock) {
            allNeighbors.Add(location + Vector3Int.down + Vector3Int.left);
        }
        if (!leftIsBlock && !upIsBlock) {
            allNeighbors.Add(location + Vector3Int.left + Vector3Int.up);
        }

        return allNeighbors.ToArray();
    }

    KeyValuePair<Vector3Int, PathNode> FindNextNodeToExplore(Dictionary<Vector3Int, PathNode> nodes) {
        PathNode lowestF = new PathNode(999999999, 0, 99999999, nullParent);
        KeyValuePair<Vector3Int, PathNode> selected;
        foreach(KeyValuePair<Vector3Int, PathNode> node in nodes) {
            if (lowestF.f > node.Value.f || (lowestF.f == node.Value.f && lowestF.h > node.Value.h)) {
                lowestF = node.Value;
                selected = node;
            }
        }
        return selected;
    }

    float CalculateH(Vector3Int src, Vector3Int dst) {
        float xDiff = (float)Mathf.Abs(src.x - dst.x);
        float yDiff = (float)Mathf.Abs(src.y - dst.y);
        return (float)(Mathf.Min(xDiff, yDiff) * 1.41) + (float)Mathf.Abs(xDiff - yDiff);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
