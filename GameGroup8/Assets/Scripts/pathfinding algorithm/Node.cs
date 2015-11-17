using UnityEngine;
using System.Collections;

public class Node {

    public bool Walkable;
    public Vector3 Position;

    public Node(bool wlk, Vector3 pos)
    {
        Walkable = wlk;
        Position = pos;
    }

 }

