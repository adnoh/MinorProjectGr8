using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class for spawning units
/// </summary>
public class PSpawner : MonoBehaviour {
	
	public GameObject unit;
    public GameObject healthUnit;
    public GameObject energyUnit;
    public GameObject fatiqueUnit;
    public GameObject baseUnit;
    public int amount;
	public int minL, minR, maxL, maxR;
	

	void Start () {
		PlaceCubes ();
	}

    /// <summary>
    /// Place an unit at a given location
    /// </summary>
    /// <param name="place"></param>
	public void placeUnit(Vector3 place){
		GameObject unitClone = Instantiate (unit, new Vector3(place.x, 1, place.z), Quaternion.identity) as GameObject;
		unitClone.transform.Rotate (-90, 0, 0);
	}

    public void nextWave()
    {
        for(int i = 0; i < 5; i++)
        {
			GameObject unitClone = Instantiate (unit, getRandomPosition(), Quaternion.identity) as GameObject;
			unitClone.transform.Rotate (-90, 0, 0);
        }
        for (int i = 0; i < 2; i++)
        {
			GameObject healthUnitClone = Instantiate(healthUnit, getRandomPosition(), Quaternion.identity) as GameObject;
            healthUnitClone.transform.Rotate(270, 0, 0);

			GameObject energyUnitClone = Instantiate(energyUnit, getRandomPosition(), Quaternion.identity) as GameObject;
            energyUnitClone.transform.Rotate(270, 0, 0);

			GameObject fatiqueUnitClone = Instantiate (fatiqueUnit, getRandomPosition (), Quaternion.identity) as GameObject;
            fatiqueUnitClone.transform.Rotate(270, 0, 0);

			GameObject baseUnitClone = Instantiate(baseUnit, getRandomPosition(), Quaternion.identity) as GameObject;
            baseUnitClone.transform.Rotate(270, 0, 0);

        }
    }

    /// <summary>
    /// Spawn all the units
    /// </summary>
	void PlaceCubes(){
		
		List<Vector3> occupied = new List<Vector3> ();
		

		while (occupied.Count <= amount) {

            Vector3 Location = getRandomPosition();

			if (!occupied.Contains (Location)) {
				occupied.Add (Location);
			}
		}

		for (int i = 0; i < amount; i++) {
			Vector3 V = occupied[i];
			GameObject unitClone = Instantiate (unit, V, Quaternion.identity) as GameObject;
			unitClone.transform.Rotate (-90, 0, 0);
		}
	}

    public Vector3 getRandomPosition()
    {
        int x, y, z;
        x = 0;
        y = 0;
        z = 0;
        int random1 = Random.Range(1, 5);

        switch (random1)
        {
            case 1:
                x = Random.Range(minL, maxL);
                y = 1;
                z = Random.Range(minL, maxL);
                break;
            case 2:
                x = Random.Range(minL, maxL);
                y = 1;
                z = Random.Range(minR, maxR);
                break;
            case 3:
                x = Random.Range(minR, maxR);
                y = 1;
                z = Random.Range(minL, maxL);
                break;
            case 4:
                x = Random.Range(minR, maxR);
                y = 1;
                z = Random.Range(minR, maxR);
                break;
            default:
                break;
        }

        return new Vector3(x, y, z);


    }
}
