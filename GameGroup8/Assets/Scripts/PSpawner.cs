using UnityEngine;
using System.Collections.Generic;



public class PSpawner : MonoBehaviour {
	
	public GameObject cube;
	public static int amount;
	public int amount2;
	public int minL, minR, maxL, maxR;
	
	
	
	// Use this for initialization
	void Start () {
		amount = amount2;
		PlaceCubes ();
		
	}
	void PlaceCubes(){
		
		List<Vector3> occupied = new List<Vector3> ();
		int x, y, z, random1;
		x = 0;
		y = 0;
		z = 0;

		while (occupied.Count <= amount) {

			random1 = UnityEngine.Random.Range (0, 5);
			//random2 = UnityEngine.Random.Range (0, 3);
			//Debug.Log(random1);
			//Debug.Log(random2);
			/*
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
			
			*/

			switch (random1)
			{
			case 1:
				x = UnityEngine.Random.Range (minL, maxL);
				y = 1;
				z = UnityEngine.Random.Range (minL, maxL);
				break;
			case 2:
				x = UnityEngine.Random.Range (minL, maxL);
				y = 1;
				z = UnityEngine.Random.Range (minR, maxR);
				break;
			case 3:
				x = UnityEngine.Random.Range (minR, maxR);
				y = 1;
				z = UnityEngine.Random.Range (minL, maxL);
				break;
			case 4:
				x = UnityEngine.Random.Range (minR, maxR);
				y = 1;
				z = UnityEngine.Random.Range (minR, maxR);
				break;
			default:
				//Debug.Log(random1);
				break;
			}

			Vector3 Location = new Vector3 (x, y, z);


			if (!occupied.Contains (Location)) {
				occupied.Add (Location);
				//Debug.Log(occupied.Count);
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

	static int getamount()
	{
		return amount;
	}
	
}
