using UnityEngine;
using System.Collections;

/// <summary>
/// Class that represents a node in a grid
/// </summary>
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="wlk"> Walkable?</param>
    /// <param name="worldPos">WorldPosition</param>
    /// <param name="X">X-Position in the grid</param>
    /// <param name="Z">z-Position in the grid</param>
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


