
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
}

public class Outsidesave{
	
	public int wave;
	public float timeTillNextWave;
	public int enemiesDefeaten;
	
	public string tagOfMat1;
	public string tagOfMat2;
	public string tagOfMat3;
	public string tagOfMat4;
	
	public int unitCount;

	public Outsidesave(){
		wave = Camera.main.GetComponent<EnemySpawner> ().wave;
		timeTillNextWave = Camera.main.GetComponent<EnemySpawner> ().timeTillNextWave;
		enemiesDefeaten = EnemySpawner.enemiesDefeaten;
		tagOfMat1 = GameObject.Find ("PlacementPlane").tag;
		tagOfMat2 = GameObject.Find ("PlacementPlane(1)").tag;
		tagOfMat3 = GameObject.Find ("PlacementPlane(2)").tag;
		tagOfMat4 = GameObject.Find ("PlacementPlane(3)").tag;
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
}


public class MonsterCollection : MonoBehaviour
{
	public static MonsterList monsterlist = new MonsterList();
	public static TurretList turretList = new TurretList();

	//[XmlArray("monstersList"),XmlArrayItem("monstersList")]
	//public Monster[] monstersList = new Monster[2];
	
	 void Update(){

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			//playerSave ("Assets/saves/Player.xml");
		}

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            EnemySpawner enemySpawner = Camera.main.GetComponent<EnemySpawner>();
            enemySpawner.savewave();
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

	/* public void playerSave(string path)
	{
		var player =  new Player();

		var serializer = new XmlSerializer(typeof(Player));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, player);
		}
	}
    */

	public static void outsideSave(string path){
		List<GameObject> Buildings = BaseController.turrets;
		for (int i = 0; i < Buildings.Count; i++){
			turretList.list[i] = new Turret(Buildings[i].GetComponent<BuildingController>());
		}
		
		var serializer = new XmlSerializer(typeof(Turret));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, turretList);
		}
	}
	
	public static MonsterList MonsterLoad(string path){
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MonsterList;
		}
	}
}