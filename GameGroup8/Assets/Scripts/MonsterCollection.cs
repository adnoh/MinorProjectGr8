
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

public class Outsidesave{
	
	public int wave;
	public float timeTillNextWave;
	public int enemiesDefeaten;
	public int enemiesToDefeat;
	public int totalEnemiesSpawned;
	
	public string tagOfMat1;
	public string tagOfMat2;
	public string tagOfMat3;
	public string tagOfMat4;
	
	public int unitCount;

	public Outsidesave(){
        wave = Camera.main.GetComponent<EnemySpawner>().wave;
		timeTillNextWave = Camera.main.GetComponent<EnemySpawner> ().timeTillNextWave;
		enemiesDefeaten = EnemySpawner.enemiesDefeaten;
		totalEnemiesSpawned = EnemySpawner.totalEnemiesSpawned;
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
		posy = position.y;
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


	 void Update(){

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			playerSave ("Assets/saves/Player.xml");
            MonsterSave("Assets/saves/Monster");
		}

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
			playerLoad();
            // EnemySpawner enemySpawner = Camera.main.GetComponent<EnemySpawner>();
            // enemySpawner.savewave();
        }

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			GameObject.Find ("Gate").GetComponent<BaseController>().buildFromSave();
			outsideLoad();
		}

    }	

    // Save & Load 
	public static void MonsterSave(string path)
	{
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, monsterlist);
		}
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

		var outside = new Outsidesave ();

		var serializer = new XmlSerializer(typeof(Outsidesave));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, outside);
		}
	}
	
	public static MonsterList MonsterLoad(string path){
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MonsterList;
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

	public static void outsideLoad(){
		var outside = outsidepreLoad ("Assets/saves/outside.xml");

		Camera.main.GetComponent<EnemySpawner> ().wave = outside.wave;
		Camera.main.GetComponent<EnemySpawner> ().timeTillNextWave = outside.timeTillNextWave + Time.time;
		Camera.main.GetComponent<EnemySpawner> ().enemiesToDefeat = outside.enemiesToDefeat;
		EnemySpawner.enemiesDefeaten = outside.enemiesDefeaten;
		EnemySpawner.totalEnemiesSpawned = outside.totalEnemiesSpawned;
		GameObject.Find ("PlacementPlane").tag = outside.tagOfMat1;
		GameObject.Find ("PlacementPlane (1)").tag = outside.tagOfMat2;
		GameObject.Find ("PlacementPlane (2)").tag = outside.tagOfMat3;
		GameObject.Find ("PlacementPlane (3)").tag = outside.tagOfMat4;
		PlayerController.setCount (-outside.unitCount);
	}
	public static Player playerpreLoad(string path){
		var serializer = new XmlSerializer(typeof(Player));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Player;
		}
		
	}
	public static void playerLoad(){
		
		var player = playerpreLoad ("Assets/saves/Player.xml");
		
		GameObject tempplayer = GameObject.FindWithTag("Player");
		Vector3 templocation;
		templocation.x = player.getPosx();
		templocation.y = player.getPosy();
		templocation.z = player.getPosz();
		Quaternion temprotation;
		temprotation.x = player.getRotx();
		temprotation.y = player.getRoty();
		temprotation.w = player.getRotw();
		temprotation.z = player.getRotz();


        tempplayer.transform.position = templocation;
        tempplayer.transform.rotation = temprotation;
        
		
	}
}