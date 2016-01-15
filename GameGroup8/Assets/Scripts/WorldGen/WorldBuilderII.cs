using UnityEngine;
using System.Collections.Generic;

public class WorldBuilderII : MonoBehaviour {

    private int map_x = 300;
    private int map_z = 300;

    public Texture2D World_texture;
    private int tileRes = 16;
    private int[][] TileMap;

    public GameObject Tree;
    public GameObject PineTree;
    public int nrTrees;
    public GameObject C_Building;
    public GameObject House;
    public int nrHouses;
    public GameObject Wall;

    private List<Vector3> TreePos = new List<Vector3>();
    private List<Vector3[]> HouseInfo = new List<Vector3[]>();
    private List<Vector3> HousePos = new List<Vector3>(2);
    private List<Vector3> WallsPos = new List<Vector3>();
    private Vector3 BasePos;
    private float offset = 0.5f;

    private int nrHotels;

    /// <summary>
    /// Creates a new map with textures and objects
    /// </summary>
    public void FirstLoad()
    {
        TDMapII map = new TDMapII(map_x, map_z);

        LoadMapTiles(map);
        BuildTexture();
        Debug.Log("texture build");
        ApplyAssets(map);
        Debug.Log("assets placed");
    }

    /// <summary>
    /// Creates a map from saved data. Only works if this class has data
    /// </summary>
    public void SecondLoad()
    {
        BuildTexture();
        ReplaceAssets();
    }

    /// <summary>
    /// Reads the given public texture and turns it into tiles. These tiles are colours stored at an given index
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Creates the texture for the map, only works if a tilemap is imported
    /// </summary>
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

    /// <summary>
    /// Places assets, such as houses and trees, in the world. 
    /// It does not let objects collide with eachother during the build
    /// </summary>
    /// <param name="map"></param>
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

                Vector3 Pos = new Vector3(150 - x + offset, 5f, 150 - z + offset);

