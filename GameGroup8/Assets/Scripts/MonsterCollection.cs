
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


public class MonsterCollection : MonoBehaviour
{
	public MonsterList monsterlist = new MonsterList();

	//[XmlArray("monstersList"),XmlArrayItem("monstersList")]
	//public Monster[] monstersList = new Monster[2];

	void Start(){



	}
	 void Update(){

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			List<EnemyController> Monsters = MiniMapScript.enemies;
			for (int i = 0; i < Monsters.Count; i++) {
				monsterlist.list[i]= new Monster (Monsters[i]);
			}
			Save ("Assets/saves/monsters.xml");
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
	
	public static MonsterList Load(string path)
	{
		var serializer = new XmlSerializer(typeof(MonsterList));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MonsterList;
		}
	}
}