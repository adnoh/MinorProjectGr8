using UnityEngine;
using System.Collections.Generic;

public class PSpawner : MonoBehaviour {
	
	public GameObject unit;
	public int amount;
	public int minL, minR, maxL, maxR;
	

	void Start () {
		PlaceCubes ();
	}

	void PlaceCubes(){
		
		List<Vector3> occupied = new List<Vector3> ();
		int x, y, z, random1;
		x = 0;
		y = 0;
		z = 0;

		while (occupied.Count <= amount) {

			random1 = Random.Range (0, 5);

			switch (random1)
			{
			case 1:
				x = Random.Range (minL, maxL);
				y = 1;
				z = Random.Range (minL, maxL);
				break;
			case 2:
				x = Random.Range (minL, maxL);
				y = 1;
				z = Random.Range (minR, maxR);
				break;
			case 3:
				x = Random.Range (minR, maxR);
				y = 1;
				z = Random.Range (minL, maxL);
				break;
			case 4:
				x = Random.Range (minR, maxR);
				y = 1;
				z = Random.Range (minR, maxR);
				break;
			default:
				break;
			}

			Vector3 Location = new Vector3 (x, y, z);


			if (!occupied.Contains (Location)) {
				occupied.Add (Location);
			}
		}

		for (int i = 0; i < amount; i++) {
			Vector3 V = occupied[i];
			Instantiate (unit, V, Quaternion.identity);
		}
	}
}
