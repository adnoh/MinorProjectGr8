using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilderII : MonoBehaviour {

    public int map_x;
    public int map_z;

    public Texture2D World_texture;
    public int tileRes;

    public GameObject Tree;
    public int nrTrees;
    public GameObject House;
    public int nrHouses;

    private List<Vector3> TreePos = new List<Vector3>();
    private List<Vector3[]> HouseInfo = new List<Vector3[]>();
    private List<Vector3> HousePos = new List<Vector3>();
    private float offset = 0.5f;

    // Initialization on play
    void Start()
    {
        WorldBuilderII nieuw = new WorldBuilderII();
    }

    public WorldBuilderII()
    {
        TDMapII map = new TDMapII(map_x, map_z);

        BuildTexture(map);
        ApplyAssets(map);
    }

    public WorldBuilderII(int[][] map)
    {

    }

    Color[][] LoadTexture()
    {
        int nrTiles_row = 6;
        int nrRows = 2;

        Color[][] tiles = new Color[nrTiles_row * nrRows][];

        for (int i = 0; i < nrRows; i++)
        {
            for (int j = 0; j < nrTiles_row; j++)
            {
                tiles[i * nrTiles_row + j] = World_texture.GetPixels(j * tileRes, i * tileRes, tileRes, tileRes);
            }
        }

        return tiles;
    }

    public void BuildTexture(TDMapII map)
    {
        int texWidth = tileRes * map_x;
        int texHeight = tileRes * map_z;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        Color[][] textures = LoadTexture();

        for (int i = 0; i < map_z; i++)
        {
            for (int j = 0; j < map_x; j++)
            {
                Color[] c = textures[map.getTile(i, j)];
                texture.SetPixels(j * tileRes, i * tileRes, tileRes, tileRes, c);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
    }

    public void ApplyAssets(TDMapII map)
    {
        int[] Village = map.getVillages();
        int[][] Forrests = map.getForrests();

        
        int x_pos = Village[0];
        int y_pos = Village[1];
        int min = -40;
        int max = 40;
        
        for (int i = 0; i < 8; i++)
        {
            if (HousePos.Count == 60)
                break;

            for (int j = 0; j < 8; j++)
            {
                if (HousePos.Count == 60)
                    break;

                int x = y_pos - 35 + 10*i;
                int z = x_pos - 35 + 10*j;

                Vector3 Pos = new Vector3(150 - x + offset, 0.8f, 150 - z + offset);

                if (HousePossible(x, z, map) && ObjPossible(HousePos, Pos, 5))
                {
                    Vector3 Rot = new Vector3(90, Random.Range(0, 2)*90, 0);
                    GameObject huisje = (GameObject)Instantiate(House, Pos, Quaternion.identity);
                    huisje.transform.Rotate(Rot);
                    HousePos.Add(Pos);
                    HouseInfo.Add(new Vector3[2] { Pos, Rot });
                }
            } 
        }
        /*
        float misses = 20;
        while (HousePos.Count <= nrHouses)
        {
            if (misses <= 0)
                break;

            int x = Random.Range(min, max) + y_pos;
            int z = Random.Range(min, max) + x_pos;

            Vector3 Pos = new Vector3(150 - x + offset, 0.8f, 150 - z + offset);

            if (CheckIfPlacableTile(x, z, map) && ObjPossible(HousePos,Pos, 30))
            {
                Vector3 Rot = new Vector3(90, Random.Range(0, 360), 0);
                GameObject huisje = (GameObject)Instantiate(House, Pos, Quaternion.identity);
                huisje.transform.Rotate(Rot);
                HousePos.Add(Pos);
                HouseInfo.Add(new Vector3[2] { Pos, Rot });
            }
            else
            {
                misses--;
            }

        }
        //Debug.Log("misses left(place): " + misses);
        */

        foreach (int[] forrest in Forrests)
        {
            int x_posF = forrest[0];
            int y_posF = forrest[1];
            int minF = forrest[2];
            int maxF = forrest[3];

            for (int i = 0; i < 10; i++)
            {
                int x = Random.Range(minF, maxF) + y_posF;
                int z = Random.Range(minF, maxF) + x_posF;

                if (CheckIfPlacableTile(x,z,map))
                {
                    Vector3 Pos = new Vector3(150 - x + offset, 0, 150 - z + offset);
                    Instantiate(Tree, Pos, Quaternion.identity);
                    TreePos.Add(Pos);
                }
            }
        }
    }

    bool CheckIfPlacableTile(int x, int z, TDMapII map)
    {
        if (map.getTile(x, z) == 1 || map.getTile(x, z) == 2 || map.getTile(x, z) == 3)
            return true;
        
        return false;
    }

    bool HousePossible(int x, int z, TDMapII map)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!CheckIfPlacableTile(x + i, z + i, map))
                return false;
            if (!CheckIfPlacableTile(x - i, z + i, map))
                return false;
            if (!CheckIfPlacableTile(x + i, z - i, map))
                return false;
            if (!CheckIfPlacableTile(x - i, z - i, map))
                return false;


            if (!CheckIfPlacableTile(x, z + i, map))
                return false;
            if (!CheckIfPlacableTile(x, z - i, map))
                return false;
            if (!CheckIfPlacableTile(x + i, z, map))
                return false;
            if (!CheckIfPlacableTile(x - i, z, map))
                return false;

        }

        return true;
    }

    bool ObjPossible(List<Vector3> T, Vector3 place, int dist)
    {
        if (!T.Contains(place))
        {
            if (T.Count == 0)
            {
                return true;
            }
            else if (T.Count >= 1)
            {
                for (int i = 0; i < T.Count; i++)
                {
                    if (Vector3.Distance(place, T[i]) < dist)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }

    /*
    void PlaceHouses()
    {
        while (HousePos.Count < nrHouses)
        {
            Vector3 place = getRandPos(2, 3);
            addHousePos(HousePos, place, 5);

        }

        for (int i = 0; i < HousePos.Count; i++)
        {
            GameObject temp = (GameObject)Instantiate(House, HousePos[i], Quaternion.identity);
            temp.transform.Rotate(new Vector3(90, Random.Range(0, 360), 0));
            //placeWalls(HousePos[i], i);
        }
    }

    void PlaceTrees(TDMapII map)
    {
        
    }

    Vector3 getRandPos(int dist, int offset)
    {
        float x = Mathf.Round(Random.Range(0, offset));
        float z = Mathf.Round(Random.Range(dist, offset));
            
        Vector3 place = new Vector3(x, 0, z);
        return place;
    }

    
    
    void placeWalls(Vector3 place, int j)
    {
        bool walls = true;
        for (int i = 0; i < HousePos.Count; i++)
        {
            if ((i != j) && (Vector3.Distance(place, HousePos[i]) < 30))
            {
                walls = false;
            }
        }
        if (walls)
        {
            GameObject NorthWall = (GameObject)Instantiate(Wall, place + new Vector3(5, 0.5f, -1.5f), Quaternion.identity);
            GameObject EastWall = (GameObject)Instantiate(Wall, place + new Vector3(-2.5f, 0.5f, 5), Quaternion.identity);
            GameObject SouthWall = (GameObject)Instantiate(Wall, place + new Vector3(-10, 0.5f, -2.5f), Quaternion.identity);
            GameObject WestWall = (GameObject)Instantiate(Wall, place + new Vector3(0, 0.5f, -10), Quaternion.identity);
            NorthWall.transform.Rotate(new Vector3(0, 90, 0));
            NorthWall.transform.localScale = new Vector3(13, 1, 0.5f);
            EastWall.transform.Rotate(new Vector3(0, 0, 0));
            EastWall.transform.localScale = new Vector3(15, 1, 0.5f);
            SouthWall.transform.Rotate(new Vector3(0, 90, 0));
            SouthWall.transform.localScale = new Vector3(15, 1, 0.5f);
            WestWall.transform.Rotate(new Vector3(0, 0, 0));

        }
    }*/
}
