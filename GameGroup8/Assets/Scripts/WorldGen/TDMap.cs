using System.Collections.Generic;
using UnityEngine;

public class TDMap{

    int[][] tiles;
    int size_x;
    int size_y;

    List<Room> rooms;

    public TDMap(int height, int width)
    {
        this.size_y = height;
        this.size_x = width;

        tiles = new int[size_y][];
        for (int i = 0; i < size_y; i++)
        {
            tiles[i] = new int[width];
        }

        rooms = new List<Room>();
        int maxFails = 10;

        while (rooms.Count < 10)
        {
            int rsx = Random.Range(4, 10);
            int rsy = Random.Range(4, 10);

            Room r = new Room();
            r.x = Random.Range(0, size_x - rsx);
            r.y = Random.Range(0, size_y - rsy);
            r.width = rsx;
            r.height = rsy;

            if (!RoomCollides(r))
            {
                rooms.Add(r);
            }
            else
            {
                maxFails--;
                if (maxFails <= 0)
                    break;
            }

        }

        foreach(Room r2 in rooms)
        {
            MakeRoom(r2);
        }

        MakeCorridoir(rooms[0], rooms[2]);
        MakeWalls();
    }

    public int getTile(int x, int y)
    {
        return tiles[y][x];
    }

    void MakeCorridoir(Room r1, Room r2)
    {
        int x1 = r1.center_x;
        int y1 = r1.center_y;
        int x2 = r2.center_x;
        int y2 = r2.center_y;

        while (x1 != x2)
        {
            tiles[y1][x1] = 1;
            x1 += x1 < x2 ? 1 : -1;
        }

        while (y1 != y2)
        {
            tiles[y1][x1] = 1;
            y1 += y1 < y2 ? 1 : -1;
        }
    }

    void MakeWalls()
    {
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                if (tiles[j][i] == 0 && HasFloorConnect(i, j))
                {
                    tiles[j][i] = 2;
                }
            }
        }
    }

    private bool HasFloorConnect(int i, int j)
    {
        if ( j < size_y-1 && tiles[j + 1][i] == 1)
            return true;
        if ( j > 0 && tiles[j - 1][i] == 1)
            return true;
        if ( i < size_x-1 && tiles[j][i + 1] == 1)
            return true;
        if ( i > 0 && tiles[j][i - 1] == 1)
            return true;

        if (i > 0 && j > 0 && tiles[j - 1][i - 1] == 1)
            return true;
        if (i > 0 && j < size_y-1 && tiles[j + 1][i - 1] == 1)
            return true;
        if (i < size_x-1 && j > 0 && tiles[j - 1][i + 1] == 1)
            return true;
        if (i < size_x-1 && j < size_y-1 && tiles[j + 1][i + 1] == 1)
            return true;

        return false;
    }

    void MakeRoom(Room r)
    {
        for (int i = 0; i < r.height; i++)
        {
            for (int j = 0; j < r.width; j++)
            {
                if (i == 0 || i == r.height-1 || j == 0 || j == r.width-1)
                {
                    tiles[r.y+i][r.x+j] = 2;
                }
                else
                {
                    tiles[r.y+i][r.x+j] = 1;
                }
            }
        }
    }

    bool RoomCollides(Room r)
    {
        foreach(Room r2 in rooms)
        {
            if (r.CollidesWithRoom(r2))
            {
                return true;
            }
        }

        return false;
    }

    protected class Room
    {
        public int x;
        public int y;
        public int height;
        public int width;
        
        public int top
        {
            get { return y + height - 1; }
        }

        public int right
        {
            get { return x + width - 1; }
        }

        public int center_x
        {
            get { return Mathf.FloorToInt(width/2) + x; }
        }

        public int center_y
        {
            get { return Mathf.FloorToInt(height/2) + y; }
        }

        public bool CollidesWithRoom(Room other)
        {
            if (x > other.right-1)
                return false;

            if (right < other.x+1)
                return false;

            if (y > other.top-1)
                return false;

            if (top < other.y+1)
                return false;

            return true;
        }
    }
}
