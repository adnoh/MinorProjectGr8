using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MiniMapScript : MonoBehaviour {

	public Text playerDot;
	public Text enemyDot;
    public Text baseDot;
	public GameObject miniMap;

	public static List<EnemyController> enemies = new List<EnemyController> ();
	private List<Text> enemiesDotList = new List<Text> ();

	public static EnemyController enemy;

	/// <summary>
	/// Called every frame to move the dots of the enemies and the player.
	/// </summary>
	void Update(){
		float xPlayer = 0.5f * (PlayerController.getPosition ().x);
		float yPlayer = 0.5f * (PlayerController.getPosition ().z);
		playerDot.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPlayer, yPlayer, 0f);

		while (enemiesDotList.Count < enemies.Count) {
			enemiesDotList.Add ((Text)Instantiate (enemyDot));
			enemiesDotList[enemiesDotList.Count - 1].transform.SetParent(miniMap.transform, false);
		}

		while (enemiesDotList.Count > enemies.Count) {
			enemiesDotList[enemiesDotList.Count - 1].enabled = false;
			enemiesDotList.RemoveAt(enemiesDotList.Count - 1);
		}

		if (enemies.Count > 0) {
			for(int i = 0; i < enemies.Count; i++){
				enemiesDotList[i].enabled = true;
				float xEnemy = 0.5f * (enemies[i].gameObject.transform.position.x);
				float yEnemy = 0.5f * (enemies[i].gameObject.transform.position.z);
				enemiesDotList[i].GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xEnemy, yEnemy, 0f);
			}
		}
	}

	/// <summary>
	/// removes all the enemies from the class. Is used when the main menu or pause menu is opened.
	/// </summary>
    public static void clearEnemies(){
        enemies.Clear();
    }

	/// <summary>
	/// Shows the base on the mini map.
	/// </summary>
    public void ShowBase_Mmap(){
        Vector3 basePos = GameObject.FindGameObjectWithTag("BASE").transform.position;
        float xBase = 0.5f * basePos.x;
        float yBase = 0.5f * basePos.z;

        Vector3 baseDot_pos = new Vector3(xBase, yBase, 0f);
        
        baseDot.GetComponent<RectTransform>().anchoredPosition = baseDot_pos;
    }
}