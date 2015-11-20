using UnityEngine;
using System.Collections;

public class Node
{

    public bool walkable;
    public Vector3 worldPosition;

    // Grid Position
    public int gridX;
    public int gridZ;

    public int gCost;   // movement cost
    public int hCost; // cost to target
    public Node parent;

    public Node(bool wlk, Vector3 worldPos, int X, int Z)
    {
        walkable = wlk;
        worldPosition = worldPos;
        gridX = X;
        gridZ = Z;
    }

    // return fCost = total cost
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}


