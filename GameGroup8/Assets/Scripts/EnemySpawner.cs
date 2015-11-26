using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject windEnemy;
	public GameObject waterEnemy;
	public GameObject earthEnemy;
	public int wave;

	private int enemiesThisWave;
	private int totalEnemiesSpawned;
	public static int enemiesDefeaten;
	public int enemiesToDefeat;
	
	public Text waveText;
	public Text enemiesToDefeatText;

	public float timeTillNextWave = 10f;
	private float timeBetweenWaves = 30f;

	public Text timeTillNextWaveText;

	private float changeToSpawnWindEnemy = 1/3;
	private float changeToSpawnWaterEnemy = 1/3;
	private float changeToSpawnEarthEnemy = 1/3;

	private float[] changeToSpawnByLevel = new float[5];
	private float mu = 0f;
	private float sigma = 1f;


	void Start () {
		wave = 1;
		waveText.text = "Current wave: 1";
		enemiesToDefeat = 0;
		enemiesDefeaten = 0;
		enemiesToDefeatText.text = "Enemies to defeat this wave: " + enemiesToDefeat;
		timeTillNextWaveText.text = "Time till next wave: " + timeTillNextWave;
		setEnemiesThisWave ();
		calculateLevelToSpawn ();
	}

	void Update () {
		if(Time.time > timeTillNextWave){
			setEnemiesThisWave();
			timeTillNextWave = Time.time + timeBetweenWaves;
			totalEnemiesSpawned +=enemiesThisWave;
			enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
			nextWave();
			mu += 1f/3f;
			calculateLevelToSpawn ();
		}
		enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
		timeTillNextWaveText.text = "Time till next wave: " + (timeTillNextWave - Time.time);
		calculateChangeToSpawnEarth ();
		calculateChangeToSpawnWater ();
		calculateChangeToSpawnWind ();
		enemiesToDefeat = totalEnemiesSpawned - enemiesDefeaten;
		enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
	}

	Vector3 getRandomPosition(){
		return new Vector3 ((float)Random.Range (20, 40), 1f, (float)Random.Range (-10, 10));
	}

	void nextWave(){
		waveText.text = "Current wave: " + wave;
		enemiesToDefeat = 0;
		for(int i = 0; i < enemiesThisWave; i++){
			float random = Random.Range (0.0f, 1.0f); 
			if(random <= changeToSpawnWaterEnemy){
				GameObject waterEnemyClone = waterEnemy;
				waterEnemyClone.GetComponent<EnemyController>().setLevel(getLevelToSpawn());
				Instantiate (waterEnemy, getRandomPosition(), Quaternion.identity);
			}
			else if(random <= changeToSpawnWaterEnemy + changeToSpawnWindEnemy){
				GameObject windEnemyClone = windEnemy;
				windEnemyClone.GetComponent<EnemyController>().setLevel (getLevelToSpawn());
				Instantiate (windEnemyClone, getRandomPosition(), Quaternion.identity);
			}
			else{
				GameObject earthEnemyClone = earthEnemy;
				earthEnemyClone.GetComponent<EnemyController>().setLevel (getLevelToSpawn());
				Instantiate (earthEnemyClone, getRandomPosition(), Quaternion.identity);
			}
		}
		wave++;
	}
			  
	void calculateChangeToSpawnWind(){
		int tempType = PlayerAttacker.currentType.getType ();
		if(tempType == 1){
			changeToSpawnWindEnemy = 1f/3f;
		}
		if(tempType == 2){
			changeToSpawnWindEnemy = 1f/6f;
		}
		if(tempType == 3){
			changeToSpawnWindEnemy = 1f/2f;
		}
	}

	void calculateChangeToSpawnWater(){
		int tempType = PlayerAttacker.currentType.getType ();
		if(tempType == 1){
			changeToSpawnWaterEnemy = 1f/2f;
		}
		if(tempType == 2){
			changeToSpawnWaterEnemy = 1f/3f;
		}
		if(tempType == 3){
			changeToSpawnWaterEnemy = 1f/6f;
		}
	}

	void calculateChangeToSpawnEarth(){
		int tempType = PlayerAttacker.currentType.getType ();
		if(tempType == 1){
			changeToSpawnEarthEnemy = 1f/6f;
		}
		if(tempType == 2){
			changeToSpawnEarthEnemy = 1f/2f;
		}
		if(tempType == 3){
			changeToSpawnEarthEnemy = 1f/3f;
		}
	}

	void setEnemiesThisWave(){
		enemiesThisWave = (int)Mathf.Floor(2f + wave * 0.5f);
	}

	void calculateLevelToSpawn(){
		for (int i = 0; i < 5; i++) {
			changeToSpawnByLevel[i] = (1f / (sigma * Mathf.Sqrt (2f * Mathf.PI))) * Mathf.Exp(-0.5f * Mathf.Pow((((float)i - mu) / sigma), 2));
		}
	}

	int getLevelToSpawn(){
		float totalChange = 0;
		for(int i = 0; i < 5; i ++){
			totalChange += changeToSpawnByLevel[i];
		}
		float random = Random.Range (0f, totalChange);
		Debug.Log ("total change: " + totalChange);
		Debug.Log ("random number: " + random);
		if (random <= changeToSpawnByLevel [0]) {
			return 1;
		} else if (random <= changeToSpawnByLevel [0] + changeToSpawnByLevel [1]) {
			return 2;
		} else if (random <= changeToSpawnByLevel [0] + changeToSpawnByLevel [1] + changeToSpawnByLevel [2]) {
			return 3;
		} else if (random <= changeToSpawnByLevel [0] + changeToSpawnByLevel [1] + changeToSpawnByLevel [2] + changeToSpawnByLevel [3]) {
			return 4;
		} else {
			return 5;
		}
	}

}
