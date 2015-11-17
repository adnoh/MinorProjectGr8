using UnityEngine;
using System.Collections;

/* Class that makes  wall detection possible
*/

public class Grid : MonoBehaviour {


    public GameObject Seeker;
    public LayerMask WallMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;   

    float nodeDiameter;
    int gridSizeX;
    int gridSizeZ;
    


    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);
        // TODO: Get dynamically from worldsize

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeZ];
        // get the position of bottomleft cornet: get center minus left edge  - upper edge
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.z / 2;
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 worldPosition = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPosition, nodeRadius, WallMask ));
                grid[x,z] = new Node(walkable, worldPosition);   
           }
        }
    }

    // Method that changes a real/world position into a node position

    Node WorldPositionToNode(Vector3 worldPosition)
    {
        float PercentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float PercentZ = Mathf.Clamp01((worldPosition.z + gridWorldSize.z / 2) / gridWorldSize.z);

        int x = Mathf.RoundToInt((gridSizeX - 1) * PercentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * PercentZ);
        return grid[x, z];
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, .5f , gridWorldSize.z));
        // visual check for grid
        if (grid != null) {
            Node SeekerNode = WorldPositionToNode(Seeker.transform.position);
            foreach (Node n in grid)
               
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
               if (SeekerNode == n)
                {
                    Gizmos.color = Color.blue;
                }
                
                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - .1f));

                
            }
        }

    }

}
