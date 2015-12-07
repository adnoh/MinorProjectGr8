using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MapGeneratorScriptV1 : MonoBehaviour {

    public int map_x;
    public int map_z;

    public float tileSize;

    public Texture2D World_texture;
    public int tileRes;

	// Use this for initialization
	void Start () {
        BuildMesh();
	}
	
    Color[][] LoadTexture()
    {
        int nrTiles_row = 6;
        int nrRows = 2;

        Color[][] tiles = new Color[nrTiles_row*nrRows][];

        for (int i = 0; i < nrRows; i++) 
        {
                for (int j = 0; j < nrTiles_row; j++) 
                {
                    tiles[i * nrTiles_row + j] = World_texture.GetPixels(j * tileRes, i * tileRes, tileRes, tileRes);
                }
        }
        
        return tiles;
    }

    void BuildTexture()
    {
        int texWidth = tileRes * map_x;
        int texHeight = tileRes * map_z;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        Color[][] textures = LoadTexture();

        for (int i = 0; i < map_z; i++)
        {
            for (int j = 0; j < map_x; j++)
            {
                Color[] c = textures[Random.Range(0,textures.Length)];
                texture.SetPixels(j * tileRes, i * tileRes, tileRes, tileRes, c); 
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        
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
                vertices[z * nrVer_x + x] = new Vector3(x * tileSize, Random.Range(0f, 0f), z * tileSize);
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
