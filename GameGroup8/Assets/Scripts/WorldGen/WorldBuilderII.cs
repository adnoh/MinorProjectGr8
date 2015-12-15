using UnityEngine;
using System.Collections.Generic;

public class WorldBuilderII : MonoBehaviour {

    private int map_x = 300;
    private int map_z = 300;

    public Texture2D World_texture;
    private int tileRes = 16;
    private int[][] TileMap;

    public GameObject Tree;
    public int nrTrees;
    public GameObject House;
    public int nrHouses;

    private List<Vector3> TreePos = new List<Vector3>();
    private List<Vector3[]> HouseInfo = new List<Vector3[]>();
    private List<Vector3> HousePos = new List<Vector3>(2);
    private Vector3 BasePos;
    private float offset = 0.5f;

    // Initialization on play
    public void FirstLoad()
    {
        TDMapII map = new TDMapII(map_x, map_z);

        LoadMapTiles(map);
        BuildTexture();
        Debug.Log("texture build");
        ApplyAssets(map);
        Debug.Log("assets placed");
    }

    public void SecondLoad()
    {
        BuildTexture();
        ReplaceAssets();
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

    public void BuildTexture()
    {
        int texWidth = tileRes * map_x;
        int texHeight = tileRes * map_z;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        Color[][] textures = LoadTexture();

        for (int i = 0; i < map_z; i++)
        {
            for (int j = 0; j < map_x; j++)
            {
                Color[] c = textures[TileMap[i][j]];
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
        int[] base_map = map.getBasePosition();
        BasePos = new Vector3(150 - base_map[1], 0, 150 - base_map[0]);
        PlaceBase();

        int[] Village = map.getVillages();
        int[][] Forrests = map.getForrests();

        int x_pos = Village[0];
        int y_pos = Village[1];

        for (int i = 0; i < 8; i++)
        {
            if (HousePos.Count == 60)
                break;

            for (int j = 0; j < 8; j++)
            {
                if (HousePos.Count == 60)
                    break;

                int x = y_pos - 35 + 10 * i;
                int z = x_pos - 35 + 10 * j;

                Vector3 Pos = new Vector3(150 - x + offset, 0.8f, 150 - z + offset);

                if (HousePossible(z, x, map, 3) && ObjPossible(HousePos, Pos, 5))
                {
                    Vector3 Rot = new Vector3(90, Random.Range(0, 2) * 90, 0);
                    GameObject huisje = (GameObject)Instantiate(House, Pos, Quaternion.identity);
                    huisje.transform.Rotate(Rot);
                    HousePos.Add(Pos);
                    HouseInfo.Add(new Vector3[2] { Pos, Rot });
                }
            }
        }

        float misses = 15;
        while (HousePos.Count <= nrHouses)
        {
            if (misses <= 0)
                break;

            int x = Random.Range(30, 270);
            int z = Random.Range(30, 270);

            Vector3 Pos = new Vector3(150 - x + offset, 0.8f, 150 - z + offset);

            if (HousePossible(z, x, map, 6) && ObjPossible(HousePos, Pos, 30))
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
        //Debug.Log("misses left(house): " + misses);


        foreach (int[] forrest in Forrests)
        {
            int x_posF = forrest[0];
            int y_posF = forrest[1];
            int minF = forrest[2];
            int maxF = forrest[3];

            for (int i = 0; i < maxF*2; i++)
            {
                int x = Random.Range(minF, maxF) + y_posF;
                int z = Random.Range(minF, maxF) + x_posF;

                Vector3 Pos = new Vector3(150 - x + offset, 0, 150 - z + offset);

                if (CheckIfPlacableTile(z, x, map) && ObjPossible(TreePos, Pos, 2) && ObjPossible(HousePos, Pos, 6))
                {
                    Instantiate(Tree, Pos, Quaternion.identity);
                    TreePos.Add(Pos);
                }
            }
        }

        misses = 15;
        while (TreePos.Count <= nrTrees)
        {
            if (misses <= 0)
                break;

            int x = Random.Range(30, 270);
            int z = Random.Range(30, 270);

            Vector3 Pos = new Vector3(150 - x + offset, 0, 150 - z + offset);

            if (CheckIfPlacableTile(z, x, map) && ObjPossible(TreePos, Pos, 2) && ObjPossible(HousePos, Pos, 6))
            {
                Instantiate(Tree, Pos, Quaternion.identity);
                TreePos.Add(Pos);
            }
            else
            {
                misses--;
            }
        }
    }

    public void ReplaceAssets()
    {
        foreach (Vector3[] Info in HouseInfo)
        {
            Vector3 Pos = Info[0];
            Vector3 Rot = Info[1];

            GameObject huisje = (GameObject)Instantiate(House, Pos, Quaternion.identity);
            huisje.transform.Rotate(Rot);
        }

        foreach (Vector3 Pos in TreePos)
        {
            Instantiate(Tree, Pos, Quaternion.identity);
        }
    }

    private void PlaceBase()
    {
        GameObject Base = GameObject.FindGameObjectWithTag("BASE");
        Base.transform.position = BasePos;
    }

    bool CheckIfPlacableTile(int x, int z, TDMapII map)
    {
        if (map.getTile(x, z) == 1 || map.getTile(x, z) == 2 || map.getTile(x, z) == 3)
            return true;
        else
            return false;
    }

    bool HousePossible(int x, int z, TDMapII map, int dist)
    {
        for (int i = 0; i < dist; i++)
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

    public void LoadMapTiles(TDMapII Tiles)
    {
        TileMap = new int[300][];

        for (int i = 0; i < 300; i++)
        {
            TileMap[i] = new int[300];
        }

        for (int i = 0; i < 300; i++)
        {
            for (int j = 0; j < 300; j++)
            {
                TileMap[i][j] = Tiles.getTile(i, j);
            }
        }
    }

    public List<Vector3> getTrees()
    {
        return TreePos;
    }

    public void setTrees(List<Vector3> TreeList)
    {
        TreePos = TreeList;
    }

    public List<Vector3[]> getHouses()
    {
        return HouseInfo;
    }

    public void setHouses(List<Vector3[]> HouseInfo)
    {
        this.HouseInfo = HouseInfo;
    }

    public int[][] getMap()
    {
        return TileMap;
    }

    public void setMap(int[][] map)
    {
        TileMap = map;
    }

    /*
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
