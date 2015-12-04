using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MapGeneratorScript : MonoBehaviour {

    public int map_x;
    public int map_z;

    public float tileSize;

    public Texture2D World_texture;
    public int tileRes;

	// Use this for initialization
	void Start () {
        BuildMesh();
	}
	
    Color[] LoadTexture(int kleurNr)
    {
        int nrTiles_row = 6;
        int nrRows = 2;

        Color[][] tiles = new Color[3][];

        for (int i = 0; i < nrRows; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    tiles[i * nrRows + j] = World_texture.GetPixels(j * tileRes, i * tileRes, tileRes, tileRes);
                }
            }
            if (i == 1)
            {
                for (int j = 0; j < nrTiles_row; j++)
                {
                    tiles[i * nrRows + j] = World_texture.GetPixels(j * tileRes, i * tileRes, tileRes, tileRes);
                }
            }
        }

        return tiles[kleurNr];
    }

    void BuildTexture()
    {
        int texWidth = 10;
        int texHeight = 10;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        for (int i = 0; i < texHeight; i++)
        {
            for (int j = 0; j < texWidth; j++)
            {
                Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                texture.SetPixel(i, j, c);
            }
        }

        texture.Apply();
        texture.filterMode = FilterMode.Point;

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
    }

	public void BuildMesh()
    {
        int nrVer_x = map_x + 1;
        int nrVer_z = map_z + 1;
        int nrVertices = nrVer_x * nrVer_z;

        int nrTiles = map_x * map_z;
        int nrTriangles = nrTiles * 2;

        Vector3[] vertices = new Vector3[nrVertices];
        Vector3[] normals = new Vector3[nrVertices];
        Vector2[] uv = new Vector2[nrVertices];

        int[] triangles = new int[nrTriangles * 3];
        
        int z, x;
        for (z = 0; z < nrVer_z; z++)
        {
            for (x = 0; x < nrVer_x; x++)
            {
                vertices[z * nrVer_x + x] = new Vector3(x * tileSize, Random.Range(0f, 2f), z * tileSize);
                normals[z * nrVer_x + x] = Vector3.up;
                uv[z * nrVer_x + x] = new Vector2((float)x / map_x, (float)z / map_z);
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
        worldMesh.uv = uv;

        //Assign worldMesh to the Mesh in Unity
        MeshFilter mesh_filter = gameObject.GetComponent<MeshFilter>();
        /*MeshCollider mesh_collider = gameObject.GetComponent<MeshCollider>();
        MeshRenderer mesh_renderer = gameObject.GetComponent<MeshRenderer>();*/

        mesh_filter.mesh = worldMesh;

        BuildTexture();
    }
}
