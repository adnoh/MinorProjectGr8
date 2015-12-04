using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* Class that implements the pathfinding algorithm
*/


public class Pathfinding : MonoBehaviour
{

    //public Transform seeker;
    // public Transform target;

    // reference to requestManager and grid
    PathRequestManager requestManager;
    Grid grid;

    // Script must be on the same gameobject to work 
    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSucces = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);


        // Set TargetNode in grid for visual feedback
        // Set SeekerNode in grid for visual feedback
        grid.TargetNode = targetNode;
        grid.SeekerNode = startNode;

        // optimization: only try to find path when it's walkable
        if (startNode.walkable && targetNode.walkable)
        {
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
                    // if target is reached, it's a succes.
                    pathSucces = true;
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
                        //else
                        //   openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        
        yield return null; // wait one frame before returning
        if (pathSucces){
            waypoints = BacktracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSucces);

    }

    // Traces the Path back using the assigned parent of the current node
    Vector3[] BacktracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            // de parent wordt de nieuwe currentNode
            currentNode = currentNode.parent; 
        }


        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    // only add waypoint when direction changes
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        // Vector2 directionOld = Vector2.zero; // to store the direction of the last 2 nodes

        for (int i = 1; i < path.Count; i++)
        {
            //Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridZ - path[i].gridZ);

            // if direction is changed, add the waypoint
            //if (directionNew != directionOld)
            //{
                waypoints.Add(path[i].worldPosition);
            //}
            // directionOld = directionNew;
        }
        // convert to Array
        return waypoints.ToArray();
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
