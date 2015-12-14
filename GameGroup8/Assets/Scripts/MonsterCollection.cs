
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
	public MonsterList monsterlist = new MonsterList();

	//[XmlArray("monstersList"),XmlArrayItem("monstersList")]
	//public Monster[] monstersList = new Monster[2];

	void Start(){



	}
	 void Update(){

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			playerSave ("Assets/saves/Player.xml");
		}

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            EnemySpawner enemySpawner = Camera.main.GetComponent<EnemySpawner>();
            enemySpawner.savewave();
        }

    }	

    // Save & Load 
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, monsterlist);
		}
	}

	public void playerSave(string path)
	{
		var player =  new Player();

		var serializer = new XmlSerializer(typeof(Player));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, player);
		}
	}
	
	public static MonsterList Load(string path)
	{
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MonsterList;
		}
	}
}