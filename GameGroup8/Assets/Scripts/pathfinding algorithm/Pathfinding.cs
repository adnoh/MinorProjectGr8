using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public Transform seeker;
    public Transform target;
    Grid grid;

	// Script must be on the same gameobject to work 
	void Awake(){
        grid = GetComponent<Grid>();
	}

    void Update()
    {
        FindPath(seeker.position, target.position);
    }
	
    void FindPath(Vector3 StartPosition, Vector3 TargetPosition)
    {

        Node startNode = grid.WorldPositionToNode(StartPosition);       
        Node targetNode = grid.WorldPositionToNode(TargetPosition);
        
        // Set TargetNode
        // Set SeekerNode
        grid.TargetNode = targetNode;
        grid.SeekerNode = startNode;
        // print(targetNode.toString());
        // print(startNode.toString());

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            grid.CurrentNode = currentNode;

            // loop through the openSet to find lowest fCost
            // if fcost is equal, look at hCost(distance to target)
            // 

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                    // grid.CurrentNode = currentNode;
                    // print(currentNode.toString());
                    
                } 
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // if currentnode reached the target, return this node
            if (currentNode == targetNode)
            {

                print("Target found");
                BacktracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {

                // if node is not walkable or in the closed set -> skip
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbor < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbor;
                    neighbour.hCost = GetDistance(currentNode, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                 
                }
            }

        }
    }

    void BacktracePath (Node startNode, Node endNode)
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
        // diagonal move cost 14
        // normal move cost 10
        // can be expanded for normal cost on certain terrain types



        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);

        if (distanceX > distanceZ)
        
            return 14 * distanceZ + 10 * (distanceX - distanceZ);
        return 14 * distanceX + 10 * (distanceZ - distanceX);
        }
}
