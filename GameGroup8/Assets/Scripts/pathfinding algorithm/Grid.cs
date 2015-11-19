using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class that makes  wall detection possible

    assumes the worldplane is in x, z plane.
*/

public class Grid : MonoBehaviour {


    public Node SeekerNode;
    public Node TargetNode;
    public Node CurrentNode;
    //public Transform Target;
    //public Transform Seeker;
    public LayerMask WallMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;   

    float nodeDiameter;
    int gridSizeX;
    int gridSizeZ;

    public List<Node> path;

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
                grid[x,z] = new Node(walkable, worldPosition, x,z);   
           }
        }
    }

    public List<Node> GetNeighbours(Node nd) { 
        List<Node> neighbours = new List<Node>();

        // scan neighbours loop
        for (int x = -1; x <=1; x++)
        {
            for (int z = -1; z <= 1; z++ )
            {
                // is current node -> skip
                if (x == 0 && z == 0)
                {
                    continue;
                }

                int checkX = nd.gridX + x;
                int checkZ = nd.gridX + z;

                // check against end of map

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                    //print("neighbours added");
                }
            }
        }
        return neighbours;
    }

    // Method that changes a real/world position into a node position

    public Node WorldPositionToNode(Vector3 worldPosition)
    {
        float PercentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float PercentZ = Mathf.Clamp01((worldPosition.z + gridWorldSize.z / 2) / gridWorldSize.z);

        int x = Mathf.RoundToInt((gridSizeX - 1) * PercentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * PercentZ);
        return grid[x,z];
    }

    


    // visual check for grid
    void OnDrawGizmos()
    {        
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1 , gridWorldSize.z));
        
        if (grid != null) {

            // 2 nodes that define target and seeker
            foreach (Node n in grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;

                // Seeker node turns blue
                if (SeekerNode == n)
                {
                    Gizmos.color = Color.blue;
                }
                
                
                                
                // set target node yellow
                if (TargetNode == n)
                {
                    Gizmos.color = Color.yellow;
                }

                if (CurrentNode == n)
                {
                    Gizmos.color = Color.magenta;
                }

                
                if (path != null)
                {

                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.green;
                    }
                }
                
                // Drawcubes as visual aid          
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - .1f));

                
            }
        }

    }

}
