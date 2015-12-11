using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TDMapII {

    int[][] tiles;
    int[][] forrests;
    int[] villages;
    int size_x;
    int size_y;

    int baseSize = 5;
    int nrVillages = 1;
    int nrForr = 10;

    public TDMapII(int height, int width)
    {
        this.size_y = height;
        this.size_x = width;

        tiles = new int[size_y][];
        for (int i = 0; i < size_y; i++)
        {
            tiles[i] = new int[width];
        }

        forrests = new int[nrForr][];

        MakeLand();
        int[] BasePos = PlaceBase();
        MakeWater();
        for (int i = 0; i < nrForr; i++)
        {
            MakeForrests(i);
        }

        villages = PlaceVillage(BasePos);

        //makePrimaryRoads(BasePos[0], BasePos[1], villages[0], villages[1]);
        //ConnectRoadGrid(BasePos[0], BasePos[1]);
        ConnectCityGrid(villages[0], villages[1]);
        BroadenRoads(7);
    }

    public int getTile(int x, int y)
    {
        return tiles[y][x];
    }

    public void setTile(int x, int y, int value)
    {
        tiles[y][x] = value;
    }

    public int[][] getForrests()
    {
        return forrests;
    }

    public int[] getVillages()
    {
        return villages;
    }

    void MakeLand()
    {
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                float texture = Random.Range(0f, 10f);
                if (texture <= 5)
                {
                    tiles[y][x] = 1;
                }
                else if (texture > 5 && texture <= 7.5)
                {
                    tiles[y][x] = 2;
                }
                else if (texture >= 7.5)
                {
                    tiles[y][x] = 3;
                }
            }
        }
    }

    int[] PlaceBase()
    {
        int x_pos = Random.Range(30, size_x-30);
        int y_pos = Random.Range(30, size_y-30);

        for (int y = 0; y < baseSize; y++)
        {
            for (int x = 0; x < baseSize; x++)
            {
                tiles[y + y_pos][x + x_pos] = 4;
            }
        }

        return new int[2] { x_pos, y_pos };
    }

    void MakeWater()
    {
        int bottom_offset = 2;
        for (int y = 0; y < size_y; y++)
        {
            bottom_offset += Random.Range(-1, 2);
            if (bottom_offset < 2)
                bottom_offset = 2;
            if (bottom_offset > 10)
                bottom_offset = 9;

            tiles[y][size_x - bottom_offset - 1] = 11;
            tiles[y][size_x - bottom_offset - 2] = 11;
            for (int x = size_x - bottom_offset; x < size_x; x++)
            {
                tiles[y][x] = 0;
            }
        }

        int top_offset = 2;
        for (int y = 0; y < size_y; y++)
        {
            top_offset += Random.Range(-1, 2);
            if (top_offset < 2)
                top_offset = 2;
            if (top_offset > 10)
                top_offset = 9;

            tiles[y][top_offset + 1] = 11;
            tiles[y][top_offset] = 11;
            for (int x = 0; x < top_offset; x++)
            {
                tiles[y][x] = 0;
            }
        }

        int right_offset = 2;
        for (int x = 0; x < size_x; x++)
        {
            right_offset += Random.Range(-1, 2);
            if (right_offset < 2)
                right_offset = 2;
            if (right_offset > 10)
                right_offset = 9;

            if (tiles[right_offset + 1][x] != 0)
                tiles[right_offset + 1][x] = 11;
            if (tiles[right_offset][x] != 0)
                tiles[right_offset][x] = 11;
            for (int y = 0; y < right_offset; y++)
            {
                tiles[y][x] = 0;
            }
        }

        int left_offset = 2;
        for (int x = 0; x < size_x; x++)
        {
            left_offset += Random.Range(-1, 2);
            if (left_offset < 2)
                left_offset = 2;
            if (left_offset > 10)
                left_offset = 9;

            if (tiles[size_y - left_offset - 1][x] != 0)
                tiles[size_y - left_offset - 1][x] = 11;
            if (tiles[size_y - left_offset - 2][x] != 0)
                tiles[size_y - left_offset - 2][x] = 11;
            for (int y = size_y - left_offset; y < size_y; y++)
            {
                tiles[y][x] = 0;
            }
        }
    }

    void MakeForrests(int i)
    {
        int x_pos = Random.Range(25,275);
        int y_pos = Random.Range(25,275);

        int max = Random.Range(12, 25);
        int min = -1 * max;
        /*
        for (int y = min; y < max; y++)
        {
            for (int x = min; x < max; x++)
            {
                if((tiles[y + y_pos][x + x_pos] != 0) && (tiles[y + y_pos][x + x_pos] != 5))
                    tiles[y + y_pos][x + x_pos] = 3;
            }
        }
        */
        forrests[i] = new int[4] { x_pos,y_pos,min,max };
    }

    int[] PlaceVillage(int [] BasePos)
    {
        int x_pos = 75;
        int y_pos = 75;

        if (BasePos[0] < 150)
            x_pos = 225;

        if (BasePos[1] < 150)
            y_pos = 225;

        int max = 60;
        int min = -60;
        
       return new int[4] { x_pos, y_pos, min, max };
    }

    List<Vector2> MakeRoadGrid(int xB, int yB)
    {
        List<Vector2> junctions = new List<Vector2>();
        Vector2 BasePos = new Vector2(xB, yB);

        int misses = 10;
        while (junctions.Count != 4)
        {
            Vector2 point_offset = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
            Vector2 point = point_offset + BasePos;
            if (Vector2.Distance(BasePos, point) > 20 && checkDistance(point, junctions, 30))
            {
                junctions.Add(point);
            }
            else
            {
                misses--;
            }
            if (misses <= 0)
                break;
        }

        misses = 10;
        while (junctions.Count != 9)
        {
            Vector2 point_offset = new Vector2(Random.Range(-130, 130), Random.Range(-130, 130));
            Vector2 point = point_offset + BasePos;
            if (Vector2.Distance(BasePos, point) > 80 && checkDistance(point, junctions , 30)) 
            {
                junctions.Add(point);
            }
            else
            {
                misses--;
            }
            if (misses <= 0)
                break;
        }

        return junctions;
    }

    List<Vector2> MakeCityGrid(int xB, int yB)
    {
        List<Vector2> junctions = new List<Vector2>();
        Vector2 BasePos = new Vector2(xB, yB);

        int misses = 10;
        while (junctions.Count != 4)
        {
            Vector2 point_offset = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
            Vector2 point = point_offset + BasePos;
            if (Vector2.Distance(BasePos, point) > 10 && checkDistance(point, junctions, 20))
            {
                junctions.Add(point);
            }
            else
            {
                misses--;
            }
            if (misses <= 0)
                break;
        }

        misses = 10;
        while (junctions.Count != 9)
        {
            Vector2 point_offset = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
            Vector2 point = point_offset + BasePos;
            if (Vector2.Distance(BasePos, point) > 30 && checkDistance(point, junctions, 20))
            {
                junctions.Add(point);
            }
            else
            {
                misses--;
            }
            if (misses <= 0)
                break;
        }

        return junctions;
    }

    bool checkDistance(Vector2 point, List<Vector2> junctions, int dist)
    {
        if (junctions.Count != 0)
        {
            foreach (Vector2 point2 in junctions)
            {
                if (Vector2.Distance(point, point2) < dist)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    void makePrimaryRoads(int x1, int y1, int x2, int y2)
    {
        int rand = 0;
        while (x1 != x2)
        {
            tiles[y1][x1] = 7;

            switch (rand){
                case 0:
                case -1:
                case 1:
                    x1 += x1 < x2 ? 1 : -1;
                    rand += Random.Range(-1, 2);
                    break;
                case 2:
                    y1++;
                    rand += Random.Range(-2, 1);
                    break;
                case -2:
                    y1--;
                    rand += Random.Range(0, 3);
                    break;
            }
            
        }

        rand = 0;
        while (y1 != y2)
        {
            tiles[y1][x1] = 7;

            switch (rand)
            {
                case 0:
                case -1:
                case 1:
                    y1 += y1 < y2 ? 1 : -1;
                    rand += Random.Range(-1, 2);
                    break;
                case 2:
                    x1++;
                    rand += Random.Range(-2, 1);
                    break;
                case -2:
                    x1--;
                    rand += Random.Range(0, 3);
                    break;
            }
        }

        while (x1 != x2)
        {
            tiles[y1][x1] = 7;
            x1 += x1 < x2 ? 1 : -1;
        }

    }

    void makeVillageRoads(int x1, int y1, int x2, int y2)
    {
        while(x1 != x2 || y1 != y2)
        {
            int diff = Mathf.Abs(x1 - x2) - Mathf.Abs(y1 - y2);
            if (x1 != x2 && diff >= 0)
            {
                tiles[y1][x1] = 7;
                x1 += x1 < x2 ? 1 : -1;
            }

            if (y1 != y2 && diff < 0)
            {
                tiles[y1][x1] = 7;
                y1 += y1 < y2 ? 1 : -1;
            }
        }
    }

    void ConnectRoadGrid(int xB, int yB)
    {
        List<Vector2> con_Points = MakeRoadGrid(xB, yB);

        for (int i = 0; i < con_Points.Count -1; i++)
        {
            Vector2 point1 = con_Points[i];
            Vector2 point2 = con_Points[i + 1];
            makePrimaryRoads((int)point1.x,(int)point1.y,(int)point2.x,(int)point2.y);
        }
    }

    void ConnectCityGrid(int xB, int yB)
    {
        List<Vector2> con_Points = MakeCityGrid(xB, yB);
        List<Vector2> inner = new List<Vector2>();
        List<Vector2> outer = new List<Vector2>();
        Vector2 BasePos = new Vector2(xB, yB);

        for (int i = 0; i < con_Points.Count - 1; i++)
        {
            Vector2 point = con_Points[i];
            if (Vector2.Distance(BasePos, point) < 20)
                inner.Add(point);
            else if (Vector2.Distance(BasePos, point) > 20)
                outer.Add(point);
        }

        for (int i = 0; i < inner.Count - 1; i++)
        {
            Vector2 point1 = inner[i];
            Vector2 point2 = inner[i + 1];
            makeVillageRoads((int)point1.x, (int)point1.y, (int)point2.x, (int)point2.y);
            Debug.Log(point1 + "inner");
        }
        makeVillageRoads((int)inner[inner.Count - 1].x, (int)inner[inner.Count - 1].y, (int)inner[0].x, (int)inner[0].y);

        for (int i = 0; i < outer.Count - 1; i++)
        {
            Vector2 point1 = outer[i];
            Vector2 point2 = outer[i + 1];
            makeVillageRoads((int)point1.x, (int)point1.y, (int)point2.x, (int)point2.y);
            Debug.Log(point1 + "outer");
        }
        makeVillageRoads((int)outer[outer.Count - 1].x, (int)outer[outer.Count - 1].y, (int)outer[0].x, (int)outer[0].y);
    }

    void BroadenRoads(int tileTex)
    {
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                if ((tiles[j][i] == 1 || tiles[j][i] == 2 || tiles[j][i] == 3) && HasFloorConnect(i, j, tileTex))
                {
                    tiles[j][i] = tileTex + 1;
                }
            }
        }
    }

    private bool HasFloorConnect(int i, int j, int tileTex)
    {
        if (j < size_y - 1 && tiles[j + 1][i] == tileTex)
            return true;
        if (j > 0 && tiles[j - 1][i] == tileTex)
            return true;
        if (i < size_x - 1 && tiles[j][i + 1] == tileTex)
            return true;
        if (i > 0 && tiles[j][i - 1] == tileTex)
            return true;

        if (i > 0 && j > 0 && tiles[j - 1][i - 1] == tileTex)
            return true;
        if (i > 0 && j < size_y - 1 && tiles[j + 1][i - 1] == tileTex)
            return true;
        if (i < size_x - 1 && j > 0 && tiles[j - 1][i + 1] == tileTex)
            return true;
        if (i < size_x - 1 && j < size_y - 1 && tiles[j + 1][i + 1] == tileTex)
            return true;

        return false;
    }
}
