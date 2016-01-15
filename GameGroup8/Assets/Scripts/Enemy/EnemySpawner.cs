using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject desertEagle;
	public GameObject hammerHead;
	public GameObject fireFox;
    public GameObject polarBear;
    public GameObject meepMeep;
    public GameObject oilPhant;
	public int wave = 1;

	private int enemiesThisWave;
	public static int totalEnemiesSpawned;
	public static int enemiesDefeaten;
	public int enemiesToDefeat;
	
	public Text waveText;
	public Text enemiesToDefeatText;

	public float timeTillNextWave = 10f;
    public float CountDownTimerValue;
	private float minTimeBetweenWaves = 45f;
	private float maxTimeBetweenWaves = 90f;

	public Text timeTillNextWaveText;

	private float changeToSpawnDesertEagle = 1/3;
	private float changeToSpawnHammerHead = 1/3;
	private float changeToSpawnFireFox = 1/3;
    private float changeToSpawnPolarBear = 1/3;
    private float changeToSpawnMeepMeep = 0;
    private float changeToSpawnOilphant = 0;

	private float[] changeToSpawnByLevel = new float[5];
	private float mu = 0f;
	private float sigma = 1f;

    public List<EnemyController> unbuffedEnemies = new List<EnemyController>();

    Score score_;
    public Canvas canvas;
    public GameObject EnemyHealthBars;


    public void FirstLoad () {
		wave = 1;
		waveText.text = "Current wave: 1";
		enemiesToDefeat = 0;
		enemiesDefeaten = 0;
		enemiesToDefeatText.text = "Enemies to defeat this wave: " + enemiesToDefeat;
		timeTillNextWaveText.text = "Time till next wave: " + timeTillNextWave;
		setEnemiesThisWave ();
		calculateLevelToSpawn ();
	}

    public void Awake()
    {
        score_ = Camera.main.GetComponent<Score>();
    }

	void Update () {
		waveText.text = "Current wave: " + wave;
		if(Time.timeSinceLevelLoad > timeTillNextWave){
			setEnemiesThisWave();
			timeTillNextWave = Time.timeSinceLevelLoad + Random.Range (minTimeBetweenWaves, maxTimeBetweenWaves);
			totalEnemiesSpawned += enemiesThisWave;
			enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
            nextWave();
            // Add scores the beginning of the wave
            score_.addScoreWave(wave);
            mu += 0.25f;
			calculateLevelToSpawn ();
		}
        CountDownTimerValue = timeTillNextWave - Time.timeSinceLevelLoad; 

        enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
		timeTillNextWaveText.text = "Time till next wave: " + (int)(CountDownTimerValue);
        enemiesToDefeat = MiniMapScript.enemies.Count;
		enemiesToDefeatText.text = "Enemies to defeat: " + enemiesToDefeat;
	}

	Vector3 getRandomPosition(){
        return new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)) ;
	}

	void nextWave(){
        Camera.main.GetComponent<PSpawner>().nextWave();
        calculateChangeToSpawnFireFox();
        calculateChangeToSpawnHammerHead();
        calculateChangeToSpawnDesertEagle();
        calculateChangeToSpawnPolarBear();
        calculateChangeToSpawnMeepMeep();
        //calculateChangeToSpawnOilphant();
        waveText.text = "Current wave: " + wave;
		enemiesToDefeat = 0;
		for(int i = 0; i < enemiesThisWave; i++){
			float random = Random.Range (0.0f, changeToSpawnHammerHead + changeToSpawnDesertEagle + changeToSpawnFireFox + changeToSpawnPolarBear + changeToSpawnMeepMeep + changeToSpawnOilphant);
			if(random <= changeToSpawnHammerHead){
				GameObject waterEnemyClone = hammerHead;
				waterEnemyClone.GetComponent<EnemyController>().setLevel(getLevelToSpawn());
				Instantiate (waterEnemyClone, getRandomPosition(), Quaternion.identity);
                unbuffedEnemies.Add(waterEnemyClone.GetComponent<EnemyController>());
			}
			else if(random <= changeToSpawnHammerHead + changeToSpawnDesertEagle){
				GameObject windEnemyClone = desertEagle;
				windEnemyClone.GetComponent<EnemyController>().setLevel (getLevelToSpawn());
				Instantiate (windEnemyClone, getRandomPosition(), Quaternion.identity);
                unbuffedEnemies.Add(windEnemyClone.GetComponent<EnemyController>());
            }
            else if(random <= changeToSpawnHammerHead + changeToSpawnDesertEagle + changeToSpawnPolarBear)
            {
                GameObject polarBearClone = polarBear;
                polarBearClone.GetComponent<EnemyController>().setLevel(getLevelToSpawn());
                Instantiate(polarBearClone, getRandomPosition(), Quaternion.identity);
                unbuffedEnemies.Add(polarBearClone.GetComponent<EnemyController>());
            }
            else if (random <= changeToSpawnHammerHead + changeToSpawnDesertEagle + changeToSpawnPolarBear + changeToSpawnMeepMeep)
            {
                GameObject meepMeepClone = meepMeep;
                meepMeepClone.GetComponent<EnemyController>().setLevel(getLevelToSpawn());
                meepMeepClone.transform.Rotate(0f, 180f, 0f);
                Instantiate(meepMeepClone, getRandomPosition(), Quaternion.identity);
            }
            else if (random <= changeToSpawnHammerHead + changeToSpawnDesertEagle + changeToSpawnPolarBear + changeToSpawnMeepMeep + changeToSpawnOilphant)
            {
                GameObject oilphantClone = oilPhant;
                oilphantClone.GetComponent<EnemyController>().setLevel(getLevelToSpawn());
                Instantiate(oilphantClone, getRandomPosition(), Quaternion.identity);
            }
            else {
				GameObject earthEnemyClone = fireFox;
				earthEnemyClone.GetComponent<EnemyController>().setLevel (getLevelToSpawn());
				Instantiate (earthEnemyClone, getRandomPosition(), Quaternion.identity);
                unbuffedEnemies.Add(earthEnemyClone.GetComponent<EnemyController>());
            }
		}
        wave++;
	}



    // starts wave from save
    public void savewave()
    {
        var Temp = MonsterCollection.MonsterLoad("Assets/saves/monsters.xml");
        var MonsterList = Temp.getMonsterlist();


        for (int i = 0; i < MonsterList.Length; i++)
        {
            Vector3 location = new Vector3(MonsterList[i].location_x, MonsterList[i].location_y, MonsterList[i].location_z);
            Quaternion rotation = new Quaternion(MonsterList[i].rotation_w, MonsterList[i].rotation_x, MonsterList[i].rotation_y, MonsterList[i].rotation_z);
       
            switch (MonsterList[i].name)
            {
                case "FireFox":
                    {
                        GameObject earthEnemyClone = fireFox;
                        var monster = earthEnemyClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(earthEnemyClone.GetComponent<EnemyController>());
                        Instantiate(earthEnemyClone, location, rotation);
                        break;
                    }

                case "DesertEagle":
                    {
                        GameObject windEnemyClone = desertEagle;
                        var monster = windEnemyClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(windEnemyClone.GetComponent<EnemyController>());
                        Instantiate(windEnemyClone, location, rotation);
                        break;
                    }
                
                case "HammerHead":
                    {
                        GameObject waterEnemyClone = hammerHead;
                        var monster = waterEnemyClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(waterEnemyClone.GetComponent<EnemyController>());
                        Instantiate(waterEnemyClone, location, rotation);
                        break;
                    }

                case "Oilphant":
                    {
                        GameObject oilphantClone = oilPhant;
                        var monster = oilphantClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(oilphantClone.GetComponent<EnemyController>());
                        Instantiate(oilphantClone, location, rotation);
                        break;
                    }

                case "MeepMeep":
                    {
                        GameObject meepMeepClone = meepMeep;
                        var monster = meepMeepClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(meepMeepClone.GetComponent<EnemyController>());
                        Instantiate(meepMeepClone, location, rotation);
                        break;
                    }

                case "PolarBear":
                    {
                        GameObject polarBearClone = polarBear;
                        var monster = polarBearClone.GetComponent<EnemyController>();
                        monster.setLevel(MonsterList[i].level);
                        monster.setHealthFirstTime(MonsterList[i].health);
                        monster.setmaxhealth(MonsterList[i].maxHealth);
                        monster.setAttackPower(MonsterList[i].attackPower);
                        monster.setWalkingSpeed(MonsterList[i].walkingSpeed);
                        monster.setPoisoned(MonsterList[i].isPoisoned);
                        unbuffedEnemies.Add(polarBearClone.GetComponent<EnemyController>());
                        Instantiate(polarBearClone, location, rotation);
                        break;
                    }


            }

        }
    }

    void calculateChangeToSpawnDesertEagle(){
		int tempType = PlayerAttacker.currentWeapon.getType().getType ();
        changeToSpawnDesertEagle = 0;
        changeToSpawnDesertEagle += 1f / 2f * Analytics.getBuildings()[1];
        if (tempType == 1 || tempType == 0){
			changeToSpawnDesertEagle += 1f/3f;
		}
		if(tempType == 2){
			changeToSpawnDesertEagle += 1f/2f;
		}
		if(tempType == 3){
			changeToSpawnDesertEagle += 1f/3f;
		}
    }

	void calculateChangeToSpawnHammerHead(){
		int tempType = PlayerAttacker.currentWeapon.getType().getType ();
        changeToSpawnHammerHead = 0;
        changeToSpawnHammerHead += Analytics.getPlayerUpgrades()[1] * 1f / 4f;
        changeToSpawnHammerHead += 1f / 2f * Analytics.getBuildings()[2];

        if (tempType == 1){
			changeToSpawnHammerHead += 1f/6f;
		}
		if(tempType == 2 || tempType == 0){
			changeToSpawnHammerHead += 1f/3f;
		}
		if(tempType == 3){
			changeToSpawnHammerHead += 1f/2f;
		}
    }

	void calculateChangeToSpawnFireFox(){
		int tempType = PlayerAttacker.currentWeapon.getType().getType ();
        changeToSpawnFireFox = 0;
        changeToSpawnFireFox += Analytics.getPlayerUpgrades()[0] * 1f / 4f;
        changeToSpawnFireFox += 1f / 2f * Analytics.getBuildings()[3];
        if (tempType == 1){
			changeToSpawnFireFox += 1f/2f;
		}
		if(tempType == 2){
			changeToSpawnFireFox += 1f/6f;
		}
		if(tempType == 3 || tempType == 0){
			changeToSpawnFireFox += 1f/3f;
		}
	}

    void calculateChangeToSpawnPolarBear()
    {
        changeToSpawnPolarBear = 0.125f * (changeToSpawnDesertEagle + changeToSpawnFireFox + changeToSpawnHammerHead + changeToSpawnMeepMeep + changeToSpawnOilphant);
    }

    void calculateChangeToSpawnMeepMeep()
    {
        changeToSpawnMeepMeep = 0.0625f * (changeToSpawnDesertEagle + changeToSpawnFireFox + changeToSpawnHammerHead + changeToSpawnPolarBear + changeToSpawnOilphant);
    }

    /*void calculateChangeToSpawnOilphant()
    {
        changeToSpawnOilphant = 0.125f * (changeToSpawnDesertEagle + changeToSpawnFireFox + changeToSpawnHammerHead + changeToSpawnPolarBear + changeToSpawnMeepMeep);
    }*/

    void setEnemiesThisWave(){
        enemiesThisWave = Random.Range(3 + wave, 5 + wave);
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
