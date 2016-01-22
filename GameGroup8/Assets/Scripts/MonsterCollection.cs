
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/* Teringgroot script dat alle klassen voor het opslaan en serializen van de savedata bevat. 
   Basically this scripts holds dataclasses of the attributes of the scene.
   In the save process there are basically these steps
   // Saving
   1. Creating a class that holds the data of the scene
   2. Serialize the class and save it into xml
   // Loading
   3. loading data from xml into data-class
   4. export from data-class to scene
   

*/

/// <summary>
///  Class that holds a list of enemies currently in the scene
/// </summary>

[XmlRoot("MonsterCollection")]
public class MonsterList
{

	[XmlArray("list"),XmlArrayItem("list")]
	 public Monster[]  list = new Monster[1000];


	public Monster[] getMonsterlist(){
		return this.list;
	}

}

/// <summary>
///  Class that holds a list of turrets currently placed
/// </summary>
public class TurretList{
	
	[XmlArray("list"),XmlArrayItem("list")]
	public Turret[]  list = new Turret[10];

	public Turret[] getTurretList(){
		return this.list;
	}
}


/// <summary>
/// Class that holds al data about the base
/// </summary>
public class BaseSave
{

    // positon
    public float posx;
    public float posy;
    public float posz;
    public float rotx;
    public float roty;
    public float rotz;
    public float rotw;


    // properties
    public int wall;
    public int health;
    public bool searchLights;
    public bool areaLight;
    
            
    public BaseSave()
    {
        var position = GameObject.FindGameObjectWithTag("BASE").GetComponent<Transform>().position;
        var rotation = GameObject.FindGameObjectWithTag("BASE").GetComponent<Transform>().rotation;

        posx = position.x;
        posy = 0.0f;
        posz = position.z;

        rotx = rotation.x;
        roty = rotation.y;
        rotz = rotation.z;
        rotw = rotation.w;
        
        BaseController Base = GameObject.Find("Gate").GetComponent<BaseController>();
        wall = Base.wall;
        health = Base.baseHealth;
        searchLights = Base.boughtLights;
        areaLight = Base.boughtFlashlight;
    }
}

/// <summary>
/// Class that holds the position of the moon
/// </summary>
public class MoonSave
{

    // position
    public float posx;
    public float posy;
    public float posz;
    public float rotx;
    public float roty;
    public float rotz;
    public float rotw;

    public MoonSave()
    {
        var position = GameObject.FindGameObjectWithTag("Moon").GetComponent<Transform>().position;
        var rotation = GameObject.FindGameObjectWithTag("Moon").GetComponent<Transform>().rotation;

        posx = position.x;
        posy = position.y;
        posz = position.z;

        rotx = rotation.x;
        roty = rotation.y;
        rotz = rotation.z;
        rotw = rotation.w;
    }
}

/// <summary>
/// Class that holds the position of the sun
/// </summary>
public class SunSave
{
    // position
    public float posx;
    public float posy;
    public float posz;
    public float rotx;
    public float roty;
    public float rotz;
    public float rotw;

    public SunSave()
    {
        var position = GameObject.FindGameObjectWithTag("Sun").GetComponent<Transform>().position;
        var rotation = GameObject.FindGameObjectWithTag("Sun").GetComponent<Transform>().rotation;

        posx = position.x;
        posy = position.y;
        posz = position.z;

        rotx = rotation.x;
        roty = rotation.y;
        rotz = rotation.z;
        rotw = rotation.w;
    }
}

public class AnalyticsSave
{
    public int score;
    public int playerLevel;
    public int timesDied;
    
    public float timeOutside;
    public float timeCloseToBase;
    public float timeBase;

    [XmlArray("list"), XmlArrayItem("list")]
    public int[] weapon;
    [XmlArray("list"), XmlArrayItem("list")]
    public int[] shots_hit;
    [XmlArray("list"), XmlArrayItem("list")]
    public int[] hit_enemyType;
    [XmlArray("list"), XmlArrayItem("list")]
    public int[] playerUpgrades;
    [XmlArray("list"), XmlArrayItem("list")]
    public int[] building;

    [XmlArray("list"), XmlArrayItem("list")]
    public float[][] placeRIP;
    [XmlArray("list"), XmlArrayItem("list")]
    public float[][] placeKill;

