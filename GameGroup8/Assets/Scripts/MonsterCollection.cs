
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[XmlRoot("MonsterCollection")]
public class MonsterList
{

	[XmlArray("list"),XmlArrayItem("list")]
	 public Monster[]  list = new Monster[1000];


	public Monster[] getMonsterlist(){
		return this.list;
	}

}

public class TurretList{
	
	[XmlArray("list"),XmlArrayItem("list")]
	public Turret[]  list = new Turret[10];

	public Turret[] getTurretList(){
		return this.list;
	}
}

public class BaseSave
{
    public float posx;
    public float posy;
    public float posz;
    public float rotx;
    public float roty;
    public float rotz;
    public float rotw;

    public int wall;
    public int health;
            
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
        
        wall = GameObject.Find("Gate").GetComponent<BaseController>().wall;
        health = GameObject.Find("Gate").GetComponent<BaseController>().baseHealth;
    }
}

public class MoonSave
{
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

public class SunSave
{
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

    public string tagOfMat1;
	public string tagOfMat2;
	public string tagOfMat3;
	public string tagOfMat4;
	
	public int unitCount;

	public Outsidesave(){
        this.wave = Camera.main.GetComponent<EnemySpawner>().wave;
		// this.timeTillNextWave = Camera.main.GetComponent<EnemySpawner>().timeTillNextWave;
        this.CountDownTimer_Value = Camera.main.GetComponent<EnemySpawner>().CountDownTimerValue;
        Debug.Log(timeTillNextWave.ToString());
		this.enemiesDefeaten = EnemySpawner.enemiesDefeaten;
		this.totalEnemiesSpawned = EnemySpawner.totalEnemiesSpawned;
		enemiesToDefeat = Camera.main.GetComponent<EnemySpawner> ().enemiesToDefeat;
		tagOfMat1 = GameObject.Find ("PlacementPlane").tag;
		tagOfMat2 = GameObject.Find ("PlacementPlane (1)").tag;
		tagOfMat3 = GameObject.Find ("PlacementPlane (2)").tag;
		tagOfMat4 = GameObject.Find ("PlacementPlane (3)").tag;
		unitCount = PlayerController.getCount();
	}	
}


public class Player
{
	public float posx;
	public float posy;
	public float posz;
	
	public float rotx;
	public float roty;
	public float rotz;
	public float rotw;
	
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

	//[XmlArray("monstersList"),XmlArrayItem("monstersList")]
	//public Monster[] monstersList = new Monster[2];

     /*
	 void Update(){

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			playerSave ("Assets/saves/Player.xml");
            MonsterSave("Assets/saves/monsters.xml");
		}

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Load player position
            playerLoad();

            // Load enemies including enemies health
            EnemySpawner enemySpawner = Camera.main.GetComponent<EnemySpawner>();
            enemySpawner.savewave();

            // 

        }

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			GameObject.Find ("Gate").GetComponent<BaseController>().buildFromSave();
			outsideLoad();
		}

    }
    */
    	

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

    public static void SunSave(string path)
    {
        var sunsave = new SunSave();

        var serializer = new XmlSerializer(typeof(SunSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, sunsave);
        }
    }


    public static MoonSave MoonPreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(MoonSave));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MoonSave;
        }
    }

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

    public static void MoonSave(string path)
    {
        var moonsave = new MoonSave();

        var serializer = new XmlSerializer(typeof(MoonSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, moonsave);
        }
    }



    public static void BaseSave(string path)
    {
        var basesave = new BaseSave();

        var serializer = new XmlSerializer(typeof(BaseSave));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, basesave);
        }
    }

    public static BaseSave BasePreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(BaseSave));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as BaseSave;
        }
    }

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

        GameObject.Find("Gate").GetComponent<BaseController>().wall = base_.wall;
        GameObject.Find("Gate").GetComponent<BaseController>().baseHealth = base_.health;

        GameObject.Find("Gate").GetComponent<BaseController>().matchWalls();
    }




    public static void playerSave(string path)
	{
		var player =  new Player();

		var serializer = new XmlSerializer(typeof(Player));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, player);
		}
	}

    public static Player playerpreLoad(string path)
    {
        var serializer = new XmlSerializer(typeof(Player));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Player;
        }

    }

    public static void playerLoad()
    {

        var player = playerpreLoad("Assets/saves/Player.xml");

        GameObject tempplayer = GameObject.FindWithTag("Player");
        Vector3 templocation;
        templocation.x = player.getPosx();
        templocation.y = 0.0f;
        templocation.z = player.getPosz();
        Quaternion temprotation;
        temprotation.x = player.getRotx();
        temprotation.y = player.getRoty();
        temprotation.w = player.getRotw();
        temprotation.z = player.getRotz();


        tempplayer.transform.position = templocation;
        tempplayer.transform.rotation = temprotation;
    }

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

	public static void outsideSave(string path){

		var outside = new Outsidesave();

		var serializer = new XmlSerializer(typeof(Outsidesave));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, outside);
		}
	}
	


	public static TurretList turretLoad(string path){
		var serializer = new XmlSerializer(typeof(TurretList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as TurretList;
		}
	}

	public static Outsidesave outsidepreLoad(string path){
		var serializer = new XmlSerializer(typeof(Outsidesave));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Outsidesave;
		}
	}

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
		PlayerController.setCount_2(outside.unitCount);
	}
	
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

    public static MapSaver mapPre_load(string path)
    {
        var serializer = new XmlSerializer(typeof(MapSaver));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MapSaver;
        }
    }

    public static void mapLoad(string path)
    {
        var map_load = mapPre_load(path);
        WorldBuilderII world = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
       
        map_load.MapLoader(world);
    }

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