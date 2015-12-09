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
        PlaceBase();
        MakeWater();
        for (int i = 0; i < nrForr; i++)
        {
            MakeForrests(i);
        }
        for (int i = 0; i < nrVillages; i++)
        {
            PlaceVillages(i);
        }
        
    }

    public int getTile(int x, int y)
    {
        return tiles[y][x];
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
                tiles[y][x] = 1;
            }
        }
    }

    void PlaceBase()
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
            for (int x = 0; x < top_offset - 1; x++)
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
            for (int y = 0; y < right_offset - 1; y++)
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

    void PlaceVillages(int i)
    {
        int x_pos = Random.Range(50, 250);
        int y_pos = Random.Range(50, 250);

        int max = Random.Range(12, 25);
        int min = -1 * max;

        for (int y = min; y < max; y++)
        {
            for (int x = min; x < max; x++)
            {
                tiles[y + y_pos][x + x_pos] = 10;
            }
        }

        villages[i] = new int[4] { x_pos, y_pos, min, max };
    }
}