    public AnalyticsSave()
    {
        score = Analytics.getScore();
        playerLevel = Analytics.getPlayerLevel();
        timesDied = Analytics.get_timesDied();
        timeOutside = Analytics.get_timeOutside();
        timeCloseToBase = Analytics.get_timeCTBase();
        timeBase = Analytics.get_timeBase();

        weapon = Analytics.getWeapons();
        shots_hit = Analytics.getHitCount();
        hit_enemyType = Analytics.getHitByEnemy();
        playerUpgrades = Analytics.getPlayerUpgrades();
        building = Analytics.getBuildings();

        placeKill = Analytics.getPlaceKill();
        placeRIP = Analytics.getPlaceDied();
    }
}


public class Outsidesave{
	
	public int wave;
	public float timeTillNextWave;
    public float CountDownTimerValue;
    public int enemiesDefeaten;
	public int enemiesToDefeat;
	public int totalEnemiesSpawned;
    public float CountDownTimer_Value;

   // tags of fields in the base
    public string tagOfMat1;
	public string tagOfMat2;
	public string tagOfMat3;
	public string tagOfMat4;
    public string tagOfMat5;
    public string tagOfMat6;
    public string tagOfMat7;
    public string tagOfMat8;
    public string tagOfMat9;

    public int unitCount;

	public int[] xCoordinatesOfUnits = new int[2000];
	public int[] zCoordinatesOfUnits = new int[2000];

	public int[] xCoordinatesOfBaseUnits = new int[50];
	public int[] zCoordinatesOfBaseUnits = new int[50];

	public int[] xCoordinatesOfHealthUnits = new int[50];
	public int[] zCoordinatesOfHealthUnits = new int[50];

	public int[] xCoordinatesOfFatiqueUnits = new int[50];
	public int[] zCoordinatesOfFatiqueUnits = new int[50];

	public int[] xCoordinatesOfEnergyUnits = new int[50];
	public int[] zCoordinatesOfEnergyUnits = new int[50];

	public Outsidesave(){
        this.wave = Camera.main.GetComponent<EnemySpawner>().wave;
		// this.timeTillNextWave = Camera.main.GetComponent<EnemySpawner>().timeTillNextWave;
        this.CountDownTimer_Value = Camera.main.GetComponent<EnemySpawner>().CountDownTimerValue;
        //Debug.Log(timeTillNextWave.ToString());
		this.enemiesDefeaten = EnemySpawner.enemiesDefeaten;
		this.totalEnemiesSpawned = EnemySpawner.totalEnemiesSpawned;
		enemiesToDefeat = Camera.main.GetComponent<EnemySpawner> ().enemiesToDefeat;
		tagOfMat1 = GameObject.Find ("PlacementPlane").tag;
		tagOfMat2 = GameObject.Find ("PlacementPlane (1)").tag;
		tagOfMat3 = GameObject.Find ("PlacementPlane (2)").tag;
		tagOfMat4 = GameObject.Find ("PlacementPlane (3)").tag;
        tagOfMat5 = GameObject.Find("PlacementPlane (4)").tag;
        tagOfMat6 = GameObject.Find("PlacementPlane (5)").tag;
        tagOfMat7 = GameObject.Find("PlacementPlane (6)").tag;
        tagOfMat8 = GameObject.Find("PlacementPlane (7)").tag;
        tagOfMat9 = GameObject.Find("PlacementPlane (8)").tag;
        unitCount = PlayerController.getCount();
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Pick-Up");
		for (int i = 0; i < units.Length; i++) {
			xCoordinatesOfUnits [i] = (int)units [i].transform.position.x;
			zCoordinatesOfUnits [i] = (int)units [i].transform.position.z;
		}
		GameObject[] healthUnits = GameObject.FindGameObjectsWithTag ("Health-Pick-Up");
		for (int i = 0; i < healthUnits.Length; i++) {
			xCoordinatesOfHealthUnits [i] = (int)healthUnits [i].transform.position.x;
			zCoordinatesOfHealthUnits [i] = (int)healthUnits [i].transform.position.z;
		}
		GameObject[] fatiqueUnits = GameObject.FindGameObjectsWithTag ("Fatique-Pick-Up");
		for (int i = 0; i < fatiqueUnits.Length; i++) {
			xCoordinatesOfFatiqueUnits [i] = (int)fatiqueUnits [i].transform.position.x;
			zCoordinatesOfFatiqueUnits [i] = (int)fatiqueUnits [i].transform.position.z;
		}
		GameObject[] energyUnits = GameObject.FindGameObjectsWithTag ("Energy-Pick-Up");
		for (int i = 0; i < energyUnits.Length; i++) {
			xCoordinatesOfEnergyUnits [i] = (int)energyUnits [i].transform.position.x;
			zCoordinatesOfEnergyUnits [i] = (int)energyUnits [i].transform.position.z;
		}
		GameObject[] baseUnits = GameObject.FindGameObjectsWithTag ("Base-Pick-Up");
		for (int i = 0; i < baseUnits.Length; i++) {
			xCoordinatesOfBaseUnits [i] = (int)baseUnits [i].transform.position.x;
			zCoordinatesOfBaseUnits [i] = (int)baseUnits [i].transform.position.z;
		}
	}	
}

