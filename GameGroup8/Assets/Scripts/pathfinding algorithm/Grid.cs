using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class that makes  wall detection possible

    assumes the worldplane is in x, z plane.
*/




public class Grid : MonoBehaviour
{

    public Node SeekerNode;
    public Node TargetNode;
    public Node CurrentNode;

    public bool displayGridCubes;
    public LayerMask WallMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeZ;

    void Awake()
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
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, WallMask));
                grid[x, z] = new Node(walkable, worldPoint, x, z);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();


        // scan neighbours loop
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {

                // is current node -> skip
                if (x == 0 && z == 0)
                {
                    continue;
                }


                int checkX = node.gridX + x;
                int checkZ = node.gridZ + z;

                // check against end of map

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }

        return neighbours;
    }
    // Method that changes a real/world position into a node position

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentZ = (worldPosition.z + gridWorldSize.z / 2) / gridWorldSize.z;
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);
        return grid[x, z];
    }

    // visual check for grid
    public List<Node> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.z));

        if (grid != null && displayGridCubes)
        {
            foreach (Node n in grid)
            {

                // walkable = white !walkable red
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                // Drawcubes as visual aid 
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}