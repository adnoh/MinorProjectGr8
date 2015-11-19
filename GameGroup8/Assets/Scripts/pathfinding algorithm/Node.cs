using UnityEngine;
using System.Collections;

public class Node {

    public bool Walkable;
    public Vector3 WorldPosition;

	// Grid Position
    public int gridX;
    public int gridZ;

    public int gCost;  // movement cost
    public int hCost;  // cost to target
    public Node parent;

    // constructor
    public Node(bool wlk, Vector3 pos, int x, int z)
    {
        Walkable = wlk;
        WorldPosition = pos;
        gridX = x;
        gridZ = z;
    }

    // return fCost = total cost
    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }

    public string toString()  
    {
        return ("GridCoordinates:" + this.gridX + ", " + this.gridZ);
    }

}