/// <summary>
/// Class that holds the player attributes, such as upgrades, health, experience 
/// </summary>
public class Player
{
	public float posx;
	public float posy;
	public float posz;
	
	public float rotx;
	public float roty;
	public float rotz;
	public float rotw;

    public int attackPoints;
    public int speedPoints;
    public int healthPoints;
    public int energyPoints;

    public int level;
    public int experience;
    public int pointsToUpgrade;

    public int health;
    public int fatique;
    public int energy;

    public int currentWeapon;
    public bool[] unlocked;
	
	public Player(){
		var position = PlayerController.getPosition();
		var rotation = PlayerController.getRotation ();
		
		posx = position.x;
		posy = 0.0f;
		posz = position.z;
		
		rotx = rotation.x;
		roty = rotation.y;
		rotz = rotation.z;
		rotw = rotation.w;

        attackPoints = PlayerAttributes.attackPoints;
        speedPoints = PlayerAttributes.speedPoints;
        healthPoints = PlayerAttributes.maxHealthPoints;
        energyPoints = PlayerAttributes.maxEnergyPoints;

        level = PlayerAttributes.level;
        experience = PlayerAttributes.experience;
        pointsToUpgrade = PlayerAttributes.pointsToUpgrade;

        health = PlayerAttributes.getHealth();
        energy = PlayerAttributes.getEnergy();
        fatique = PlayerAttributes.getFatique();

        currentWeapon = GameObject.Find("player").GetComponent<PlayerAttacker>().currentWeaponInt;
        unlocked = PlayerAttacker.unlocked;


    }
	public  float getRotx(){
		return rotx;
	}
	public float getRoty(){
		return roty;
	}
	public float getRotw(){
		return rotw;
	}
	public float getRotz(){
		return rotz;
	}
	public  float getPosx(){
		return posx;
	}
	public  float getPosy(){
		return posy;
	}
	public float getPosz(){
		return posz;
	}
}


public class MonsterCollection : MonoBehaviour
{
	public static MonsterList monsterlist = new MonsterList();
	public static TurretList turretList = new TurretList();
    public GameObject player;
    
