using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{

    public Transform seeker;
    public Transform target;

    Grid grid;
    // Script must be on the same gameobject to work 
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // Set TargetNode in grid for visual feedback
        // Set SeekerNode in grid for visual feedback
        grid.TargetNode = targetNode;
        grid.SeekerNode = startNode;

        List<Node> openSet = new List<Node>();        
        List<Node> closedSet = new List<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            // zoek de goedkoopste van de openset -> Dit wordt nieuwe currentNode
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                BacktracePath(startNode, targetNode);
                break;
            }

            // voeg de buren toe aan de openset inclusief kosten
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                // if node is not walkable or in the closed set -> skip
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                // bereken de kosten van de nieuwe buren
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                // voeg alleen toe als kosten lager zijn of anders nog niet in openSet zit
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void BacktracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);

        if (dstX > dstZ)
            return 14 * dstZ + 10 * (dstX - dstZ);
        return 14 * dstX + 10 * (dstZ - dstX);
    }
}
