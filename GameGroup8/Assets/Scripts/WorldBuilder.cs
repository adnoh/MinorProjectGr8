using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilder : MonoBehaviour {

    private int LvlSize = 30;
    private int nrHouses = 200;
    //private int nrVillages = 2;
    private int nrTrees = 1000;
    //private int nrWalls = 10;
    private int BaseSize = 15;

    private List<Vector3> TreePos = new List<Vector3>();
    private List<Vector3> HousePos = new List<Vector3>();
    private int maxChange;

    public GameObject House;
    public GameObject Tree;
    //public GameObject Wall;
    
	//Initialize world
	void Start () {
        transform.localScale = new Vector3(LvlSize, 1, LvlSize);
        maxChange = LvlSize * 5;
        PlaceHouses();
        PlaceTrees();
	}

    void PlaceHouses()
    {
        while (HousePos.Count < nrHouses)
        {
            Vector3 place = getRandPos();
            if (!HousePos.Contains(place))
            {
                HousePos.Add(place);
            }
        }

        for (int i = 0; i < HousePos.Count; i++)
        {
            GameObject temp = (GameObject)Instantiate(House, HousePos[i], Quaternion.identity);
            temp.transform.Rotate(new Vector3(90, Random.Range(0, 360), 0));
        }
    }

    void PlaceTrees()
    {
        while (TreePos.Count < nrTrees)
        {
            Vector3 place = getRandPos();
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
    
    Vector3 getRandPos()
    {
        float x = Random.Range(0, maxChange);
        float temp = Random.Range(0, 2);
        if (temp < 1)
        {
            x = x * -1;
        }
        float z = 0;
        if (Mathf.Abs(x) <= BaseSize)
        {
            z = Random.Range(BaseSize, maxChange);
            float temp2 = Random.Range(0, 2);
            if (temp2 < 1)
            {
                z = z * -1;
            }
        }
        else if (Mathf.Abs(x) > BaseSize)
        {
            z = Random.Range(0, maxChange);
            float temp2 = Random.Range(0, 2);
            if (temp2 < 1)
            {
                z = z * -1;
            }
        }
        Vector3 place = new Vector3(x, 0, z);
        return place;
    }
}
