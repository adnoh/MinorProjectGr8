using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Objectspawner : MonoBehaviour {

	public GameObject cube;
	public int amount;
	public int minL, minR, maxL, maxR;

	

	// Use this for initialization
	void Start () {

		PlaceCubes ();

	}
	void PlaceCubes(){

		List<Vector3> occupied = new List<Vector3> ();


		while (occupied.Count <= amount) {
			int x, y, z, random1, random2;
			random1 = UnityEngine.Random.Range (0, 3);
			random2 = UnityEngine.Random.Range (0, 3);
			Debug.Log(random1);
			Debug.Log(random2);
			if (random1 == 1 && random2 == 1) {
				x = UnityEngine.Random.Range (minL, maxL);
				y = 1;
				z = UnityEngine.Random.Range (minL, maxL);

			}

			if (random1 == 1 && random2 == 2) {
				x = UnityEngine.Random.Range (minL, maxL);
				y = 1;
				z = UnityEngine.Random.Range (minR, maxR);
			
			}
			if (random1 == 2 && random2 == 1) {
				x = UnityEngine.Random.Range (minR, maxR);
				y = 1;
				z = UnityEngine.Random.Range (minL, maxL);

			}
			if (random1 == 2 && random2 == 2) {
				x = UnityEngine.Random.Range (minR, maxR);
				y = 1;
				z = UnityEngine.Random.Range (minR, maxR);

			}

			Vector3 Location = new Vector3 (x, y, z);
		
	

			if (!occupied.Contains (Location)) {
				occupied.Add (Location);
				Debug.Log(occupied.Count);
			}
		}
		for (int i = 0; i < amount; i++) {
			Vector3 V = (Vector3)occupied[i];

			Instantiate (cube, V, Quaternion.identity);
		}
			

	}
	

	bool equals(Vector3 thiy, Vector3 that)
	{

		return thiy.x == that.x && thiy.z == that.z;

	}
	
	}

