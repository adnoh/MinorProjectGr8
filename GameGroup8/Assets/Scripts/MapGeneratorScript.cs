using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MapGeneratorScript : MonoBehaviour {

    public int map_x;
    public int map_z;

    private float tileSize = 1.0f;

	// Use this for initialization
	void Start () {
        BuildMesh();
	}
	
	void BuildMesh()
    {
        int nrVer_x = map_x + 1;
        int nrVer_z = map_z + 1;
        int nrVertices = nrVer_x * nrVer_z;
        int nrTiles = map_x * map_z;
        int nrTriangles = nrTiles * 2;

        Vector3[] vertices = new Vector3[nrVertices];
        Vector3[] normals = new Vector3[nrVertices];
        int[] triangles = new int[nrTriangles * 3];
        //Vector2[] uv = new Vector2[nrVertices];

        int z, x;
        for (z = 0; z < nrVer_z; z++)
        {
            for (x = 0; x < nrVer_x; x++)
            {
                vertices[z * nrVer_x + x] = new Vector3(x * tileSize, 0, z * tileSize);
                normals[z * nrVer_x + x] = Vector3.up;
            }
        }

        for (z = 0; z < map_z; z++)
        {
            for (x = 0; x < map_x; x++)
            {
                int tileIndex = z * map_x + x;
                int triangleIndex = tileIndex * 6;
                triangles[triangleIndex + 0] = z * nrVer_x + x + 0;
                triangles[triangleIndex + 1] = z * nrVer_x + x + nrVer_x + 0;
                triangles[triangleIndex + 2] = z * nrVer_x + x + nrVer_x + 1;
                
                triangles[triangleIndex + 3] = z * nrVer_x + x + 0;
                triangles[triangleIndex + 4] = z* nrVer_x +x + nrVer_x + 1;
                triangles[triangleIndex + 5] = z * nrVer_x + x + 1;
            }
        }
        

        //Create Mesh
        Mesh worldMesh = new Mesh();
        worldMesh.vertices = vertices;
        worldMesh.triangles = triangles;
        worldMesh.normals = normals;

        //Assign worldMesh to the Mesh in Unity
        MeshFilter mesh_filter = gameObject.GetComponent<MeshFilter>();
        MeshCollider mesh_collider = gameObject.GetComponent<MeshCollider>();
        MeshRenderer mesh_renderer = gameObject.GetComponent<MeshRenderer>();

        mesh_filter.mesh = worldMesh;

    }
}
