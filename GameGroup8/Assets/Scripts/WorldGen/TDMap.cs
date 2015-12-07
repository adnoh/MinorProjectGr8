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

        for (int i = 0; i < 25; i++)
        {
            int rsx = Random.Range(4, 8);
            int rsy = Random.Range(4, 8);

            Room r = new Room();
            r.x = Random.Range(0, size_x - rsx);
            r.y = Random.Range(0, size_y - rsy);
            r.width = rsx;
            r.height = rsy;

            if (!RoomCollides(r))
            {
                rooms.Add(r);
            }

            foreach(Room r2 in rooms)
            {
                MakeRoom(r2);
            }
        }

    }

    public int getTile(int x, int y)
    {
        return tiles[y][x];
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
