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
    public GameObject Base;

	public int amountOfUnitsSpawned;
	

	public void FirstLoad () {
		PlaceCubes ();
		amountOfUnitsSpawned = amount;
	}

    /// <summary>
    /// Place an unit at a given location
    /// </summary>
    /// <param name="place"></param>
	public void placeUnit(Vector3 place){
		GameObject unitClone = Instantiate (unit, new Vector3(place.x, 1, place.z), Quaternion.identity) as GameObject;
		unitClone.transform.Rotate (-90, 0, 0);
		amountOfUnitsSpawned++;
	}

	/// <summary>
	/// Spawns units when a new wave starts.
	/// </summary>
    public void nextWave(){
		
		for (int i = 0; i < 20; i++) {
			if (amountOfUnitsSpawned < 300) {
				GameObject unitClone = Instantiate (unit, getRandomPosition (), Quaternion.identity) as GameObject;
				unitClone.transform.Rotate (-90, 0, 0);
				amountOfUnitsSpawned += 20;
			}
		}
		for (int i = 0; i < 2; i++) {
			GameObject healthUnitClone = Instantiate (healthUnit, getRandomPosition (), Quaternion.identity) as GameObject;
			healthUnitClone.transform.Rotate (270, 0, 0);

			GameObject energyUnitClone = Instantiate (energyUnit, getRandomPosition (), Quaternion.identity) as GameObject;
			energyUnitClone.transform.Rotate (270, 0, 0);

			GameObject fatiqueUnitClone = Instantiate (fatiqueUnit, getRandomPosition (), Quaternion.identity) as GameObject;
			fatiqueUnitClone.transform.Rotate (270, 0, 0);

			GameObject baseUnitClone = Instantiate (baseUnit, getRandomPosition (), Quaternion.identity) as GameObject;
			baseUnitClone.transform.Rotate (270, 0, 0);
		}
    }

    /// <summary>
    /// Spawn all the units
    /// </summary>
	void PlaceCubes(){
		List<Vector3> occupied = new List<Vector3> ();
		while (occupied.Count <= amount) {
			Vector3 Location = getRandomPosition ();
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

	/// <summary>
	/// Get a position for a unit to be spawned, it gets a position with a distance larger than 25 from the base.
	/// </summary>
	/// <returns>The random position.</returns>
    public Vector3 getRandomPosition(){
        Vector3 tempPos = new Vector3(Random.Range(-130, 130), 0, Random.Range(-130, 130));
		int misses = 5;
        while (misses > 0){
            if (Vector3.Distance(tempPos, Base.transform.position) > 25){
                return tempPos;
				break;
            }else{
                misses--;
            }
        }
        return new Vector3 (139, 0, -139);
    }

	/// <summary>
	/// Spawns units saved in a xml file.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="xBase">X base.</param>
	/// <param name="zBase">Z base.</param>
	/// <param name="xEnergy">X energy.</param>
	/// <param name="zEnergy">Z energy.</param>
	/// <param name="xHealth">X health.</param>
	/// <param name="zHealth">Z health.</param>
	/// <param name="xFatique">X fatique.</param>
	/// <param name="zFatique">Z fatique.</param>
	public void LoadFromSave(int[] x, int[] z, int[] xBase, int[] zBase, int[] xEnergy, int[] zEnergy, int[] xHealth, int[] zHealth, int[] xFatique, int[] zFatique){
		for (int i = 0; i < x.Length; i++) {
			if (x [i] != 0) {
				GameObject unitClone = Instantiate (unit, new Vector3 (x [i], 1, z [i]), Quaternion.identity) as GameObject;
				unitClone.transform.Rotate (-90, 0, 0);
				amountOfUnitsSpawned++;
			}
		}
		for (int i = 0; i < xBase.Length; i++) {
			if (xBase [i] != 0) {
				GameObject baseUnitClone = Instantiate (baseUnit, new Vector3 (xBase [i], 1, zBase [i]), Quaternion.identity) as GameObject;
				baseUnitClone.transform.Rotate (-90, 0, 0);
			}
		}
		for (int i = 0; i < xEnergy.Length; i++) {
			if (xEnergy [i] != 0) {
				GameObject energyUnitClone = Instantiate (energyUnit, new Vector3 (xEnergy [i], 1, zEnergy [i]), Quaternion.identity) as GameObject;
				energyUnitClone.transform.Rotate (-90, 0, 0);
			}
		}
		for (int i = 0; i < xHealth.Length; i++) {
			if (xHealth [i] != 0) {
				GameObject healthUnitClone = Instantiate (healthUnit, new Vector3 (xHealth [i], 1, zHealth [i]), Quaternion.identity) as GameObject;
				healthUnitClone.transform.Rotate (-90, 0, 0);
			}
		}
		for (int i = 0; i < xFatique.Length; i++) {
			if (xFatique [i] != 0) {
				GameObject fatiqueUnitClone = Instantiate (baseUnit, new Vector3 (xFatique [i], 1, zFatique [i]), Quaternion.identity) as GameObject;
				fatiqueUnitClone.transform.Rotate (-90, 0, 0);
			}
		}
	}
}