                if (HousePossible(z, x, map, 3) && ObjPossible(HousePos, Pos, 5))
                {
                    Vector3 Rot = new Vector3(-90, Random.Range(0, 2) * -90, 0);
                    GameObject huisje = (GameObject)Instantiate(C_Building, Pos, Quaternion.identity);
                    huisje.transform.Rotate(Rot);
                    HousePos.Add(Pos);
                    HouseInfo.Add(new Vector3[2] { Pos, Rot });
                }
            }
        }

        nrHotels = HousePos.Count;

        float misses = 30;
        while (HousePos.Count <= nrHouses)
        {
            if (misses <= 0)
                break;

            int x = Random.Range(30, 270);
            int z = Random.Range(30, 270);

            Vector3 Pos = new Vector3(150 - x + offset, 0.8f, 150 - z + offset);

            if (HousePossible(z, x, map, 7) && ObjPossible(HousePos, Pos, 30))
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
        //Debug.Log("misses left(house): " + misses + "\n nr houses: " + HousePos.Count);

        foreach (int[] forrest in Forrests)
        {
            int x_posF = forrest[0];
            int y_posF = forrest[1];
            int minF = forrest[2];
            int maxF = forrest[3];

            misses = 10;
            for (int i = 0; i < maxF*2; i++)
            {
                if (misses <= 0)
                    break;

                int x = Random.Range(minF, maxF) + y_posF;
                int z = Random.Range(minF, maxF) + x_posF;

                Vector3 Pos = new Vector3(150 - x + offset, 0, 150 - z + offset);

                if (CheckIfPlacableTile(z, x, map) && ObjPossible(TreePos, Pos, 2) && ObjPossible(HousePos, Pos, 6) && Vector3.Distance(Pos,BasePos) > 12)
                {
                    float nr = Random.Range(0f, 1f);
                    if(nr >= .4)
                    {
                        Instantiate(Tree, Pos, Quaternion.identity);
                    } else if (nr < .4)
                    {
                        Pos.y = 0.8f;
                        Instantiate(PineTree, Pos, new Quaternion(-0.7071f, 0, 0, 0.7071f)); //PineTree
                    }
                    
                    TreePos.Add(Pos);
                }
                else
                {
                    misses--;
                }
            }
        }

        misses = 25;
        while (TreePos.Count <= nrTrees)
        {
            if (misses <= 0)
                break;

            int x = Random.Range(30, 270);
            int z = Random.Range(30, 270);

            Vector3 Pos = new Vector3(150 - x + offset, 0, 150 - z + offset);

            if (CheckIfPlacableTile(z, x, map) && ObjPossible(TreePos, Pos, 2) && ObjPossible(HousePos, Pos, 6) && Vector3.Distance(Pos, BasePos) > 12)
            {
                float nr = Random.Range(0f, 1f);
                if (nr >= .4)
                {
                    Instantiate(Tree, Pos, Quaternion.identity);
                }
                else if (nr < .4)
                {
                    Pos.y = 0.8f;
                    Instantiate(PineTree, Pos, new Quaternion(-0.7071f, 0, 0, 0.7071f)); //PineTree
                }

                TreePos.Add(Pos);
            }
            else
            {
                misses--;
            }
        }

        
        for (int i = nrHotels; i < HousePos.Count; i++)
        {
            Vector3 Pos = HousePos[i];

            int x = (int)(150 - Pos.z - offset);
            int z = (int)(150 - Pos.x - offset);

            if (HousePossible(z, x, map, 15) && ObjPossible(TreePos, Pos, 10))
            {
                placeWalls(Pos);
                WallsPos.Add(Pos);
            }
        }
    }

    /// <summary>
    /// Places the assets in a world that already excisted.
    /// It does not generate new locations, olny replaces the assets
    /// </summary>
    public void ReplaceAssets()
    {
        for (int i = 0; i < nrHotels; i++)
        {
            Vector3 Pos = HouseInfo[i][0];
            Vector3 Rot = HouseInfo[i][1];

            GameObject huisje = (GameObject)Instantiate(C_Building, Pos, Quaternion.identity);
            huisje.transform.Rotate(Rot);
        }

        for (int i = nrHotels; i < HouseInfo.Count; i++)
        {
            Vector3 Pos = HouseInfo[i][0];
            Vector3 Rot = HouseInfo[i][1];

            GameObject huisje = (GameObject)Instantiate(House, Pos, Quaternion.identity);
            huisje.transform.Rotate(Rot);

            if (HouseInfo[i].Length != 2 && HouseInfo[i][2] == new Vector3 (1,1,1))
                placeWalls(Pos);
        }

        foreach (Vector3 Pos in TreePos)
        {
            if (Pos.y == 0)
                Instantiate(Tree, Pos, Quaternion.identity);
            else if (Pos.y != 0)
                Instantiate(PineTree, Pos, new Quaternion(-0.7071f, 0, 0, 0.7071f)); //PineTree
        }

        foreach (Vector3 Pos in WallsPos)
        {
            placeWalls(Pos);
        }
        
    }

    /// <summary>
    /// Places the base during the build of the map. Only used for a new map
    /// </summary>
    private void PlaceBase()
    {
        GameObject Base = GameObject.FindGameObjectWithTag("BASE");
        Base.transform.position = BasePos;
    }

    /// <summary>
    /// Checks if a generated location is an land tile, so objects do not spawn on roads
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    bool CheckIfPlacableTile(int x, int z, TDMapII map)
    {
        if (map.getTile(x, z) == 1 || map.getTile(x, z) == 2 || map.getTile(x, z) == 3)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks if a generated point is a given distance to the road or any other tile than grass
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="map"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Checks if a generated object is not overlapping any other object in the given list with the given distance
    /// </summary>
    /// <param name="T"></param>
    /// <param name="place"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Loads a TDMap into a tilemap used in this class
    /// </summary>
    /// <param name="Tiles"></param>
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

    /// <summary>
    /// Returns the list of locations of all the trees
    /// </summary>
    /// <returns></returns>
    public List<Vector3> getTrees()
    {
        return TreePos;
    }

    /// <summary>
    /// Sets the list of locations of the trees
    /// </summary>
    /// <param name="TreeList"></param>
    public void setTrees(List<Vector3> TreeList)
    {
        TreePos = TreeList;
    }

    /// <summary>
    /// Returns the list with all the info of the houses and city
    /// </summary>
    /// <returns></returns>
    public List<Vector3[]> getHouses()
    {
        return HouseInfo;
    }

    /// <summary>
    /// Set all the info of houses into this class
    /// </summary>
    /// <param name="HouseInfo"></param>
    public void setHouses(List<Vector3[]> HouseInfo)
    {
        this.HouseInfo = HouseInfo;
    }

    /// <summary>
    /// Returns the number of houses in the city, hotels
    /// </summary>
    /// <returns></returns>
    public int get_nrHotels()
    {
        return nrHotels;
    }

    /// <summary>
    /// Sets the number of houses in the city
    /// </summary>
    /// <param name="nr"></param>
    public void set_nrHotels(int nr)
    {
        nrHotels = nr;
    }

    /// <summary>
    /// Return the loaded Tilemap from this class, usefull so after loading the map does not has to be converted again
    /// </summary>
    /// <returns></returns>
    public int[][] getMap()
    {
        return TileMap;
    }

    /// <summary>
    /// Sets the Tilemap for this class, this is usefull after a load so the map does hot has to be converted again
    /// </summary>
    /// <param name="map"></param>
    public void setMap(int[][] map)
    {
        TileMap = map;
    }
    
    /// <summary>
    /// Places a cool set of walls around houses that are far away from other houses, trees and roads
    /// </summary>
    /// <param name="place"></param>
    void placeWalls(Vector3 place)
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

    /// <summary>
    /// Returns the positions of the 'object' which contains the walls
    /// </summary>
    /// <returns></returns>
    public List<Vector3> getWalls()
    {
        return WallsPos;
    }

    /// <summary>
    /// Sets the positions of walls
    /// </summary>
    /// <param name="WallsPos"></param>
    public void setWalls(List<Vector3> WallsPos)
    {
        this.WallsPos = WallsPos;
    }
}
