using UnityEngine;
using System.Collections.Generic;

public class WorldBuilder : MonoBehaviour {

    public int LvlSize;
    public int nrHouses;
    public int nrVillages;
    public int nrTrees;
    //public int nrWalls = 10;
    private int BaseSize = 15;

    private List<Vector3> TreePos = new List<Vector3>();
    private List<Vector3> HousePos = new List<Vector3>();
    private List<Vector3> BigPos = new List<Vector3>();
    private int maxChange;

    public GameObject House;
    public GameObject Tree;
    public GameObject Wall;
    
	//Initialize world
	public void StartWorldBuilder() {
        transform.localScale = new Vector3(LvlSize, 1, LvlSize);
        maxChange = LvlSize * 5;
        PlaceHouses();
        PlaceTrees();
    }

    void PlaceHouses()
    {
        int countVillages = 0;
        int minVillageSize = (int)Mathf.Round(nrHouses / (nrVillages * 2));
        int maxVillageSize = (int)Mathf.Round(nrHouses / (nrVillages + 1));

        while (countVillages < nrVillages)
        {
            Vector3 marketPlace = getRandBigPos();
            int villageSize = (int)Mathf.Round(Random.Range(minVillageSize, maxVillageSize));
            int countHouses = HousePos.Count + villageSize;
            while (HousePos.Count < countHouses)
            {
                Vector3 place = getRandPos(5, (int)Mathf.Round((villageSize/1.5f))) + marketPlace;
                addHousePos(HousePos, place, 5);
            }
            countVillages++;
        }

        while (HousePos.Count < nrHouses)
        {
            Vector3 place = getRandPos(BaseSize, maxChange);
            addHousePos(HousePos, place, 5);
            
        }

        for (int i = 0; i < HousePos.Count; i++)
        {
            GameObject temp = (GameObject)Instantiate(House, HousePos[i], Quaternion.identity);
            temp.transform.Rotate(new Vector3(90, Random.Range(0, 360), 0));
            placeWalls(HousePos[i],i);
        }
    }

    void PlaceTrees()
    {
        while (TreePos.Count < nrTrees)
        {
            Vector3 place = getRandPos(BaseSize, maxChange);
            if (!TreePos.Contains(place)&&(!HousePos.Contains(place)))
            {
                TreePos.Add(place);
            }
        }

        for (int i = 0; i < TreePos.Count; i++)
        {
            Instantiate(Tree, TreePos[i], Quaternion.identity);
        }
    }
    
    Vector3 getRandPos(int dist, int offset)
    {
        float x = Mathf.Round(Random.Range(0, offset));
        float temp = Random.Range(0, 2);
        if (temp < 1)
        {
            x = x * -1;
        }
        float z = 0;
        if (Mathf.Abs(x) <= dist)
        {
            z = Mathf.Round(Random.Range(dist, offset));
            float temp2 = Random.Range(0, 2);
            if (temp2 < 1)
            {
                z = z * -1;
            }
        }
        else if (Mathf.Abs(x) > dist)
        {
            z = Random.Range(0, offset);
            float temp2 = Random.Range(0, 2);
            if (temp2 < 1)
            {
                z = z * -1;
            }
        }
        Vector3 place = new Vector3(x, 0, z);
        return place;
    }

    Vector3 getRandBigPos()
    {
        bool add = false;
        Vector3 value = new Vector3(0, 0, 0);
        while (!add)
        {
            value = getRandPos(30, maxChange / 2);
            if (BigPos.Count == 0)
            { 
                BigPos.Add(value);
                break;
            }
            else if (BigPos.Count >= 1)
            {
                for (int i = 0; i < BigPos.Count; i++)
                {
                    if (Vector3.Distance(value, BigPos[i]) > 50)
                    {
                        add = true;
                    }
                }
                if (!add)
                {
                    BigPos.Add(value);
                }
            }
        }
        return value;
    }

    void addHousePos(List<Vector3> T, Vector3 place, int dist)
    {
        if (!T.Contains(place))
        {
            if (T.Count == 0)
            {
                T.Add(place);
            }
            else if (T.Count >= 1)
            {
                bool add = true;
                for (int i = 0; i < T.Count; i++)
                {
                    if (Vector3.Distance(place, T[i]) < dist)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    T.Add(place);
                    
                }
            }
        }
    }

    void placeWalls(Vector3 place, int j)
    {
        bool walls = true;
        for (int i = 0; i < HousePos.Count; i++)
        {
            if ((i!=j)&&(Vector3.Distance(place, HousePos[i]) < 30))
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
    }
}