    // Save & Load 
	public static void MonsterSave(string path)
	{

        List<EnemyController> Monsters = MiniMapScript.enemies;
        for (int i = 0; i < Monsters.Count; i++)
        {
            monsterlist.list[i] = new Monster(Monsters[i]);
        }

        var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, monsterlist);
		}
	}


    /// <summary>
    /// Serialize 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static MonsterList MonsterLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(MonsterList));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MonsterList;
        }
    }

    public static SunSave SunPreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(SunSave));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as SunSave;
        }
    }

    public static void SunLoad(string path)
    {

        var sun_ = SunPreLoad(path);

        GameObject tempSun = GameObject.FindWithTag("Sun");
        Vector3 templocation;
        templocation.x = sun_.posx;
        templocation.y = sun_.posy;
        templocation.z = sun_.posz;
        Quaternion temprotation;
        temprotation.x = sun_.rotx;
        temprotation.y = sun_.roty;
        temprotation.w = sun_.rotw;
        temprotation.z = sun_.rotz;


        tempSun.transform.position = templocation;
        tempSun.transform.rotation = temprotation;
    }


    /// <summary>
    /// Saves sun to xml
    /// </summary>
    /// <param name="path"></param>
    public static void SunSave(string path)
    {
        var sunsave = new SunSave();

        var serializer = new XmlSerializer(typeof(SunSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, sunsave);
        }
    }
    /// <summary>
    /// Saves moon to xml
    /// </summary>
    /// <param name="path"></param>
    public static MoonSave MoonPreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(MoonSave));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MoonSave;
        }
    }


    /// <summary>
    ///  Load moon from xml
    /// </summary>
    /// <param name="path"></param>
    public static void MoonLoad(string path)
    {

        var moon_ = MoonPreLoad(path);
        GameObject tempMoon = GameObject.FindWithTag("Moon");
        Vector3 templocation;
        templocation.x = moon_.posx;
        templocation.y = moon_.posy;
        templocation.z = moon_.posz;
        Quaternion temprotation;
        temprotation.x = moon_.rotx;
        temprotation.y = moon_.roty;
        temprotation.w = moon_.rotw;
        temprotation.z = moon_.rotz;

        tempMoon.transform.position = templocation;
        tempMoon.transform.rotation = temprotation;
    }

    /// <summary>
    /// Saves moon to xml
    /// </summary>
    /// <param name="path"></param>
    public static void MoonSave(string path)
    {
        var moonsave = new MoonSave();

        var serializer = new XmlSerializer(typeof(MoonSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, moonsave);
        }
    }

    /// <summary>
    /// Saves base to xml
    /// </summary>
    /// <param name="path"></param>

    public static void BaseSave(string path)
    {
        var basesave = new BaseSave();

        var serializer = new XmlSerializer(typeof(BaseSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, basesave);
        }
    }


    /// <summary>
    /// Loads base.xml into base dataclass
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Basesave dataclass</returns>

    public static BaseSave BasePreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(BaseSave));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as BaseSave;
        }
    }


    /// <summary>
    /// Loads the data from the base-dataclass in the scene
    /// </summary>
    /// <param name="path"></param>
    public static void BaseLoad(string path)
    {

        var base_ = BasePreLoad(path);

        GameObject tempBase = GameObject.FindWithTag("BASE");
        Vector3 templocation;
        templocation.x = base_.posx;
        templocation.y = 0.0f;
        templocation.z = base_.posz;
        Quaternion temprotation;
        temprotation.x = base_.rotx;
        temprotation.y = base_.roty;
        temprotation.w = base_.rotw;
        temprotation.z = base_.rotz;


        tempBase.transform.position = templocation;
        tempBase.transform.rotation = temprotation;

        BaseController Base = GameObject.Find("Gate").GetComponent<BaseController>();
        Base.wall = base_.wall;
        Base.baseHealth = base_.health;
        Base.boughtFlashlight = base_.areaLight;
        Base.boughtLights = base_.searchLights;
    }


    /// <summary>
    /// Saves the playerdata-class in a xml
    /// </summary>
    /// <param name="path"></param>

    public static void playerSave(string path)
	{
		var player =  new Player();

		var serializer = new XmlSerializer(typeof(Player));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, player);
		}
	}

    /// <summary>
    /// Loads player xml into player dataclass
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Player playerpreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(Player));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Player;
        }

    }

    /// <summary>
    /// sets player data in scene from player class
    /// </summary>
    /// <param name="path"></param>

    public static void playerLoad(string path)
    {

        var player = playerpreLoad(path);

        Vector3 templocation;
        templocation.x = player.getPosx();
        templocation.y = 0.0f;
        templocation.z = player.getPosz();
        Quaternion temprotation;
        temprotation.x = player.getRotx();
        temprotation.y = player.getRoty();
        temprotation.w = player.getRotw();
        temprotation.z = player.getRotz();

		GameObject.Find("player").transform.position = templocation;
		GameObject.Find("player").transform.rotation = temprotation;

        PlayerAttributes.attackPoints = player.attackPoints;
        PlayerAttributes.speedPoints = player.speedPoints;
        PlayerAttributes.maxHealthPoints = player.healthPoints;
        PlayerAttributes.maxEnergyPoints = player.energyPoints;

        PlayerAttributes.level = player.level;
        PlayerAttributes.experience = player.experience;
        PlayerAttributes.pointsToUpgrade = player.pointsToUpgrade;

        PlayerAttributes.setHealth(player.health);
        PlayerAttributes.setEnergy(player.energy);
        PlayerAttributes.setFatique(player.fatique);

        GameObject.Find("player").GetComponent<PlayerAttacker>().currentWeaponInt = player.currentWeapon;
        PlayerAttacker.unlocked = player.unlocked;
		GameObject.Find ("player").GetComponent<PlayerAttacker> ().LoadFromSave ();
    }


    /// <summary>
    ///  Saves the turrets in a xml
    /// </summary>
    /// <param name="path"></param>

    public static void turretSave(string path){
		List<GameObject> Buildings = BaseController.turrets;
		for (int i = 0; i < Buildings.Count; i++){
			turretList.list[i] = new Turret(Buildings[i].GetComponent<BuildingController>());
		}
		
		var serializer = new XmlSerializer(typeof(TurretList));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, turretList);
		}
	}
    /// <summary>
    /// Saves the data from outsidesavedata-class as xml
    /// </summary>
    /// <param name="path"></param>
	public static void outsideSave(string path){

		var outside = new Outsidesave();

		var serializer = new XmlSerializer(typeof(Outsidesave));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, outside);
		}
	}
	

    /// <summary>
    ///  Loads turrets into Turretlist-dataclass
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
	public static TurretList turretLoad(string path){
		var serializer = new XmlSerializer(typeof(TurretList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as TurretList;
		}
	}
    
    /// <summary>
    ///  loads data of outside into outsidesave-data-class
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>

	public static Outsidesave outsidepreLoad(string path){
		var serializer = new XmlSerializer(typeof(Outsidesave));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Outsidesave;
		}
	}


    /// <summary>
    ///  Sets the outsidedata-class data in scene
    /// </summary>
    /// <param name="path"></param>

	public static void outsideLoad(string path){
		var outside = outsidepreLoad (path);

		Camera.main.GetComponent<EnemySpawner>().wave = outside.wave;
        Camera.main.GetComponent<EnemySpawner>().timeTillNextWave = outside.CountDownTimer_Value;
        // Camera.main.GetComponent<EnemySpawner>().CountDownTimerValue = outside.CountDownTimer_Value;
        Camera.main.GetComponent<EnemySpawner>().enemiesToDefeat = outside.enemiesToDefeat;
		EnemySpawner.enemiesDefeaten = outside.enemiesDefeaten;
		EnemySpawner.totalEnemiesSpawned = outside.totalEnemiesSpawned;
		GameObject.Find ("PlacementPlane").tag = outside.tagOfMat1;
		GameObject.Find ("PlacementPlane (1)").tag = outside.tagOfMat2;
		GameObject.Find ("PlacementPlane (2)").tag = outside.tagOfMat3;
		GameObject.Find ("PlacementPlane (3)").tag = outside.tagOfMat4;
        GameObject.Find("PlacementPlane (4)").tag = outside.tagOfMat5;
        GameObject.Find("PlacementPlane (5)").tag = outside.tagOfMat6;
        GameObject.Find("PlacementPlane (6)").tag = outside.tagOfMat7;
        GameObject.Find("PlacementPlane (7)").tag = outside.tagOfMat8;
        GameObject.Find("PlacementPlane (8)").tag = outside.tagOfMat9;
        PlayerController.setCount(-outside.unitCount);
		Camera.main.GetComponent<PSpawner> ().LoadFromSave (outside.xCoordinatesOfUnits, outside.zCoordinatesOfUnits, outside.xCoordinatesOfBaseUnits, outside.zCoordinatesOfBaseUnits, outside.xCoordinatesOfEnergyUnits, outside.zCoordinatesOfEnergyUnits, outside.xCoordinatesOfHealthUnits, outside.zCoordinatesOfHealthUnits, outside.xCoordinatesOfFatiqueUnits, outside.zCoordinatesOfFatiqueUnits);
	}

    /// <summary>
    ///  saves the map in xml
    /// </summary>
    /// <param name="path"></param>
	
    public static void MapSave(string path)
    {
        WorldBuilderII world = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
        MapSaver map_save = new MapSaver(world);

        var serializer = new XmlSerializer(typeof(MapSaver));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, map_save);
        }
    }

    /// <summary>
    /// loads world.xml into mapsaver dataclass
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>

    public static MapSaver mapPre_load(string path)
    {
        var serializer = new XmlSerializer(typeof(MapSaver));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MapSaver;
        }
    }

    /// <summary>
    /// sets the scene from world
    /// </summary>
    /// <param name="path"></param>
    public static void mapLoad(string path)
    {
        var map_load = mapPre_load(path);
        WorldBuilderII world = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
       
        map_load.MapLoader(world);
    }


    /// <summary>
    ///  saves the analytics-dataclass
    /// </summary>
    /// <param name="path"></param>

    public static void AnalyticsSave(string path)
    {
        var analyticsSave = new AnalyticsSave();

        var serializer = new XmlSerializer(typeof(AnalyticsSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, analyticsSave);
        }
    }
}