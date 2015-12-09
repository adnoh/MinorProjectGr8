using UnityEngine;
using System.Collections;

public class TDMapII {

    int[][] tiles;
    int[][] forrests;
    int[][] villages;
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
        villages = new int[nrVillages][];

        MakeLand();
        int[] BasePos = PlaceBase();
        MakeWater();
        for (int i = 0; i < nrForr; i++)
        {
            MakeForrests(i);
        }

        int[] VPos = PlaceVillages(0);
        
        makePrimaryRoads(BasePos[0], BasePos[1], VPos[0], VPos[1]);
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

    public int[][] getVillages()
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

        tiles[3][6] = 5;
        Debug.Log("tiles[3][6] = white;");
        tiles[6][3] = 4;
        Debug.Log("tiles[6][3] = black");
    }

    int[] PlaceBase()
    {
        int x_pos = (int)Mathf.Floor(size_x / 2) + Random.Range(-10, 10);
        int y_pos = (int)Mathf.Floor(size_y / 2) + Random.Range(-10, 10);

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

    int[] PlaceVillages(int i)
    {
        int x_pos = Random.Range(50, 250);
        int y_pos = Random.Range(50, 250);

        int max = Random.Range(12, 25);
        int min = -1 * max;

        int maxMarket = (int)Mathf.Floor(max/2);

        for (int y = 0; y < maxMarket; y++)
        {
            for (int x = 0; x < maxMarket; x++)
            {
                tiles[y + y_pos][x + x_pos] = 10;
            }
        }

        villages[i] = new int[4] { x_pos, y_pos, min, max };

        return new int[2] { x_pos, y_pos };
    }

    void makePrimaryRoads(int xPosB, int yPosB, int xPosV, int yPosV)
    {
        int y1 = yPosB + 1;
        int x1 = xPosB + 5;

        int x2 = xPosV;
        int y2 = yPosV;

        while (x1 != x2)
        {
            tiles[y1][x1] = 7;
            x1 += x1 < x2 ? 1 : -1;
        }

        while (y1 != y2)
        {
            tiles[y1][x1] = 7;
            y1 += y1 < y2 ? 1 : -1;
        }

    }
}
