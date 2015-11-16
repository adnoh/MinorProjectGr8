using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    // untested 3D code is currently commented out.

    public LayerMask unwalkableMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;   // Node[,,] grid; for 3D

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;    
    int gridSizeZ;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter); 
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        // get the position of bottomleft cornet: get center minus left edge  - upper edge
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        print(worldBottomLeft);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask ));
                print(walkable);
                print(worldPoint);
                grid[x,y] = new Node(walkable, worldPoint);
                
                
                
                // for (int z = 0; x < gridSizeZ; z++)
                // {
                //
                // }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        // visual check for grid
        if (grid != null) {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - .1f));
            }
        }

    }



}
